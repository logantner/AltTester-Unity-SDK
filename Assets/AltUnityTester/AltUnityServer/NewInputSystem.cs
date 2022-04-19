#if ENABLE_INPUT_SYSTEM
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Altom.AltUnityTester
{
    public class NewInputSystem : MonoBehaviour
    {
        public static InputTestFixture InputTestFixture = new InputTestFixture();
        public static NewInputSystem Instance;
        public static Keyboard Keyboard;
        public static Mouse Mouse;
        public static Gamepad Gamepad;
        public static Touchscreen Touchscreen;
        public static Accelerometer Accelerometer;
        public void Awake()
        {
            if (Instance == null)
                Instance = this;
            InputTestFixture = new InputTestFixture();
            Keyboard = (Keyboard)InputSystem.GetDevice("AltUnityKeyboard");
            if (Keyboard == null)
            {
                Keyboard = InputSystem.AddDevice<Keyboard>("AltUnityKeyboard");
            }

            Mouse = (Mouse)InputSystem.GetDevice("AltUnityMouse");
            if (Mouse == null)
            {
                Mouse = InputSystem.AddDevice<Mouse>("AltUnityMouse");

            }
            Gamepad = (Gamepad)InputSystem.GetDevice("AltUnityGamepad");
            if (Gamepad == null)
            {
                Gamepad = InputSystem.AddDevice<Gamepad>("AltUnityGamepad");

            }
            Touchscreen = (Touchscreen)InputSystem.GetDevice("AltUnityTouchscreen");
            if (Touchscreen == null)
            {
                Touchscreen = InputSystem.AddDevice<Touchscreen>("AltUnityTouchscreen");
            }
            Accelerometer = (Accelerometer)InputSystem.GetDevice("AltUnityAccelerometer");
            if (Accelerometer == null)
            {
                Accelerometer = InputSystem.AddDevice<Accelerometer>("AltUnityAccelerometer");
            }
        }


        internal static IEnumerator ScrollLifeCircle(float speed, float duration)
        {

            float currentTime = 0;
            float frameTime = 0;// using this because of a bug with yield return which waits only every other iteration
            while (currentTime <= duration - Time.fixedUnscaledDeltaTime)
            {
                InputTestFixture.Set(Mouse.scroll, new Vector2(speed * frameTime / duration, speed * frameTime / duration), queueEventOnly: true);
                var initialTime = Time.fixedUnscaledTime;
                yield return null;
                var afterTime = Time.fixedUnscaledTime;
                frameTime = afterTime - initialTime;
                currentTime += frameTime;
            }
            InputTestFixture.Set(Mouse.scroll, new Vector2(0, 0), queueEventOnly: true);
        }

        internal static IEnumerator MoveMouseCycle(UnityEngine.Vector2 location, float duration)
        {
            float time = 0;
            Mouse.MakeCurrent();
            var mousePosition = Mouse.current.position;
            var distance = location - new UnityEngine.Vector2(mousePosition.x.ReadValue(), mousePosition.y.ReadValue());
            do
            {
                UnityEngine.Vector2 delta;
                if (time + UnityEngine.Time.unscaledDeltaTime < duration)
                {
                    delta = distance * UnityEngine.Time.unscaledDeltaTime / duration;
                }
                else
                {
                    delta = location - new UnityEngine.Vector2(mousePosition.x.ReadValue(), mousePosition.y.ReadValue());
                }

                InputTestFixture.Move(Mouse.current.position, delta);
                yield return null;
                time += UnityEngine.Time.unscaledDeltaTime;
            } while (time < duration);
        }

        internal static IEnumerator TapElementCycle(GameObject target, int count, float interval)
        {
            Touchscreen.MakeCurrent();
            var touchId = 0;
            UnityEngine.Vector3 screenPosition;
            AltUnityRunner._altUnityRunner.FindCameraThatSeesObject(target, out screenPosition);
            InputTestFixture.SetTouch(touchId,phase:UnityEngine.InputSystem.TouchPhase.Began,position:screenPosition,screen:Touchscreen);
            for (int i = 0; i < count; i++)
            {
                float time = 0;
                InputTestFixture.BeginTouch(touchId, screenPosition, screen: Touchscreen);
                yield return null;
                time += Time.fixedUnscaledDeltaTime;
                InputTestFixture.EndTouch(touchId, screenPosition, screen: Touchscreen);
                if (i != count - 1 && time < interval)
                    yield return new WaitForSecondsRealtime(interval - time);
            }
        }
        internal static IEnumerator TapCoordinatesCycle(UnityEngine.Vector2 screenPosition, int count, float interval)
        {
            Touchscreen.MakeCurrent();
            var touchId = 0;
            InputTestFixture.SetTouch(touchId,phase:UnityEngine.InputSystem.TouchPhase.Began,position:screenPosition,screen:Touchscreen);
            for (int i = 0; i < count; i++)
            {
                float time = 0;
                InputTestFixture.BeginTouch(touchId, screenPosition, screen: Touchscreen);
                yield return new WaitForSecondsRealtime(Time.fixedUnscaledDeltaTime);
                time += Time.fixedUnscaledDeltaTime;
                InputTestFixture.EndTouch(touchId, screenPosition, screen: Touchscreen);
                if (i != count - 1 && time < interval)
                    yield return new WaitForSecondsRealtime(interval - time);
            }
        }

        internal static IEnumerator ClickElementLifeCycle(GameObject target, int count, float interval)
        {
            Mouse.MakeCurrent();
            UnityEngine.Vector3 screenPosition;
            AltUnityRunner._altUnityRunner.FindCameraThatSeesObject(target, out screenPosition);
            InputTestFixture.Set(Mouse.position,new Vector2(screenPosition.x,screenPosition.y), queueEventOnly: true);
           
            for (int i = 0; i < count; i++)
            {
                float time = 0;
                InputTestFixture.Press(Mouse.leftButton, queueEventOnly: true);
                yield return new WaitForSecondsRealtime(Time.fixedUnscaledDeltaTime);
                time += Time.fixedUnscaledDeltaTime;
                InputTestFixture.Release(Mouse.leftButton, queueEventOnly: true);
                if (i != count - 1 && time < interval)
                    yield return new WaitForSecondsRealtime(interval - time);
            }
        }
        internal static IEnumerator ClickCoordinatesLifeCycle(UnityEngine.Vector2 screenPosition, int count, float interval)
        {
            Mouse.MakeCurrent();
            InputTestFixture.Set(Mouse.position, screenPosition);
            for (int i = 0; i < count; i++)
            {
                float time = 0;
                InputTestFixture.Press(Mouse.leftButton);
                yield return new WaitForSecondsRealtime(Time.fixedUnscaledDeltaTime);
                time += Time.fixedUnscaledDeltaTime;
                InputTestFixture.Release(Mouse.leftButton);
                if (i != count - 1 && time < interval)
                    yield return new WaitForSecondsRealtime(interval - time);
            }
        }

        internal static IEnumerator MultipointSwipeLifeCycle(UnityEngine.Vector2[] positions, float duration)
        {
            Touchscreen.MakeCurrent();
            var touchId =0;
            float oneTouchDuration = duration / (positions.Length-1);
            yield return new WaitForEndOfFrame();
            InputTestFixture.BeginTouch(touchId, positions[0], screen:Touchscreen);
            for (int i = 1; i < positions.Length; i++)
            {
                float timeExecuted = 0;
                Vector2 curentPosition = positions[i-1];
                var wholeDelta = positions[i] - curentPosition;
                var deltaPerSecond = wholeDelta / oneTouchDuration;
                Vector2 nextFramePosition = curentPosition;
                do
                {
                    timeExecuted += Time.deltaTime;
                    if(timeExecuted < oneTouchDuration)
                    {
                        nextFramePosition += deltaPerSecond*Time.unscaledDeltaTime;
                    }
                    else
                    {
                        nextFramePosition = positions[i];
                    }
                    Vector2 delta = nextFramePosition - curentPosition;

                    InputTestFixture.MoveTouch(touchId, nextFramePosition, delta, screen:Touchscreen);
                    yield return null;

                } while (timeExecuted <= oneTouchDuration);
            }
            InputTestFixture.EndTouch(touchId, positions[positions.Length-1], screen:Touchscreen);
        }
    
        internal static IEnumerator AccelerationLifeCycle(Vector3 accelerationValue, float duration)
        {
            float currentTime = 0;
            float frameTime = 0;// using this because of a bug with yield return which waits only every other iteration
            InputSystem.EnableDevice(Accelerometer);
            while (currentTime <= duration - Time.fixedUnscaledDeltaTime)
            {
                InputTestFixture.Set(Accelerometer.acceleration, accelerationValue * frameTime / duration, queueEventOnly: true);
                var initialTime = Time.fixedUnscaledTime;
                yield return null;
                var afterTime = Time.fixedUnscaledTime;
                frameTime = afterTime - initialTime;
                currentTime += frameTime;
            }
            InputTestFixture.Set(Accelerometer.acceleration, Vector3.zero);
            InputSystem.DisableDevice(Accelerometer);
        }
    }

}
#else
namespace Altom.AltUnityTester
{
    public class NewInputSystem
    {

    }
}
#endif