using System.Reflection;
using Altom.AltUnityDriver.Notifications;
using Altom.AltUnityTester.Communication;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Altom.AltUnityTester.Notification
{
    public class AltUnityChangedParentOfObjectNotification : BaseNotification
    {
        List<UnityEngine.GameObject> hierarchyObjects;

        public AltUnityChangedParentOfObjectNotification(ICommandHandler commandHandler, bool isOn) : base(commandHandler)
        {
            hierarchyObjects.addedObject -= onObjectChangedParent;

            if (isOn)
            {
                hierarchyObjects.addedObject += onObjectChangedParent;

            }

        }

        static void onObjectChangedParent(GameObject gameObject, AltUnityHierarchyMode mode)
        {
            var data = new AltUnityHierarchyNotificationResultParams(gameObject.name, (AltUnityHierarchyMode)mode);
            SendNotification(data, "objectChangedParentNotification");
        }
    }
}