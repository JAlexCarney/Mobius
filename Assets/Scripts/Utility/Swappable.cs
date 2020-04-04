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
            Debug.Log("just released");
            if (Draggable.holding)
            {
                Debug.Log("holding");
                // Check if the mouse was clicked over a UI element
                if (CheckBounds(Draggable.heldObj) && Draggable.heldObj != gameObject && Draggable.heldObj.GetComponent<Swappable>())
                {
                    Draggable.justReleased = false;
                
                    Draggable heldDrag = Draggable.heldObj.GetComponent<Draggable>();
                    Draggable thisDrag = GetComponent<Draggable>();

                    Debug.Log(Draggable.heldObj.GetComponent<Swappable>().swapID + " SAP ID " + swapID);
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

    //this is collision
    public bool CheckBounds(GameObject heldObj)
    {
        Vector3 pos = transform.position;
        Vector3 heldObjPos = heldObj.transform.position;

        //Vector3 delta = touchPos - pos;
        float width = GetComponent<RectTransform>().sizeDelta.x;
        float height = GetComponent<RectTransform>().sizeDelta.y;

        if (Util.RectOverlaps(GetComponent<RectTransform>(), heldObj.GetComponent<RectTransform>()))
        {
            Debug.Log("yaaas queen");
            return true;
        }
        return false;
    }
}
