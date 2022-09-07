
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AltUnityInputFieldRaisedEvents : MonoBehaviour
{
    private bool onValueChangedInvoked = false;
    private bool onSubmitInvoked = false;
    private bool onEndEditInvoked = false;

    public void OnValueChanged()
    {
        onValueChangedInvoked = true;
    }

    public void OnSubmit()
    {
        onSubmitInvoked = true;
    }

    public void OnEndEdit()
    {
        onEndEditInvoked = true;
    }
}