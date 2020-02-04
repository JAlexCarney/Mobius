using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isHeld = false;
    public bool isGoingBack = false;
    private int counter = 0;
    public Vector3 startPos;
    public Vector3 dropPos;
    public string label;
    public static bool justReleased = false;
    public int swapID;
    static public bool holding = false;
    static public string held = "";
    static public GameObject heldObj = null;

    private Touch touch;

    private void Start()
    {
        label = gameObject.name;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
        }
        if (isHeld)
        {
            if (Input.touchCount == 0)
            {
                transform.position = Input.mousePosition;
            }
            else
            {
                transform.position = touch.position;
            }
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
        dropPos = transform.position;
        justReleased = true;
        Invoke("Drop", 0.1f);
    }

    public void Hold()
    {
        if (!holding && !isGoingBack)
        {
            isHeld = true;
            holding = true;
            held = label;
            heldObj = gameObject;
            this.transform.localScale = new Vector3(2.0f, 2.0f, 1.0f);
            this.transform.eulerAngles = new Vector3(0.0f, 0.0f, 30.0f);
        }
    }

    public void Drop()
    {
        isHeld = false;
        holding = false;
        held = "";
        heldObj = null;
        isGoingBack = true;
        this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        justReleased = false;
    }
}
