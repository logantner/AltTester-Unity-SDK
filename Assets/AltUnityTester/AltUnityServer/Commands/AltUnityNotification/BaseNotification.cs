using System.Globalization;
using Altom.AltUnityDriver.Commands;
using Altom.AltUnityTester.Communication;
using Newtonsoft.Json;

namespace Altom.AltUnityTester.Notification
{
    public class BaseNotification
    {
        public static void SendNotification<T>(T data, string commandName)
        {
            var cmdResponse = new CommandResponse
            {
                commandName = commandName,
                messageId = null,
                data = JsonConvert.SerializeObject(data),
                error = null,
                isNotification = true
            };

            var notification = JsonConvert.SerializeObject(cmdResponse, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Culture = CultureInfo.InvariantCulture
            });
            BaseWebSocketHandler.NotificationHandler.Send(notification);

        }
    }
}