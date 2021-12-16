using System.Reflection;
using Altom.AltUnityDriver.Notifications;
using Altom.AltUnityTester.Communication;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;


namespace Altom.AltUnityTester.Notification
{
    public class AltUnityHierarchyChangedNotification : BaseNotification
    {
        // List<AltUnityObject> hierarchyObjects;
        public AltUnityHierarchyChangedNotification(ICommandHandler commandHandler, bool isOn) : base(commandHandler)
        {
            // CreateGameObjectHierarchyEventArgs.instanceId -= onObjectChanged;
                
            // if(isOn)
            // {
            //     CreateGameObjectHierarchyEventArgs.isInstanceId += onObjectChanged;
            // }
            // Hierarchy.childCount -= onHierarchyChanged;

        }

        static void onHierarchyChanged(GameObject gameObject, AltUnityHierarchyMode mode)
        {
            var data = new AltUnityHierarchyNotificationResultParams(gameObject.name, (AltUnityHierarchyMode)mode);
            SendNotification(data, "hierarchyChangedNotification");
        }
    }
}