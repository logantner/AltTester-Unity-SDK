using Altom.AltUnityDriver.Notifications;
namespace Altom.AltUnityDriver.MockClasses
{
    internal class MockNotificationCallBacks : INotificationCallbacks
    {
        public static string LastSceneLoaded = "";
        public static string LastObjectChanged = "";
        public void SceneLoadedCallback(AltUnityLoadSceneNotificationResultParams altUnityLoadSceneNotificationResultParams)
        {
            LastSceneLoaded = altUnityLoadSceneNotificationResultParams.sceneName;
        }
        public void HierarchyChangesCallback(AltUnityHierarchyNotificationResultParams altUnityHierarchyNotificationResultParams)
        {
            LastObjectChanged = altUnityHierarchyNotificationResultParams.objectName;
        }

    }
}
