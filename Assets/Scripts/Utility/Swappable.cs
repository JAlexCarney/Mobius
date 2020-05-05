
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swappable : MonoBehaviour
{
    public int swapID;
    private Animator anim;
    private TopVisualFolllow movingVisual = null;
    private SoundManager sm;
    private bool shaking = false;

    public void Start()
    {
        movingVisual = GameObject.Find("MovingVisualCanvas").GetComponent<TopVisualFolllow>();
        anim = GetComponentInChildren<Animator>();
        Debug.Log(anim.name);
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
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

                    sm.Play("placeFlask");

                    Vector3 temp = heldDrag.startPos;
                    heldDrag.startPos = thisDrag.startPos;
                    thisDrag.startPos = temp;
                    heldDrag.dropPos = Draggable.heldObj.transform.position;
                    thisDrag.dropPos = transform.position;
                    thisDrag.isGoingBack = true;
                    heldDrag.isGoingBack = true;
                }

                //telegraph SHAKE SHAKE
                else if (shaking == false)
                {
                    anim.SetBool("Clicked", true);
                    sm.Play("flaskShake");
                    shaking = true;
                    Invoke("SetClickedFalse", 1f);
                }
            }
            else //if its shaking, STOP IT
            {
                anim.SetBool("Clicked", false);
                shaking = false;
            }
        }
    }

    public void SetClickedFalse()
    {
        anim.SetBool("Clicked", false);
    }
}