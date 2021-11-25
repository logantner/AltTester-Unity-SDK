
namespace Altom.AltUnityDriver.Notifications
{
    public interface INotificationCallbacks
    {
        void SceneLoadedCallback(AltUnityLoadSceneNotificationResultParams altUnityLoadSceneNotificationResultParams);
        void HierarchyCallback(AltUnityHierarchyNotificationResultParams altUnityHierarchyNotificationResultParams);
    }
}