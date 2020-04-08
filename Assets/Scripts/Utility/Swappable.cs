
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
        
            if (Draggable.holding)
            {
                // Check if the mouse was clicked over a UI element
                if (CheckBounds(Draggable.heldObj) && Draggable.heldObj != gameObject && Draggable.heldObj.GetComponent<Swappable>())
                {

                    Draggable heldDrag = Draggable.heldObj.GetComponent<Draggable>();
                    Draggable thisDrag = GetComponent<Draggable>();

                    if (Draggable.heldObj.GetComponent<Swappable>().swapID == swapID && Draggable.justReleased)
                    {
                        Draggable.justReleased = false;

                        Vector3 temp = heldDrag.startPos;
                        heldDrag.startPos = thisDrag.startPos;
                        thisDrag.startPos = temp;
                        heldDrag.dropPos = Draggable.heldObj.transform.position;
                        thisDrag.dropPos = transform.position;
                        thisDrag.isGoingBack = true;
                        heldDrag.isGoingBack = true;
                    }

                    else
                    {
                        Debug.Log("shake");
                    }
                }


            }
    }

    //this is collision
    //THIS ALWAYS RETURNS TRUE!
    public bool CheckBounds(GameObject heldObj)
    {
        Vector3 pos = transform.position;
        Vector3 heldObjPos = heldObj.transform.position;

        //Vector3 delta = touchPos - pos;
        float width = GetComponent<RectTransform>().sizeDelta.x;
        float height = GetComponent<RectTransform>().sizeDelta.y;

        //this ALWAYS RETURSN TRUE bc both inputs can be same obj
        //RECT OVERLAPS IS INCORRECT !!!!
        //TO DO: how to zoom out game screen ? to see all of the rectangles
        //how to position rects correctly in game space (they are all in top left corner)
        if (Util.RectOverlapsDraggable(heldObj.GetComponent<RectTransform>(), GetComponent<RectTransform>()))
        {
            return true;
        }
        return false;
    }

    //void OnGUI()
    //{
    //    RectTransform rectTrans1 = this.GetComponent<RectTransform>();
    //    GUI.Box(new Rect(rectTrans1.localPosition.x + 725, -rectTrans1.localPosition.y + 350, rectTrans1.rect.width, rectTrans1.rect.height), this.name);
    //    Debug.Log(this.name + " " + rectTrans1.localPosition.y + 350 + " " + rectTrans1.transform.position);
    //    GUI.Box(new Rect(0, 0, rectTrans1.rect.width, rectTrans1.rect.height), "ORIGIN POINT");
    //    GUI.Box(new Rect(0, 100, rectTrans1.rect.width, rectTrans1.rect.height), "PLUS 100 Y");
    //}

}