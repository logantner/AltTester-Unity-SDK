namespace Altom.AltUnityTester.Communication
{
    public interface INotificationHandler
    {

        SendMessageHandler OnSendMessage { get; set; }
        void Send(string data);
    }
}
