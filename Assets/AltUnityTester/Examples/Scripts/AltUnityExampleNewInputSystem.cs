using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;




public class AltUnityExampleNewInputSystem : MonoBehaviour
{
    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    #endregion

    int jumpCounter = 0;
    public bool wasClicked = false;

    public Text counterText;
    public Text actionText;
    public Text swipeText;
    public Rigidbody capsuleRigidBody;
    public static Mouse Mouse;
    public static Touchscreen Touchscreen;
    private SimpleControls simpleControls;
    private float minimumDistance = .2f;
    private float maximumTime = 1f;
    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;
    
    private void Start() 
    {
        // simpleControls.Capsule.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        // simpleControls.Capsule.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);

    }
    // private void OnEnable()
    // {
    //     OnStartTouch += SwipeStart;
    //     OnEndTouch +=SwipeEnd;
    // }
    // private void OnDisable()
    // {
    //     OnStartTouch -= SwipeStart;
    //     OnEndTouch -=SwipeEnd;
    // }
    public void Jump(InputAction.CallbackContext context)
    {
        if(context.action.name == "Jump" && context.phase is InputActionPhase.Performed)
        {
            if (context.interaction is TapInteraction)
            {
                JumpCapsule();
                actionText.text = "Capsule was tapped!";
            }
            else
            {
                if(Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit))
                {
                    Debug.LogFormat("You hit [{0}]", hit.collider.gameObject.name);
                    if(hit.collider.gameObject.name == "Capsule")
                    {
                        JumpCapsule();
                        actionText.text = "Capsule was clicked!";
                    }
                }
            
            }
        }
    }
   
   public void JumpCapsule()
   {
        jumpCounter++;
        counterText.text = jumpCounter.ToString();
        capsuleRigidBody.GetComponent<Rigidbody>().AddForce(Vector3.up *5f, ForceMode.Impulse);
   }
    // private void SwipeStart(Vector2 position, float time) 
    // {
    //     startPosition = position;
    //     startTime = time;
    //     DetectSwipe()
    // }
    
   public void PrimaryContact(InputAction.CallbackContext context)
   {
       if(Vector3.Distance(startPosition,endPosition)>=minimumDistance && (endTime-startTime)<=maximumTime)
       {
           Debug.Log("Swipe detected");
       }
   }

   public void StartTouchPrimary(InputAction.CallbackContext context)
   {
       if(OnStartTouch != null)
       {
       OnStartTouch(ScreenToWorld(Camera.main,simpleControls.Capsule.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
        OnStartTouch += SwipeStart;
 
       } 

   }

   public void EndTouchPrimary(InputAction.CallbackContext context)
   {
       if(OnEndTouch != null) 
       {
       OnEndTouch(ScreenToWorld(Camera.main,simpleControls.Capsule.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
            OnEndTouch +=SwipeEnd;

       }

   }

   public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
   {
       position.z = camera.nearClipPlane;
       return camera.ScreenToWorldPoint(position);
   }

    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
        DetectSwipe();
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if(Vector3.Distance(startPosition,endPosition)>=minimumDistance && (endTime-startTime)<=maximumTime)
        {
            Debug.DrawLine(startPosition,endPosition,Color.red);
        }

    }

  
}