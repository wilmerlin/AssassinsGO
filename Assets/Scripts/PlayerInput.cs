using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerInput : MonoBehaviour
{
    // store horizontal input
    float m_h;
    public float H { get { return m_h; } }

    // store the vertical input 
    float m_v;
    public float V { get { return m_v; } }

    // global flag for enabling and disabling user input
    bool m_inputEnabled = false;
    public bool InputEnabled { get { return m_inputEnabled; } set { m_inputEnabled = value; } }

    Vector2 touchdownPos = Vector2.zero;



    public bool m_useDiagnostic = false;

    public TouchController touchController;

    // get keyboard input
    public void GetKeyInput()
    {
        // if input is enabled, just get the raw axis data from the Horizontal and Vertical virtual axes (defined in InputManager)
        if (m_inputEnabled)
        {
            m_h = Input.GetAxisRaw("Horizontal");
            m_v = Input.GetAxisRaw("Vertical");
        }
        // if input is disabled, ensure that extra key input does not cause unintended movement
        else
        {
            ClearInput();
        }
    }

    public void GetInput()
    {
#if UNITY_STANDALONE || UNITY_WEBPLAYER
        GetKeyInput();
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

#endif
    }



#if UNITY_STANDALONE || UNITY_WEBPLAYER


    //Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

    private void Awake()
    {
        if (touchController != null)
        {
            touchController.SwipeEnd += OnSwipeEnd;
        }
    }

    private void OnDestroy()
    {
        if (touchController != null)
        {
            touchController.SwipeEnd -= OnSwipeEnd;
        }
    }

    // event handler
    public void OnSwipeEnd(Vector2 inputVector)
    {
        if (!m_inputEnabled)
        {
            return;
        }

        // horizontal
        if (Mathf.Abs(inputVector.x) > Mathf.Abs(inputVector.y))
        {
            m_h = (inputVector.x >= 0f) ? 1f : -1f;
            m_v = 0f;

        }
        // vertical
        else
        {
            m_h = 0f;
            m_v = (inputVector.y >= 0f) ? 1f : -1f;

        }
    }


    public void ClearInput()
    {
        m_h = 0f;
        m_v = 0f;
    }

    //    public void GetInput()
    //    {

    //    //Check if Input has registered more than zero touches
    //    if (Input.touchCount > 0)
    //    {

    //        // first touch position
    //        Touch firstTouch = Input.touches[0];

    //        // if we're touching for the first time, make sure we store this position
    //        if (firstTouch.phase == TouchPhase.Began)
    //        {
    //            touchdownPos = firstTouch.position;
    //        }

    //        // if we have moved our finger since we touched down
    //        else if (firstTouch.phase == TouchPhase.Ended && touchdownPos.x >= 0)
    //        {
    //            // store this ending position
    //            Vector2 endTouchPos = firstTouch.position;

    //            // x difference
    //            float x = endTouchPos.x - touchdownPos.x;

    //            // y difference
    //            float y = endTouchPos.y - touchdownPos.y;

    //            // set this to -1 so we don't repeat
    //            touchdownPos.x = -1;

    //            // set the horizontal or verticle depending on which one is bigger
    //            if (Mathf.Abs(x) > Mathf.Abs(y))
    //            {
    //                m_h = x > 0 ? 1 : -1;
    //                m_v = 0;
    //            }
    //            else
    //            {
    //                m_v = y > 0 ? 1 : -1;
    //                m_h = 0;
    //            }

    //        }
    //    }

    //}
    //void Update () 

    //if (Input.touchCount > 0)
    //{
    //    Touch touch = Input.touches[0];

    //    if (touch.phase == TouchPhase.Began)
    //    {
    //        m_touchMovement = Vector2.zero;
    //        Diagnostic("","");

    //    }
    //    else if (touch.phase == TouchPhase.Ended)
    //    {
    //        if (m_touchMovement.magnitude > m_minSwipeDistance)
    //        {
    //            OnSwipeEnd();
    //            Diagnostic("Swipe detected",m_touchMovement.ToString() + " " + SwipeDiagnostic(m_touchMovement));
    //        }

    //    }


    //}
#endif //End of mobile platform dependendent compilation section started above with #elif


}

