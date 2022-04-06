using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using Altom.AltUnityTester;

public class NewInputSystem : MonoBehaviour
{
    public static InputTestFixture InputTestFixture = new InputTestFixture();
    public static NewInputSystem Instance;
    public static Keyboard Keyboard;
    public static Mouse Mouse;
    public static Gamepad Gamepad;
    public static Touchscreen Touchscreen;
    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        InputTestFixture = new InputTestFixture();
        InputTestFixture.Setup();
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

    }
    internal static IEnumerator ScrollLifeCircle(float speed, float duration)
    {

        float currentTime = 0;
        float frameTime = 0;// using this because of a bug with yield return which waits only every other iteration
        while (currentTime <= duration - Time.fixedUnscaledDeltaTime)
        {
            InputTestFixture.Move(Mouse.current.scroll, new Vector2(speed * frameTime / duration, speed * frameTime / duration));
            var initialTime = Time.fixedUnscaledTime;
            yield return null;
            var afterTime = Time.fixedUnscaledTime;
            frameTime = afterTime - initialTime;
            currentTime += frameTime;
        }
        InputTestFixture.Set(Mouse.current.scroll, new Vector2(0, 0));
    }

    internal static IEnumerator MoveMouseCycle(UnityEngine.Vector2 location, float duration)
    {
        float time = 0;
        Mouse.MakeCurrent();
        var mousePosition = Mouse.current.position;
        var distance = location - new UnityEngine.Vector2(mousePosition.x.ReadValue(),mousePosition.y.ReadValue());
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

            InputTestFixture.Move(Mouse.current.position,delta);
            yield return null;
            time+=UnityEngine.Time.unscaledDeltaTime;
        } while(time<duration);
    }

    
    internal static IEnumerator ClickElementLifeCycle(GameObject target, int count, float interval)
    {
        Mouse.MakeCurrent();
        UnityEngine.Vector3 screenPosition;
        AltUnityRunner._altUnityRunner.FindCameraThatSeesObject(target, out screenPosition);
        InputTestFixture.Set(Mouse.current.position,screenPosition);
        for(int i=0;i<count;i++)        
        {
            float time = 0;
            InputTestFixture.Click(Mouse.leftButton);
            yield return new WaitForSecondsRealtime(Time.fixedUnscaledDeltaTime);
            time += Time.fixedUnscaledDeltaTime;
            if (i != count - 1 && time < interval)
                yield return new WaitForSecondsRealtime(interval-time);
        }
    }

    internal static IEnumerator ClickCoordinatesLifeCycle(UnityEngine.Vector2 screenPosition,int count, float interval)
    {
        Mouse.MakeCurrent();
        InputTestFixture.Set(Mouse.current.position,screenPosition);
        for( int i=0; i<count; i++)
        {
            float time = 0;
            InputTestFixture.Click(Mouse.leftButton);
            yield return new WaitForSecondsRealtime(Time.fixedUnscaledDeltaTime);
            time += Time.fixedUnscaledDeltaTime;
            if (i != count - 1 && time < interval)
                yield return new WaitForSecondsRealtime(interval-time);
        }
    }

    internal static IEnumerator MultipointSwipeLifeCycle(UnityEngine.Vector2[] positions, float duration)
    {
        Touchscreen.MakeCurrent();
        // var touch = new UnityEngine.InputSystem.Controls.TouchControl
        // {
        //     phase = UnityEngine.InputSystem.TouchPhase.Began,
        //     position = positions[0]
        // };
        var touch = new UnityEngine.Touch
        {
            phase = UnityEngine.TouchPhase.Began,
            position = positions[0]
        };
        var touchId = 0;
        yield return null;
        // InputTestFixture.BeginTouch(touchId,positions[0]);
        var oneInputDuration = duration / (positions.Length - 1);
        UnityEngine.Vector2 touchPosition = positions[0]; 
        for(var i = 1; i< positions.Length;i++)
        {
            var wholeDelta = positions[i] - positions[0];
            var deltaPerSecond = wholeDelta / oneInputDuration;
            float time = 0;
            do
            {
                UnityEngine.Vector2 previousPosition = touchPosition;
                if (time + UnityEngine.Time.unscaledDeltaTime < oneInputDuration)
                {
                    touchPosition += deltaPerSecond * UnityEngine.Time.unscaledDeltaTime;
                }
                else
                {
                    touchPosition = positions[i];
                }
                time += UnityEngine.Time.unscaledDeltaTime;
                InputTestFixture.BeginTouch(touchId,touchPosition);
                yield return null;
            } while(time <= oneInputDuration);
        }
        yield return null;
        InputTestFixture.EndTouch(touchId,positions[positions.Length - 1]);
        // yield return null;
    }

}



