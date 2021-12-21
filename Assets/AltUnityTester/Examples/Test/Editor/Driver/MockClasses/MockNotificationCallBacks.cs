using Altom.AltUnityDriver.Notifications;
namespace Altom.AltUnityDriver.MockClasses
{
    internal class MockNotificationCallBacks : INotificationCallbacks
    {
        public static string LastSceneLoaded = "";
        public static string LastSceneUnloaded = "";
        public static string LastChangeInHierarchy = "";
        public void SceneLoadedCallback(AltUnityLoadSceneNotificationResultParams altUnityLoadSceneNotificationResultParams)
        {
            LastSceneLoaded = altUnityLoadSceneNotificationResultParams.sceneName;
        }
        public void HierarchyChangesCallback(AltUnityHierarchyNotificationResultParams altUnityHierarchyNotificationResultParams)
        {
            // LastChangeInHierarchy = altUnityHierarchyNotificationResultParams.hierarchyMode;
        }

        public void SceneUnloadedCallback(string sceneName)
        {
            LastSceneUnloaded = sceneName;
        }
    }
}
