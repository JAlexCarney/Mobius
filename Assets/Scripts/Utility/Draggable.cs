using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isHeld = false;
    private bool isGoingBack = false;
    private int counter = 0;
    private Vector3 startPos;
    private Vector3 dropPos;
    public string label;
    static public bool holding = false;
    static public string held = "";

    private void Start()
    {
        label = gameObject.name;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeld)
        {
            transform.position = Input.mousePosition;
        }
        else if (isGoingBack)
        {
            if (counter == 30)
            {
                counter = 0;
                isGoingBack = false;
            }
            else
            {
                counter++;
                transform.position = Vector3.Lerp(dropPos, startPos, (float)counter/30f);
            }
        }
    }

    private void ReturnToStartPosition()
    {
        transform.position = startPos;
    }

    public void OnPointerDown(PointerEventData d)
    {
        //Debug.Log("Clicked");
        Hold();
    }

    public void OnPointerUp(PointerEventData d)
    {
        //Debug.Log("released");
        Invoke("Drop", 0.1f);
    }

    public void Hold()
    {
        if (!holding)
        {
            isHeld = true;
            holding = true;
            held = label;
        }
    }

    public void Drop()
    {
        isHeld = false;
        holding = false;
        held = "";
        dropPos = transform.position;
        isGoingBack = true;
    }
}
