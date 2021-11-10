using Altom.AltUnityDriver.Commands;
using Altom.AltUnityDriver.Notifications;
using Altom.AltUnityTester.Communication;
using Altom.AltUnityTester.Notification;

namespace Altom.AltUnityTester.Commands
{
    public class AltUnitySetNotificationCommand : AltUnityCommand<AltUnitySetNotificationParams, string>
    {
        ICommandHandler commandHandler;
        public AltUnitySetNotificationCommand(ICommandHandler commandHandler, AltUnitySetNotificationParams cmdParams) : base(cmdParams)
        {
            this.commandHandler = commandHandler;

        }
        public override string Execute()
        {
            new AltUnityLoadSceneNotification(commandHandler, CommandParams.NotificationType.HasFlag(NotificationType.LOADSCENE));
            return "Ok";
        }

    }
}