﻿namespace TrashMob.Security
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using TrashMob.Models;
    using TrashMob.Shared.Persistence.Interfaces;

    public class UserIsPartnerUserOrIsAdminAuthHandler : AuthorizationHandler<UserIsPartnerUserOrIsAdminRequirement, Partner>
    {
        private readonly IHttpContextAccessor httpContext;
        private readonly IUserRepository userRepository;
        private readonly IPartnerUserRepository partnerUserRepository;

        public UserIsPartnerUserOrIsAdminAuthHandler(IHttpContextAccessor httpContext, IUserRepository userRepository, IPartnerUserRepository partnerUserRepository)
        {
            this.httpContext = httpContext;
            this.userRepository = userRepository;
            this.partnerUserRepository = partnerUserRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserIsPartnerUserOrIsAdminRequirement requirement, Partner resource)
        {
            var userName = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var user = await userRepository.GetUserByNameIdentifier(userName);

            if (user == null)
            {
                return;
            }

            httpContext.HttpContext.Items.Add("UserId", user.Id);

            if (user.IsSiteAdmin)
            {
                context.Succeed(requirement);
            }
            else
            {
                var currentUserPartner = partnerUserRepository.GetPartnerUsers().FirstOrDefault(pu => pu.PartnerId == resource.Id && pu.UserId == user.Id);

                if (currentUserPartner != null)
                {
                    context.Succeed(requirement);
                }
            }
        }
    }
}
