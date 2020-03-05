using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swappable : MonoBehaviour
{
    public int swapID;
    private TopVisualFolllow movingVisual = null;

    public void Start()
    {
        movingVisual = GameObject.Find("MovingVisualCanvas").GetComponent<TopVisualFolllow>();
    }

    public void Update()
    {
        // Check if the left mouse button was raised
        if (Draggable.justReleased)
        {
            if (Draggable.holding)
            {
                // Check if the mouse was clicked over a UI element
                if (CheckBounds(Draggable.heldObj.GetComponent<Draggable>().dropPos) && Draggable.heldObj != gameObject && Draggable.heldObj.GetComponent<Swappable>())
                {
                    Draggable.justReleased = false;
                
                    Draggable heldDrag = Draggable.heldObj.GetComponent<Draggable>();
                    Draggable thisDrag = GetComponent<Draggable>();
                    if (Draggable.heldObj.GetComponent<Swappable>().swapID == swapID)
                    {
                        Vector3 temp = heldDrag.startPos;
                        heldDrag.startPos = thisDrag.startPos;
                        thisDrag.startPos = temp;
                        heldDrag.dropPos = Draggable.heldObj.transform.position;
                        thisDrag.dropPos = transform.position;
                        thisDrag.isGoingBack = true;
                        heldDrag.isGoingBack = true;

                        Debug.Log(Draggable.heldObj + " <-> " + gameObject);
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
        if (delta.x < width / 2 && delta.x > -width / 2 &&
            delta.y < height / 2 && delta.y > -height / 2)
        {
            return true;
        }
        return false;
    }
}
