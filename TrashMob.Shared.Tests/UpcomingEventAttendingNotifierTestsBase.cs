namespace TrashMob.Shared.Tests
{
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using TrashMob.Models;
    using TrashMob.Shared.Engine;
    using TrashMob.Shared.Poco;
    using Xunit;

    public abstract class UpcomingEventAttendingNotifierTestsBase : NotifierTestsBase
    {
        protected abstract INotificationEngine Engine { get; }

        protected abstract int NumberOfDaysToAddForEventOutOfWindow { get; }

        [Fact]
        public async Task GenerateNotificationsAsync_WithNoDataAvailable_Succeeds()
        {
            // Arrange

            // Act
            await Engine.GenerateNotificationsAsync().ConfigureAwait(false);

            // Assert
            UserNotificationManager.Verify(_ => _.Add(It.IsAny<UserNotification>()), Times.Never);
            EmailManager.Verify(_ => _.SendTemplatedEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<object>(), It.IsAny<List<EmailAddress>>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task GenerateNotificationsAsync_With1EventForUser_Sends1Email()
        {
            // Arrange
            List<Event> events = GetEventList1();
            List<User> users = GetUserList1();

            // The user is attending all available events
            EventAttendeeManager.Setup(ea => ea.GetEventsUserIsAttending(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<CancellationToken>())).ReturnsAsync(events);
            EventManager.Setup(e => e.GetActiveEvents(It.IsAny<CancellationToken>())).ReturnsAsync(events);
            UserManager.Setup(u => u.Get(It.IsAny<CancellationToken>())).ReturnsAsync(users);

            // Act
            await Engine.GenerateNotificationsAsync().ConfigureAwait(false);

            // Assert
            EventManager.Verify(_ => _.GetActiveEvents(It.IsAny<CancellationToken>()), Times.Once);
            UserManager.Verify(_ => _.Get(It.IsAny<CancellationToken>()), Times.Once);
            EventAttendeeManager.Verify(_ => _.GetEventsUserIsAttending(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
            UserNotificationManager.Verify(_ => _.Add(It.IsAny<UserNotification>()), Times.Once);
            EmailManager.Verify(_ => _.SendTemplatedEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<object>(), It.IsAny<List<EmailAddress>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GenerateNotificationsAsync_With2EventForUser_Sends2Emails()
        {
            // Arrange
            List<Event> events = GetEventList2();
            List<User> users = GetUserList1();

            // The user is attending all available events
            EventAttendeeManager.Setup(ea => ea.GetEventsUserIsAttending(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<CancellationToken>())).ReturnsAsync(events);
            EventManager.Setup(e => e.GetActiveEvents(It.IsAny<CancellationToken>())).ReturnsAsync(events);
            UserManager.Setup(u => u.Get(It.IsAny<CancellationToken>())).ReturnsAsync(users);

            // Act
            await Engine.GenerateNotificationsAsync().ConfigureAwait(false);

            // Assert
            EventManager.Verify(_ => _.GetActiveEvents(It.IsAny<CancellationToken>()), Times.Once);
            UserManager.Verify(_ => _.Get(It.IsAny<CancellationToken>()), Times.Once);
            EventAttendeeManager.Verify(_ => _.GetEventsUserIsAttending(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
            UserNotificationManager.Verify(_ => _.Add(It.IsAny<UserNotification>()), Times.Exactly(2));
            EmailManager.Verify(_ => _.SendTemplatedEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<object>(), It.IsAny<List<EmailAddress>>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact]
        public async Task GenerateNotificationsAsync_With1EventFor2Users_Sends2Email()
        {
            // Arrange
            List<Event> events = GetEventList1();
            List<User> users = GetUserList2();

            // The users are attending all available events
            EventAttendeeManager.Setup(ea => ea.GetEventsUserIsAttending(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<CancellationToken>())).ReturnsAsync(events);
            EventManager.Setup(e => e.GetActiveEvents(It.IsAny<CancellationToken>())).ReturnsAsync(events);
            UserManager.Setup(u => u.Get(It.IsAny<CancellationToken>())).ReturnsAsync(users);

            // Act
            await Engine.GenerateNotificationsAsync().ConfigureAwait(false);

            // Assert
            UserManager.Verify(_ => _.Get(It.IsAny<CancellationToken>()), Times.Once);
            EventManager.Verify(_ => _.GetActiveEvents(It.IsAny<CancellationToken>()), Times.Exactly(2));
            EventAttendeeManager.Verify(_ => _.GetEventsUserIsAttending(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
            UserNotificationManager.Verify(_ => _.Add(It.IsAny<UserNotification>()), Times.Exactly(2));
            EmailManager.Verify(_ => _.SendTemplatedEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<object>(), It.IsAny<List<EmailAddress>>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact]
        public async Task GenerateNotificationsAsync_With1EventFor1UsersWhoIsNotAttending_SendsNoEmail()
        {
            // Arrange
            List<Event> events = GetEventList1();
            List<User> users = GetUserList1();

            EventManager.Setup(e => e.GetActiveEvents(It.IsAny<CancellationToken>())).ReturnsAsync(events);
            UserManager.Setup(u => u.Get(It.IsAny<CancellationToken>())).ReturnsAsync(users);

            // Act
            await Engine.GenerateNotificationsAsync().ConfigureAwait(false);

            // Assert
            UserManager.Verify(_ => _.Get(It.IsAny<CancellationToken>()), Times.Once);
            EventManager.Verify(_ => _.GetActiveEvents(It.IsAny<CancellationToken>()), Times.Once);
            EventAttendeeManager.Verify(_ => _.GetEventsUserIsAttending(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
            UserNotificationManager.Verify(_ => _.Add(It.IsAny<UserNotification>()), Times.Never);
            EmailManager.Verify(_ => _.SendTemplatedEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<object>(), It.IsAny<List<EmailAddress>>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task GenerateNotificationsAsync_With1EventFor1UsersWhoIsAttendingOtherEventsButNotThisOne_SendsOneEmail()
        {
            // Arrange
            List<Event> events = GetEventList1();
            List<User> users = GetUserList1();
            List<Event> alternateEvents = GetEventList1();
            alternateEvents[0].Id = new Guid();

            EventManager.Setup(e => e.GetActiveEvents(It.IsAny<CancellationToken>())).ReturnsAsync(events);
            UserManager.Setup(u => u.Get(It.IsAny<CancellationToken>())).ReturnsAsync(users);

            // The user is attending all available events
            EventAttendeeManager.Setup(ea => ea.GetEventsUserIsAttending(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<CancellationToken>())).ReturnsAsync(events);

            // Act
            await Engine.GenerateNotificationsAsync().ConfigureAwait(false);

            // Assert
            UserManager.Verify(_ => _.Get(It.IsAny<CancellationToken>()), Times.Once);
            EventManager.Verify(_ => _.GetActiveEvents(It.IsAny<CancellationToken>()), Times.Once);
            EventAttendeeManager.Verify(_ => _.GetEventsUserIsAttending(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
            UserNotificationManager.Verify(_ => _.Add(It.IsAny<UserNotification>()), Times.Once);
            EmailManager.Verify(_ => _.SendTemplatedEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<object>(), It.IsAny<List<EmailAddress>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GenerateNotificationsAsync_With1EventFor1UsersWhoHasAlreadyReceivedTheEmail_SendsNoEmail()
        {
            // Arrange
            List<Event> events = GetEventList1();
            List<User> users = GetUserList1();

            // The user is attending all available events
            EventAttendeeManager.Setup(ea => ea.GetEventsUserIsAttending(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<CancellationToken>())).ReturnsAsync(events);
            EventManager.Setup(e => e.GetActiveEvents(It.IsAny<CancellationToken>())).ReturnsAsync(events);
            UserManager.Setup(u => u.Get(It.IsAny<CancellationToken>())).ReturnsAsync(users);

            // The user has already received notifications for all events
            var userNotification = new UserNotification()
            {
                EventId = events[0].Id,
                UserId = users[0].Id,
                SentDate = DateTimeOffset.UtcNow.AddDays(-1),
                UserNotificationTypeId = (int)NotificationType,
            };

            var userNotifications = new List<UserNotification>() { userNotification };

            // Todo: fix this
            // UserNotificationManager.Setup(ea => ea.GetUserNotifications(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(userNotifications);

            // Act
            await Engine.GenerateNotificationsAsync().ConfigureAwait(false);

            // Assert
            UserManager.Verify(_ => _.Get(It.IsAny<CancellationToken>()), Times.Once);
            EventManager.Verify(_ => _.GetActiveEvents(It.IsAny<CancellationToken>()), Times.Once);
            EventAttendeeManager.Verify(_ => _.GetEventsUserIsAttending(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
            UserNotificationManager.Verify(_ => _.Add(It.IsAny<UserNotification>()), Times.Never);
            EmailManager.Verify(_ => _.SendTemplatedEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<object>(), It.IsAny<List<EmailAddress>>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task GenerateNotificationsAsync_With1EventFor1UsersWhoHasNotAlreadyReceivedTheEmail_Sends1Email()
        {
            // Arrange
            List<Event> events = GetEventList1();
            List<User> users = GetUserList1();

            // The user is attending all available events
            EventAttendeeManager.Setup(ea => ea.GetEventsUserIsAttending(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<CancellationToken>())).ReturnsAsync(events);
            EventManager.Setup(e => e.GetActiveEvents(It.IsAny<CancellationToken>())).ReturnsAsync(events);
            UserManager.Setup(u => u.Get(It.IsAny<CancellationToken>())).ReturnsAsync(users);

            var userNotifications = new List<UserNotification>();

            // Todo: fix this
            // UserNotificationManager.Setup(ea => ea.Get(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(userNotifications);

            // Act
            await Engine.GenerateNotificationsAsync().ConfigureAwait(false);

            // Assert
            UserManager.Verify(_ => _.Get(It.IsAny<CancellationToken>()), Times.Once);
            EventManager.Verify(_ => _.GetActiveEvents(It.IsAny<CancellationToken>()), Times.Once);
            EventAttendeeManager.Verify(_ => _.GetEventsUserIsAttending(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
            UserNotificationManager.Verify(_ => _.Add(It.IsAny<UserNotification>()), Times.Once);
            EmailManager.Verify(_ => _.SendTemplatedEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<object>(), It.IsAny<List<EmailAddress>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GenerateNotificationsAsync_With1EventFor1UsersWhereEventIsMoreThanRequiredHoursAway_SendsNoEmail()
        {
            // Arrange
            List<Event> events = GetEventList1();
            List<User> users = GetUserList1();
            // The user is attending all available events
            EventAttendeeManager.Setup(ea => ea.GetEventsUserIsAttending(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<CancellationToken>())).ReturnsAsync(events);

            events[0].EventDate = DateTimeOffset.UtcNow.AddDays(NumberOfDaysToAddForEventOutOfWindow);

            EventManager.Setup(e => e.GetActiveEvents(It.IsAny<CancellationToken>())).ReturnsAsync(events);
            UserManager.Setup(u => u.Get(It.IsAny<CancellationToken>())).ReturnsAsync(users);

            // Act
            await Engine.GenerateNotificationsAsync().ConfigureAwait(false);

            // Assert
            UserManager.Verify(_ => _.Get(It.IsAny<CancellationToken>()), Times.Once);
            EventManager.Verify(_ => _.GetActiveEvents(It.IsAny<CancellationToken>()), Times.Once);
            EventAttendeeManager.Verify(_ => _.GetEventsUserIsAttending(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
            UserNotificationManager.Verify(_ => _.Add(It.IsAny<UserNotification>()), Times.Never);
            EmailManager.Verify(_ => _.SendTemplatedEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<object>(), It.IsAny<List<EmailAddress>>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
