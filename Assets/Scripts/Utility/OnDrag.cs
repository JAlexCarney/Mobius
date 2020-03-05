using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnDrag : MonoBehaviour
{
    [System.Serializable]
    public struct Event
    {
        public string trigger;
        public UnityEvent action;
    }

    public Event[] Events;

    public void Update()
    {
        // Check if the left mouse button was raised
        if (Draggable.justReleased)
        {
            //Debug.Log("Just Released");
            if (Draggable.holding)
            {
                //Debug.Log("Holding");
                Debug.Log(Draggable.heldObj.GetComponent<Draggable>().dropPos);
                // Check if the mouse was clicked over a UI element
                if (CheckBounds(Draggable.heldObj.GetComponent<Draggable>().dropPos))
                {
                    //Debug.Log("Inside Bounds");
                    Draggable.justReleased = false;
                
                    foreach (Event e in Events)
                    {
                        //Debug.Log(Draggable.held);
                        if (e.trigger == Draggable.held)
                        {
                            Debug.Log("Triggered");
                            //Debug.Log(e.trigger);
                            e.action.Invoke();
                        }
                    }
                }
            }
        }
    }

    public bool CheckBounds(Vector3 touchPos)
    {
        Vector3 pos = transform.position;
        Vector3 delta = touchPos - pos;
        float width = GetComponent<RectTransform>().rect.width;
        float height = GetComponent<RectTransform>().rect.height;
        if (delta.x < width/2 && delta.x > -width/2 &&
            delta.y < height/2 && delta.y > -height/2)
        {
            return true;
        }
        return false;
    }
}
