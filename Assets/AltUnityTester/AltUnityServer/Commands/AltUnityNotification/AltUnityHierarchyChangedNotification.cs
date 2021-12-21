using System.Reflection;
using Altom.AltUnityDriver.Notifications;
using Altom.AltUnityTester.Communication;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;



namespace Altom.AltUnityTester.Notification
{
    public class AltUnityHierarchyChangedNotification : BaseNotification
    {
        static bool sendNotification = false;
        public AltUnityHierarchyChangedNotification(ICommandHandler commandHandler, bool isOn) : base(commandHandler)
        {
            sendNotification = isOn;
        }

        
    
        public static void onHierarchyChanged(string mode)
        {
            if(sendNotification)
            {
                SendNotification(mode, "hierarchyChangedNotification");
            }

        }
    }
}