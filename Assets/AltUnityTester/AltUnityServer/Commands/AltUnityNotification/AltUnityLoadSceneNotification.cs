using Altom.AltUnityDriver.Notifications;
using UnityEngine.SceneManagement;

namespace Altom.AltUnityTester.Notification
{
    public class AltUnityLoadSceneNotification : BaseNotification
    {
        public static void SetNotification(bool isOn)
        {
            if (isOn)
            {
                SceneManager.sceneLoaded += onSceneLoaded;

            }
            else
            {
                SceneManager.sceneLoaded -= onSceneLoaded;
            }
        }
        static void onSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            var data = new AltUnityLoadSceneNotificationResultParams(scene.name, mode);
            SendNotification(data, "loadSceneNotification");

        }
    }
}