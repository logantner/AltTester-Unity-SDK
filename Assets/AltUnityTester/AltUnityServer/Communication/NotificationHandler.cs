using System;
using System.Globalization;
using Altom.AltUnityDriver.Commands;
using Altom.AltUnityTester.Communication;
using Altom.AltUnityTester.Logging;
using Newtonsoft.Json;

namespace Altom.AltUnityTester.Communication
{
    public class NotificationHandler : INotificationHandler
    {

        private static readonly NLog.Logger logger = ServerLogManager.Instance.GetCurrentClassLogger();

        public SendMessageHandler OnSendMessage { get; set; }



        public void Send(string data)
        {
            if (this.OnSendMessage != null)
            {
                this.OnSendMessage.Invoke(data);
                logger.Debug("response sent: " + trimLog(data));
            }
        }

        private string trimLog(string log, int maxLogLength = 1000)
        {
            if (string.IsNullOrEmpty(log)) return log;
            if (log.Length <= maxLogLength) return log;
            return log.Substring(0, maxLogLength) + "[...]";
        }
    }
}
