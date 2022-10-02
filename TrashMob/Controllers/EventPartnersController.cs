﻿
namespace TrashMob.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.ApplicationInsights;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Identity.Web.Resource;
    using TrashMob.Models;
    using TrashMob.Poco;
    using TrashMob.Shared;
    using TrashMob.Shared.Engine;
    using TrashMob.Shared.Managers.Interfaces;
    using TrashMob.Shared.Managers.Partners;
    using TrashMob.Shared.Persistence.Interfaces;

    [Route("api/eventpartners")]
    public class EventPartnersController : SecureController
    {
        private readonly IEventPartnerRepository eventPartnerRepository;
        private readonly IUserRepository userRepository;
        private readonly IKeyedManager<Partner> partnerManager;
        private readonly IPartnerRepository partnerRepository;
        private readonly IPartnerLocationRepository partnerLocationRepository;
        private readonly IPartnerUserRepository partnerUserRepository;
        private readonly IEmailManager emailManager;

        public EventPartnersController(TelemetryClient telemetryClient,
                                       IUserRepository userRepository,
                                       IKeyedManager<Partner> partnerManager,   
                                       IAuthorizationService authorizationService,
                                       IEventPartnerRepository eventPartnerRepository, 
                                       IPartnerRepository partnerRepository, 
                                       IPartnerLocationRepository partnerLocationRepository,
                                       IPartnerUserRepository partnerUserRepository,
                                       IEmailManager emailManager) 
            : base(telemetryClient, authorizationService)
        {
            this.eventPartnerRepository = eventPartnerRepository;
            this.userRepository = userRepository;
            this.partnerManager = partnerManager;
            this.partnerRepository = partnerRepository;
            this.partnerLocationRepository = partnerLocationRepository;
            this.partnerUserRepository = partnerUserRepository;
            this.emailManager = emailManager;
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetEventPartners(Guid eventId, CancellationToken cancellationToken)
        {
            var displayEventPartners = new List<DisplayEventPartner>();
            var currentPartners = await eventPartnerRepository.GetEventPartners(eventId, cancellationToken).ConfigureAwait(false);
            var possiblePartners = await eventPartnerRepository.GetPotentialEventPartners(eventId, cancellationToken).ConfigureAwait(false);

            // Convert the current list of partners for the event to a display partner (reduces round trips)
            foreach (var cp in currentPartners.ToList())
            {
                var displayEventPartner = new DisplayEventPartner
                {
                    EventId = eventId,
                    PartnerId = cp.PartnerId,
                    PartnerLocationId = cp.PartnerLocationId,
                    EventPartnerStatusId = cp.EventPartnerStatusId,
                };

                var partner = await partnerRepository.GetPartner(cp.PartnerId, cancellationToken).ConfigureAwait(false);
                displayEventPartner.PartnerName = partner.Name;

                var partnerLocation = partnerLocationRepository.GetPartnerLocations(cancellationToken).FirstOrDefault(pl => pl.PartnerId == cp.PartnerId && pl.Id == cp.PartnerLocationId);

                displayEventPartner.PartnerLocationName = partnerLocation.Name;
                displayEventPartner.PartnerLocationNotes = partnerLocation.PublicNotes;
 
                displayEventPartners.Add(displayEventPartner);
            }

            // Convert the current list of possible partners for the event to a display partner unless the partner location is already included (reduces round trips)
            foreach (var pp in possiblePartners.ToList())
            {
                if (!displayEventPartners.Any(ep => ep.PartnerLocationId == pp.Id))
                {
                    var displayEventPartner = new DisplayEventPartner
                    {
                        EventId = eventId,
                        PartnerId = pp.PartnerId,
                        PartnerLocationId = pp.Id,
                        EventPartnerStatusId = (int)EventPartnerStatusEnum.None,
                        PartnerLocationName = pp.Name,
                        PartnerLocationNotes = pp.PublicNotes,
                    };

                    var partner = await partnerRepository.GetPartner(pp.PartnerId, cancellationToken).ConfigureAwait(false);
                    displayEventPartner.PartnerName = partner.Name;

                    displayEventPartners.Add(displayEventPartner);
                }
            }

            return Ok(displayEventPartners);
        }

        [HttpPut]
        [RequiredScope(Constants.TrashMobWriteScope)]
        public async Task<IActionResult> UpdateEventPartner(EventPartner eventPartner)
        {
            var partner = partnerManager.Get(eventPartner.PartnerId);

            if (partner == null)
            {
                return NotFound();
            }

            var authResult = await AuthorizationService.AuthorizeAsync(User, partner, "UserIsPartnerUserOrIsAdmin");

            if (!User.Identity.IsAuthenticated || !authResult.Succeeded)
            {
                return Forbid();
            }

            eventPartner.LastUpdatedByUserId = UserId;

            var updatedEventPartner = await eventPartnerRepository.UpdateEventPartner(eventPartner).ConfigureAwait(false);

            var user = await userRepository.GetUserByInternalId(eventPartner.CreatedByUserId).ConfigureAwait(false);

            // Notify Admins that a partner request has been responded to
            var subject = "A partner request for an event has been responded to!";
            var message = $"A partner request for an event has been responded to for event {eventPartner.EventId}!";

            var recipients = new List<EmailAddress>
            {
                new EmailAddress { Name = Constants.TrashMobEmailName, Email = Constants.TrashMobEmailAddress }
            };

            var adminDynamicTemplateData = new
            {
                username = Constants.TrashMobEmailName,
                emailCopy = message,
                subject = subject,
            };

            await emailManager.SendTemplatedEmail(subject, SendGridEmailTemplateId.GenericEmail, SendGridEmailGroupId.EventRelated, adminDynamicTemplateData, recipients, CancellationToken.None).ConfigureAwait(false);

            var partnerMessage = emailManager.GetHtmlEmailCopy(NotificationTypeEnum.EventPartnerResponse.ToString());
            var partnerSubject = "A TrashMob.eco Partner has responded to your request!";

            partnerMessage = partnerMessage.Replace("{UserName}", user.UserName);

            var dashboardLink = string.Format("https://www.trashmob.eco/manageeventdashboard/{0}", eventPartner.EventId);
            partnerMessage = partnerMessage.Replace("{PartnerResponseUrl}", dashboardLink);

            var partnerRecipients = new List<EmailAddress>
            {
                new EmailAddress { Name = user.UserName, Email = user.Email },
            };

            var dynamicTemplateData = new
            {
                username = user.UserName,
                emailCopy = partnerMessage,
                subject = subject,
            };

            await emailManager.SendTemplatedEmail(partnerSubject, SendGridEmailTemplateId.GenericEmail, SendGridEmailGroupId.EventRelated, dynamicTemplateData, partnerRecipients, CancellationToken.None).ConfigureAwait(false);
            TelemetryClient.TrackEvent(nameof(UpdateEventPartner));

            return Ok(updatedEventPartner);
        }

        [HttpPost]
        [Authorize(Policy = "ValidUser")]
        [RequiredScope(Constants.TrashMobWriteScope)]
        public async Task<IActionResult> AddEventPartner(EventPartner eventPartner)
        {
            eventPartner.CreatedByUserId = UserId;
            eventPartner.LastUpdatedByUserId = UserId;
            eventPartner.CreatedDate = DateTimeOffset.UtcNow;
            eventPartner.LastUpdatedDate = DateTimeOffset.UtcNow;
            await eventPartnerRepository.AddEventPartner(eventPartner).ConfigureAwait(false);

            var partnerLocation = partnerLocationRepository.GetPartnerLocations().FirstOrDefault(pl => pl.Id == eventPartner.PartnerLocationId);

            // Notify Admins that a new partner request has been made
            var subject = "A New Partner Request for an Event has been made!";
            var message = $"A new partner request for an event has been made for event {eventPartner.EventId}!";

            var recipients = new List<EmailAddress>
            {
                new EmailAddress { Name = Constants.TrashMobEmailName, Email = Constants.TrashMobEmailAddress }
            };

            var adminDynamicTemplateData = new
            {
                username = Constants.TrashMobEmailName,
                emailCopy = message,
                subject = subject,
            };

            await emailManager.SendTemplatedEmail(subject, SendGridEmailTemplateId.GenericEmail, SendGridEmailGroupId.EventRelated, adminDynamicTemplateData, recipients, CancellationToken.None).ConfigureAwait(false);

            var partnerMessage = emailManager.GetHtmlEmailCopy(NotificationTypeEnum.EventPartnerRequest.ToString());
            var partnerSubject = "A TrashMob.eco Event would like to Partner with you!";

            partnerMessage = partnerMessage.Replace("{PartnerLocationName}", partnerLocation.Name);

            var dynamicTemplateData = new
            {
                username = partnerLocation.Name,
                emailCopy = partnerMessage,
                subject = subject,
            };

            var partnerRecipients = new List<EmailAddress>
            {
                new EmailAddress { Name = partnerLocation.Name, Email = partnerLocation.PrimaryEmail },
                new EmailAddress { Name = partnerLocation.Name, Email = partnerLocation.SecondaryEmail }
            };

            await emailManager.SendTemplatedEmail(partnerSubject, SendGridEmailTemplateId.GenericEmail, SendGridEmailGroupId.EventRelated, dynamicTemplateData, partnerRecipients, CancellationToken.None).ConfigureAwait(false);
            TelemetryClient.TrackEvent(nameof(AddEventPartner));

            return Ok();
        }
    }
}
