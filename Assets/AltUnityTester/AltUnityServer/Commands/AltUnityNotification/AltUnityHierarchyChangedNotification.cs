using System.Reflection;
using Altom.AltUnityDriver.Notifications;
using Altom.AltUnityTester.Communication;
using UnityEngine;
using UnityEditor;
using System;



namespace Altom.AltUnityTester.Notification
{
    public class AltUnityHierarchyChangedNotification : BaseNotification
    {
        
        public AltUnityHierarchyChangedNotification(ICommandHandler commandHandler, bool isOn) : base(commandHandler)
        {
            EditorApplication.hierarchyChanged -= onHierarchyChanged;

            if(isOn)
            {
                EditorApplication.hierarchyChanged += onHierarchyChanged;
            }
        }

        static void onHierarchyChanged(GameObject gameObject, AltUnityHierarchyMode mode)
        {
            var data = new AltUnityHierarchyNotificationResultParams(gameObject.name, (AltUnityHierarchyMode)mode);
            SendNotification(data, "hierarchyChangedNotification");
        }
    }
}