namespace Altom.AltUnityTester.Communication
{
    public class BaseWebSocketHandler
    {
        protected readonly ICommandHandler _commandHandler;

        public static INotificationHandler NotificationHandler;

        public BaseWebSocketHandler(ICommandHandler commandHandler, INotificationHandler notificationHandler)
        {
            _commandHandler = commandHandler;
            NotificationHandler = notificationHandler;
        }
    }
}