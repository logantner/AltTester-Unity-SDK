using System.Reflection;
using Altom.AltUnityDriver.Notifications;
using Altom.AltUnityTester.Communication;
using UnityEngine.SceneManagement;

namespace Altom.AltUnityTester.Notification
{
    public class AltUnityLoadSceneNotification : BaseNotification
    {
        public AltUnityLoadSceneNotification(ICommandHandler commandHandler, bool isOn) : base(commandHandler)
        {
            SceneManager.sceneLoaded -= onSceneLoaded;

            if (isOn)
            {
                UnityEngine.Debug.Log("What is happening " + isOn);
                SceneManager.sceneLoaded += onSceneLoaded;

            }

        }

        static void onSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            var data = new AltUnityLoadSceneNotificationResultParams(scene.name, mode);
            SendNotification(data, "loadSceneNotification");
        }
    }
}