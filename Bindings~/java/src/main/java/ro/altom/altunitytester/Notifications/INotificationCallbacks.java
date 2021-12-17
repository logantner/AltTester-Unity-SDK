package ro.altom.altunitytester.Notifications;

public interface INotificationCallbacks {
    void SceneLoadedCallBack(AltUnityLoadSceneNotificationResultParams altUnityLoadSceneNotificationResultParams);
    void HierarchyChangedCallBack(AltUnityHierarchyChangedNotificationResultParams altUnityHierarchyChangedNotificationResultParams);
    void SceneUnloadedCallBack(String sceneName);
}