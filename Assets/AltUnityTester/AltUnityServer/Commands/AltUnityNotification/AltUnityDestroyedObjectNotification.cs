using System.Reflection;
using Altom.AltUnityDriver.Notifications;
using Altom.AltUnityTester.Communication;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Altom.AltUnityTester.Notification
{
    public class AltUnityDestroyedObjectNotification : BaseNotification
    {
        List<UnityEngine.GameObject> hierarchyObjects;

        public AltUnityDestroyedObjectNotification(ICommandHandler commandHandler, bool isOn) : base(commandHandler)
        {
            hierarchyObjects.addedObject -= onObjectDestroyed;

            if (isOn)
            {
                hierarchyObjects.addedObject += onObjectDestroyed;

            }

        }

        static void onObjectDestroyed(GameObject gameObject, AltUnityHierarchyMode mode)
        {
            var data = new AltUnityHierarchyNotificationResultParams(gameObject.name, (AltUnityHierarchyMode)mode);
            SendNotification(data, "objectDeletedNotification");
        }
    }
}