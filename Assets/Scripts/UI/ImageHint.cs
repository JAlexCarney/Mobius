using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageHint : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isHeld = false;
    public Vector3 startPos;
    public Vector3 dropPos;
    public string label;
    public static bool justReleased = false;
    public int swapID;
    static public bool holding = false;
    static public string held = "";
    static public GameObject heldObj = null;

    private Transform parent;

    private Touch touch;

    private void Start()
    {
        label = gameObject.name;
        startPos = transform.position;
        parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
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
        if (!holding)
        {
            isHeld = true;
            holding = true;
            held = label;
            heldObj = gameObject;
        }
    }

    public void Drop()
    {
        isHeld = false;
        holding = false;
        held = "";
        heldObj = null;
        justReleased = false;
    }
}
