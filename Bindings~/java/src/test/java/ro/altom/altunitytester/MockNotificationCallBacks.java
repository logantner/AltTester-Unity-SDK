package ro.altom.altunitytester;

import ro.altom.altunitytester.Notifications.AltUnityLoadSceneNotificationResultParams;
import ro.altom.altunitytester.Notifications.BaseNotificationCallbacks;

public class MockNotificationCallBacks extends BaseNotificationCallbacks {

    public static String sceneName;
    public static String objectName;

    @Override
    public void SceneLoadedCallBack(
            AltUnityLoadSceneNotificationResultParams altUnityLoadSceneNotificationResultParams) {
        sceneName = altUnityLoadSceneNotificationResultParams.sceneName;
    }

    @Override
    public void HierarchyCallback(
            AltUnityHierarchyChangedNotificationResultParams altUnityHierarchyChangedNotificationResultParams) {
        objectName = altUnityHierarchyChangedNotificationResultParams.objectName;
    }
    

}
