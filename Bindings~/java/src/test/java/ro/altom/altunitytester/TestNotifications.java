package ro.altom.altunitytester;

import static org.junit.Assert.assertEquals;

import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;

import ro.altom.altunitytester.Commands.AltUnityCommands.AltUnitySetNotificationParams;
import ro.altom.altunitytester.Commands.UnityCommand.AltLoadSceneParameters;

public class TestNotifications {
    private static AltUnityDriver altUnityDriver;

    @BeforeClass
    public static void setUp() throws Exception {
        altUnityDriver = new AltUnityDriver("127.0.0.1", 13010, true);
        AltUnitySetNotificationParams altUnitySetNotificationParams = new AltUnitySetNotificationParams.Builder()
                .addNotificationForLoadScene().build();
        altUnityDriver.SetNotification(altUnitySetNotificationParams, new MockNotificationCallBacks());
    }

    @AfterClass
    public static void tearDown() throws Exception {

        AltUnitySetNotificationParams altUnitySetNotificationParams = new AltUnitySetNotificationParams.Builder()
                .setAllNotificationOff().build();
        altUnityDriver.SetNotification(altUnitySetNotificationParams, new MockNotificationCallBacks());
        if (altUnityDriver != null) {
            altUnityDriver.stop();
        }
        Thread.sleep(1000);
    }

    @Before
    public void loadLevel() throws Exception {

        altUnityDriver.loadScene(new AltLoadSceneParameters.Builder("Scene 1 AltUnityDriverTestScene").build());
    }

    @Test
    public void testLodeNonExistentScene() throws Exception {
        assertEquals("Scene 1 AltUnityDriverTestScene", MockNotificationCallBacks.sceneName);

    }

}
