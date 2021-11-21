using System.Threading;
using Altom.AltUnityDriver;
using Altom.AltUnityDriver.Logging;
using Altom.AltUnityDriver.Notifications;
using Altom.AltUnityTester.MockClasses;
using NUnit.Framework;

public class TestNotification
{
    private AltUnityDriver altUnityDriver;
    [OneTimeSetUp]
    public void SetUp()
    {
        string portStr = System.Environment.GetEnvironmentVariable("PROXY_PORT");
        int port = 13000;
        if (!string.IsNullOrEmpty(portStr)) port = int.Parse(portStr);
        altUnityDriver = new AltUnityDriver(port: port, enableLogging: true);
        INotificationCallbacks notificationCallbacks = new MockNotificationCallBacks();
        altUnityDriver.SetNotification(NotificationType.ALL, notificationCallbacks);
        DriverLogManager.SetMinLogLevel(AltUnityLogger.Console, AltUnityLogLevel.Info);
        DriverLogManager.SetMinLogLevel(AltUnityLogger.Unity, AltUnityLogLevel.Info);
    }
    [OneTimeTearDown]
    public void TearDown()
    {
        altUnityDriver.SetNotification(NotificationType.None);
        altUnityDriver.Stop();
    }

    [SetUp]
    public void LoadLevel()
    {

        altUnityDriver.LoadScene("Scene 1 AltUnityDriverTestScene", true);
    }

    [Test]
    public void TestLoadSceneNotification()
    {
        Thread.Sleep(1000);
        Assert.AreEqual("Scene 1 AltUnityDriverTestScene", MockNotificationCallBacks.LastSceneLoaded);
    }


}