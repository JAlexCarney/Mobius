
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swappable : MonoBehaviour
{
    public int swapID;
    private Animator anim;
    private TopVisualFolllow movingVisual = null;

    public void Start()
    {
        movingVisual = GameObject.Find("MovingVisualCanvas").GetComponent<TopVisualFolllow>();
        anim = GetComponentInChildren<Animator>();
        Debug.Log(anim.name);
    }

    public void Update()
    {

        if (Draggable.holding)
        {
            // Check if the mouse was clicked over a UI element
            if (Util.CheckBounds(this.gameObject, Draggable.heldObj.transform.position) && Draggable.heldObj != gameObject && Draggable.heldObj.GetComponent<Swappable>())
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

                //telegraph SHAKE SHAKE
                else
                {
                    anim.SetBool("Clicked", true);
                    Invoke("SetClickedFalse", 1f);
                }
            }
            else //if its shaking, STOP IT
            {
                anim.SetBool("Clicked", false);
            }
        }
    }

    public void SetClickedFalse()
    {
        anim.SetBool("Clicked", false);
    }
}