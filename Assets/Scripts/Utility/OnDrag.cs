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
        // Check if the left mouse button was clicked
        if (Input.GetMouseButtonUp(0))
        {
            // Check if the mouse was clicked over a UI element
            if (EventSystem.current.IsPointerOverGameObject() && Draggable.holding)
            {
                Debug.Log("invoked");
                foreach (Event e in Events)
                {
                    if (e.trigger == Draggable.held)
                    {
                        Debug.Log("invoked");
                        Debug.Log(e.trigger);
                        e.action.Invoke();
                    }
                }
            }
        }
    }
}
