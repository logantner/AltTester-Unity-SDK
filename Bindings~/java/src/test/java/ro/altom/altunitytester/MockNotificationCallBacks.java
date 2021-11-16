package ro.altom.altunitytester;

import ro.altom.altunitytester.Notifications.AltUnityLoadSceneNotificationResultParams;
import ro.altom.altunitytester.Notifications.INotificationCallbacks;

public class MockNotificationCallBacks implements INotificationCallbacks {

    public static String sceneName;

    @Override
    public void SceneLoadedCallBack(
            AltUnityLoadSceneNotificationResultParams altUnityLoadSceneNotificationResultParams) {
        sceneName = altUnityLoadSceneNotificationResultParams.sceneName;
    }

}
