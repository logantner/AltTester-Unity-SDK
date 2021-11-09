using Altom.AltUnityDriver.Notifications;

namespace Altom.AltUnityDriver.Commands
{
    public class AltUnitySetNotification : AltBaseCommand
    {
        private readonly AltUnitySetNotificationParams cmdParams;

        public AltUnitySetNotification(IDriverCommunication commHandler, NotificationType notificationType) : base(commHandler)
        {
            this.cmdParams = new AltUnitySetNotificationParams(notificationType);
        }
        public void Execute()
        {
            this.CommHandler.Send(this.cmdParams);
            var data = this.CommHandler.Recvall<string>(this.cmdParams);
            ValidateResponse("Ok", data);
        }
    }
}
