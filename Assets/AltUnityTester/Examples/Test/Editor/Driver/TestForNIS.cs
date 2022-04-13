using System;
using System.Threading;
using Altom.AltUnityDriver;
using NUnit.Framework;

public class TestForNIS
{
    public AltUnityDriver altUnityDriver;
    //Before any test it connects with the socket
    string scene7 = "Assets/AltUnityTester/Examples/Scenes/Scene 7 Drag And Drop NIS.unity";
    string scene8 = "Assets/AltUnityTester/Examples/Scenes/Scene 8 Draggable Panel NIP.unity";
    string scene9 = "Assets/AltUnityTester/Examples/Scenes/scene 9 NIS.unity";
    string scene10 = "Assets/AltUnityTester/Examples/Scenes/Scene 10 Sample NIS.unity";
    string scene11 = "Assets/AltUnityTester/Examples/Scenes/Scene 7 New Input System Actions.unity";
    string scene12 = "Assets/AltUnityTester/Examples/Scenes/Scene6.unity";


    [OneTimeSetUp]
    public void SetUp()
    {
        altUnityDriver = new AltUnityDriver();
    }

    //At the end of the test closes the connection with the socket
    [OneTimeTearDown]
    public void TearDown()
    {
        altUnityDriver.Stop();
    }

    [Test]
    public void TestScroll()
    {
        altUnityDriver.LoadScene(scene10);
        var player = altUnityDriver.FindObject(By.NAME, "Player");
        Assert.False(player.GetComponentProperty<bool>("AltUnityNIPDebugScript", "wasScrolled", "Assembly-CSharp"));
        altUnityDriver.Scroll(300, 1, true);
        Assert.True(player.GetComponentProperty<bool>("AltUnityNIPDebugScript", "wasScrolled", "Assembly-CSharp"));
    }

    [Test]
    public void TestTapElement()
    {
        altUnityDriver.LoadScene(scene11);
        var capsule = altUnityDriver.FindObject(By.NAME, "Capsule");
        capsule.Tap();
        var counter = capsule.GetComponentProperty<int>("AltUnityExampleNewInputSystem", "jumpCounter", "Assembly-CSharp");
        Assert.AreEqual(counter, 1);
    }

    [Test]
    public void TestMultiTapElement()
    {
        altUnityDriver.LoadScene(scene11);
        var capsule = altUnityDriver.FindObject(By.NAME, "Capsule");
        capsule.Tap(count: 2, interval: 1.0f);
        var counter = capsule.GetComponentProperty<int>("AltUnityExampleNewInputSystem", "jumpCounter", "Assembly-CSharp");
        Assert.AreEqual(counter, 2);
    }

    [Test]
    public void TestTapCoordinates()
    {
        altUnityDriver.LoadScene(scene11);
        var capsule = altUnityDriver.FindObject(By.NAME, "Capsule");
        altUnityDriver.Tap(capsule.getScreenPosition());
        altUnityDriver.WaitForObject(By.PATH, "//ActionText[@text=Capsule was tapped!]");
    }

    [Test]
    public void TestScrollElement()
    {
        altUnityDriver.LoadScene(scene9);
        var scrollbar = altUnityDriver.FindObject(By.NAME, "Handle");
        var scrollbarPosition = scrollbar.getScreenPosition();
        altUnityDriver.MoveMouse(scrollbarPosition);
        altUnityDriver.Scroll(300, 1, true);
        var scrollbarFinal = altUnityDriver.FindObject(By.NAME, "Handle");
        var scrollbarPositionFinal = scrollbarFinal.getScreenPosition();
        Assert.AreNotEqual(scrollbarPosition.y, scrollbarPositionFinal.y);

    }

    [Test]
    public void TestClickElement()
    {
        altUnityDriver.LoadScene(scene11);
        var capsule = altUnityDriver.FindObject(By.NAME, "Capsule");
        capsule.Click();
        var counter = capsule.GetComponentProperty<int>("AltUnityExampleNewInputSystem", "jumpCounter", "Assembly-CSharp");
        Assert.AreEqual(counter, 1);
    }


    [Test]
    public void TestClickObject()
    {
        altUnityDriver.LoadScene(scene11);
        var capsule = altUnityDriver.FindObject(By.NAME, "Capsule");
        capsule.Click();
        Assert.True(capsule.GetComponentProperty<bool>("AltUnityExampleNewInputSystem", "wasClicked", "Assembly-CSharp"));

    }

    [Test]
    public void TestClickCoordinates()
    {
        altUnityDriver.LoadScene(scene11);
        var capsule = altUnityDriver.FindObject(By.NAME, "Capsule");
        altUnityDriver.Click(capsule.getScreenPosition());
        altUnityDriver.WaitForObject(By.PATH, "//ActionText[@text=Capsule was clicked!]");
    }

    [Test]
    public void TestSwipe()
    {
        altUnityDriver.LoadScene(scene9);
        var scrollbarPosition = altUnityDriver.FindObject(By.NAME, "Handle").getScreenPosition();
        var button = altUnityDriver.FindObject(By.PATH, "//Scroll View/Viewport/Content/Button (4)");
        altUnityDriver.Swipe(new AltUnityVector2(button.x + 1, button.y + 1), new AltUnityVector2(button.x + 1, button.y + 20), 1);
        Thread.Sleep(500);
        var scrollbarPositionFinal = altUnityDriver.FindObject(By.NAME, "Handle").getScreenPosition();
        Assert.AreNotEqual(scrollbarPosition.y,scrollbarPositionFinal.y);

    }

    [Test]
    public void TestMultipointSwipe()
    {
        altUnityDriver.LoadScene(scene7);
        var altElement1 = altUnityDriver.FindObject(By.NAME, "Drag Image1");
        var altElement2 = altUnityDriver.FindObject(By.NAME, "Drop Box1");
        altUnityDriver.MultipointSwipe(new[] { new AltUnityVector2(altElement1.x, altElement1.y), new AltUnityVector2(altElement2.x, altElement2.y) }, 2, wait: false);
        Thread.Sleep(2000);

        altElement1 = altUnityDriver.FindObject(By.NAME, "Drag Image1");
        altElement2 = altUnityDriver.FindObject(By.NAME, "Drop Box1");
        var altElement3 = altUnityDriver.FindObject(By.NAME, "Drop Box2");
        var positions = new[]
        {
            new AltUnityVector2(altElement1.x, altElement1.y),
            new AltUnityVector2(altElement2.x, altElement2.y),
            new AltUnityVector2(altElement3.x, altElement3.y)
        };

        altUnityDriver.MultipointSwipe(positions, 3);
        var imageSource = altUnityDriver.FindObject(By.NAME, "Drag Image1").GetComponentProperty<dynamic>("UnityEngine.UI.Image", "sprite", "UnityEngine.UI");
        var imageSourceDropZone = altUnityDriver.FindObject(By.NAME, "Drop Image").GetComponentProperty<dynamic>("UnityEngine.UI.Image", "sprite", "UnityEngine.UI");
        Assert.AreNotEqual(imageSource["name"], imageSourceDropZone["name"]);

        imageSource = altUnityDriver.FindObject(By.NAME, "Drag Image2").GetComponentProperty<dynamic>("UnityEngine.UI.Image", "sprite", "UnityEngine.UI");
        imageSourceDropZone = altUnityDriver.FindObject(By.NAME, "Drop").GetComponentProperty<dynamic>("UnityEngine.UI.Image", "sprite", "UnityEngine.UI");
        Assert.AreNotEqual(imageSource["name"], imageSourceDropZone["name"]);
    }

    public void TestTilt()
    {
        altUnityDriver.LoadScene(scene11);
        var capsule = altUnityDriver.FindObject(By.NAME, "Capsule");
        var initialPosition = capsule.getWorldPosition();
        altUnityDriver.Tilt(new AltUnityVector3(1000, 10, 10), 3f);
        Assert.AreNotEqual(initialPosition, altUnityDriver.FindObject(By.NAME, "Capsule").getWorldPosition());
    }
}