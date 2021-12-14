using System.Reflection;
using Altom.AltUnityDriver.Notifications;
using Altom.AltUnityTester.Communication;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Altom.AltUnityTester.Notification
{
    public class AltUnityAddedObjectNotification : BaseNotification
    {
        List<UnityEngine.GameObject> hierarchyObjects;
        public AltUnityAddedObjectNotification(ICommandHandler commandHandler, bool isOn) : base(commandHandler)
        {
            hierarchyObjects.addedObject -= onObjectAdded;

            if (isOn)
            {
                hierarchyObjects.addedObject += onObjectAdded;

            }

        }

        static void onObjectAdded(GameObject gameObject, AltUnityHierarchyMode mode)
        {
            var data = new AltUnityHierarchyNotificationResultParams(gameObject.name, (AltUnityHierarchyMode)mode);
            SendNotification(data, "objectAddedNotification");
        }
    }
}