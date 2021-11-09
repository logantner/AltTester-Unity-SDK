using UnityEngine.Playables;
using WebSocketSharp;

namespace Altom.AltUnityTester.Communication
{
    public class AltClientWebSocketHandler : BaseWebSocketHandler
    {
        private readonly WebSocket _webSocket;


        public AltClientWebSocketHandler(WebSocket webSocket, ICommandHandler commandHandler, INotificationHandler notificationHandler) : base(commandHandler, notificationHandler)
        {
            this._webSocket = webSocket;
            webSocket.OnMessage += this.onMessage;


            this._commandHandler.OnSendMessage += webSocket.Send;

            notificationHandler.OnSendMessage += webSocket.Send;
        }

        private void onMessage(object sender, MessageEventArgs message)
        {
            this._commandHandler.OnMessage(message.Data);
        }
    }
}