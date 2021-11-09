using System.Text;
using Altom.AltUnityTester.Logging;

namespace Altom.AltUnityTester.Communication
{

    public class AltWebGLWebSocketHandler : BaseWebSocketHandler
    {
        protected readonly WebGLWebSocket _webSocket;

        public AltWebGLWebSocketHandler(ICommandHandler cmdHandler, WebGLWebSocket webSocket, INotificationHandler notificationHandler) : base(cmdHandler, notificationHandler)
        {
            this._webSocket = webSocket;
            this._webSocket.OnMessage += this.onMessage;

            _commandHandler.OnSendMessage += (message) =>
             {
                 this._webSocket.SendText(message).ConfigureAwait(false).GetAwaiter().GetResult();
             };

            NotificationHandler.OnSendMessage += (message) =>
            {
                this._webSocket.SendText(message).ConfigureAwait(false).GetAwaiter().GetResult();
            };

        }
        private void onMessage(byte[] data)
        {
            var message = Encoding.UTF8.GetString(data);
            this._commandHandler.OnMessage(message);
        }
    }
}