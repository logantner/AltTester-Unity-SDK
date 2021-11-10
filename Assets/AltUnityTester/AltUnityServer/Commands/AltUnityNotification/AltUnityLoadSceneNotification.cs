using Altom.AltUnityDriver.Notifications;
using Altom.AltUnityTester.Communication;
using UnityEngine.SceneManagement;

namespace Altom.AltUnityTester.Notification
{
    public class AltUnityLoadSceneNotification : BaseNotification
    {
        public AltUnityLoadSceneNotification(ICommandHandler commandHandler, bool isOn) : base(commandHandler)
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

        void onSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            var data = new AltUnityLoadSceneNotificationResultParams(scene.name, mode);
            SendNotification(data, "loadSceneNotification");
        }
    }
}