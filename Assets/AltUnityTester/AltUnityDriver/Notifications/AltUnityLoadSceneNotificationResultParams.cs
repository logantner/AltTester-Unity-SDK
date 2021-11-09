using UnityEngine.SceneManagement;

namespace Altom.AltUnityDriver.Notifications
{
    public class AltUnityLoadSceneNotificationResultParams
    {
        public string sceneName;
        public LoadSceneMode loadSceneMode;

        public AltUnityLoadSceneNotificationResultParams(string sceneName, LoadSceneMode loadSceneMode)
        {
            this.sceneName = sceneName;
            this.loadSceneMode = loadSceneMode;
        }
    }
}