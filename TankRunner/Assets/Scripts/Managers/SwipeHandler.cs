using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using com;

public class TouchData
{
    public Vector2 pos;
    public float timestamp;
}

public class SwipeHandler : UIBehaviour, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public float durationMax = 0.8f;
    public float distanceMin = 20f;
    public float swipeDirectionAbsoluteTangentMin = 4;

    private bool _isTouchDown;
    private TouchData _data;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown" + eventData.position);
        _isTouchDown = true;
        _data = new TouchData();
        _data.timestamp = Time.time;
        _data.pos = eventData.position;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit" + eventData.position);
        _isTouchDown = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp" + eventData.position);
        if (_isTouchDown)
        {
            OnTouchReleased(eventData.position);
        }
        _isTouchDown = false;
    }

    void OnTouchReleased(Vector2 pos)
    {
        var dt = Time.time - _data.timestamp;
        if (dt > durationMax)
        {
            Debug.Log("swipe too slow!");
            return;
        }

        var deltaPos = pos - _data.pos;
        var distance = deltaPos.magnitude;
        if (distance < distanceMin)
        {
            Debug.Log("swipe too short!");
            return;
        }

        if (Mathf.Abs(deltaPos.x) > Mathf.Abs(deltaPos.y))
        {
            //横向划动
            if (deltaPos.y != 0 && Mathf.Abs(deltaPos.x) / Mathf.Abs(deltaPos.y) < swipeDirectionAbsoluteTangentMin)
            {
                Debug.Log("swipe not enough horizontal!");
                return;
            }

            if (deltaPos.x > 0)
            {
                Debug.Log("swipe to right!");
                GameSystem.instance.tankMovement.MoveRight();
            }
            else
            {
                Debug.Log("swipe to left!");
                GameSystem.instance.tankMovement.MoveLeft();
            }
        }
        else
        {
            //竖向划动
            if (deltaPos.x != 0 && Mathf.Abs(deltaPos.y) / Mathf.Abs(deltaPos.x) < swipeDirectionAbsoluteTangentMin)
            {
                Debug.Log("swipe not enough vertical!");
                return;
            }

            if (deltaPos.y > 0)
            {
                Debug.Log("swipe to up!");
                GameSystem.instance.tankShooting.Fire();
            }
            else
            {
                Debug.Log("swipe to down!");
            }
        }
    }
}