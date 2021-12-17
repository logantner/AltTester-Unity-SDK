
namespace Altom.AltUnityDriver.Notifications
{
    public interface INotificationCallbacks
    {
        void SceneLoadedCallback(AltUnityLoadSceneNotificationResultParams altUnityLoadSceneNotificationResultParams);
        void HierarchyChangesCallback(AltUnityHierarchyNotificationResultParams altUnityHierarchyNotificationResultParams);
        void SceneUnloadedCallback(string sceneName);
    }
}