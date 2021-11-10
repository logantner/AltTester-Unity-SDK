using Altom.AltUnityDriver.Notifications;

namespace Altom.AltUnityDriver.Commands
{
    public interface IDriverCommunication
    {
        INotificationCallbacks NotificationCallbacks { get; set; }

        void Send(CommandParams param);
        T Recvall<T>(CommandParams param);
        void Connect();
        void Close();
    }
}