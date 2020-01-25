using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent myEvent;
    public string trigger;

    public void OnPointerDown(PointerEventData d)
    {

    }

    public void OnPointerUp(PointerEventData d)
    {
        Debug.Log("Up");
        if (Draggable.holding)
        {
            Debug.Log("Holding");
            if (Draggable.held == trigger)
            {
                Debug.Log("triggered");
                Debug.Log(trigger);
                myEvent.Invoke();
            }
        }
    }
}
