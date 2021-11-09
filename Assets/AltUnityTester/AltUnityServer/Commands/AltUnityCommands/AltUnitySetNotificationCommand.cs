using Altom.AltUnityDriver.Commands;
using Altom.AltUnityDriver.Notifications;
using Altom.AltUnityTester.Notification;

namespace Altom.AltUnityTester.Commands
{
    public class AltUnitySetNotificationCommand : AltUnityCommand<AltUnitySetNotificationParams, string>
    {
        public AltUnitySetNotificationCommand(AltUnitySetNotificationParams cmdParams) : base(cmdParams)
        {

        }
        public override string Execute()
        {
            AltUnityLoadSceneNotification.SetNotification(CommandParams.NotificationType.HasFlag(NotificationType.LOADSCENE));
            return "Ok";
        }

    }
}