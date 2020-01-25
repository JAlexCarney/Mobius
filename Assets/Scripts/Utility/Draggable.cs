using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isHeld = false;
    private Vector3 startPos;
    public string label;
    static public bool holding = false;
    static public string held = "";

    private void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeld)
        {
            transform.position = Input.mousePosition;
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
        Drop();
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
        ReturnToStartPosition();
    }
}
