﻿
namespace TrashMob.Shared.Engine
{
    using System.Threading;
    using System.Threading.Tasks;
    using TrashMob.Shared.Persistence;

    public class EventSummaryHostReminderNotifier : NotificationEngineBase, INotificationEngine
    {
        protected override NotificationTypeEnum NotificationType => NotificationTypeEnum.EventSummaryHostReminder;

        protected override int NumberOfHoursInWindow => -4;

        protected override string EmailSubject => "Upcoming TrashMob.eco events in your area today!";

        public EventSummaryHostReminderNotifier(IEventRepository eventRepository, 
                                                IUserRepository userRepository, 
                                                IEventAttendeeRepository eventAttendeeRepository,
                                                IUserNotificationRepository userNotificationRepository,
                                                IUserNotificationPreferenceRepository userNotificationPreferenceRepository,
                                                IEmailSender emailSender,
                                                IMapRepository mapRepository) :
            base(eventRepository, userRepository, eventAttendeeRepository, userNotificationRepository, userNotificationPreferenceRepository, emailSender, mapRepository)
        {
        }


        public Task GenerateNotificationsAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
