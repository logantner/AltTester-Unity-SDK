using Altom.AltUnityDriver.Notifications;
namespace Altom.AltUnityTester.MockClasses
{
    internal class MockNotificationCallBacks : INotificationCallbacks
    {
        public static string LastSceneLoaded;
        public void SceneLoadedCallback(AltUnityLoadSceneNotificationResultParams altUnityLoadSceneNotificationResultParams)
        {
            LastSceneLoaded = altUnityLoadSceneNotificationResultParams.sceneName;
        }
    }
}
