using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControls : MonoBehaviour
{
    Vector2 swipeStart;
    Vector2 swipeEnd;
    float minimumDistance = 10;
    public enum SwipeDirection { Up, Down, Left, Right };

    public static event System.Action<SwipeDirection> OnSwipe = delegate{};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Touch touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Began)
            {
                swipeStart = touch.position;
                Debug.Log("Touch Start");
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                swipeEnd = touch.position;
                Debug.Log("Touch End");
                ProcessSwipe();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            swipeStart = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            swipeEnd = Input.mousePosition;
            ProcessSwipe();
        }

        //Keyboard Controls
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnSwipe(SwipeDirection.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnSwipe(SwipeDirection.Down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnSwipe(SwipeDirection.Left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnSwipe(SwipeDirection.Right);
        }
    }

    void ProcessSwipe()
    {
        Debug.Log("Processing Swipe...");
        float distance = Vector2.Distance(swipeStart, swipeEnd);
        if (distance > minimumDistance)
        {
            if (IsVerticalSwipe())
            {
                if(swipeEnd.y > swipeStart.y)
                {
                    OnSwipe(SwipeDirection.Up);
                }
                else if(swipeEnd.y < swipeStart.y)
                {
                    OnSwipe(SwipeDirection.Down);
                }
            }
            else
            {
                if(swipeEnd.x > swipeStart.x)
                {
                    OnSwipe(SwipeDirection.Right);
                }
                else if(swipeEnd.x < swipeStart.x)
                {
                    OnSwipe(SwipeDirection.Left);
                }
            }
        }
    }

    bool IsVerticalSwipe()
    {
        float virtical = Mathf.Abs(swipeEnd.y - swipeStart.y);
        float horizontal = Mathf.Abs(swipeEnd.x - swipeStart.x);
        if(virtical > horizontal)
        {
            return true;
        }
        
        return false; 
    }
}
