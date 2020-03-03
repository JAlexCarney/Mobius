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

    private Transform bottomLeftCorner;
    private Transform topRightCorner; 

    private Transform parent;

    private Touch touch;

    private void Start()
    {
        label = gameObject.name;
        startPos = transform.position;
        parent = transform.parent;

        //get text children of the journal canvas
        //unity can't find inactive children using Find() :(
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            string name = child.gameObject.name;
            if (child != null && name.Equals("TopRight"))
            {
                topRightCorner = child;
            }
            else if (child != null && name.Equals("BottomLeft"))
            {
                bottomLeftCorner = child;
            }
        }
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
                transform.position = new Vector2(Clamp(Input.mousePosition.x, bottomLeftCorner.position.x, topRightCorner.position.x),
                    Clamp(Input.mousePosition.y, bottomLeftCorner.position.y, topRightCorner.position.y));

                //code graveyard below, enter at ur own risk ooo00OOOOoooooooOOoo00000ooooo

                //    Input.mousePosition;

                //Vector2 pageSize = parent.GetComponent<RectTransform>().sizeDelta;
                //float width = pageSize.x;
                //float height = pageSize.y;

                //float leftBound = parent.position.x;
                //if (transform.position.x < -(width/2))
                //    transform.position = new Vector2(-width/2, transform.position.y);

                //float upperBound = parent.position.y + (height / 2); 
                //if (transform.position.y > ( parent.position.y + height/2))
                //{
                //    transform.position = new Vector2(transform.position.x, parent.position.y);
                //    Debug.Log("AAAAAAAAAAAAAAAAAAA");
                //}
                //Debug.Log(-width/2 + " JDFIOEJFIOE " + parent.position.y+" " + height/2 + " almond: " + transform.position.x + "," + transform.position.y);

                //float rightBounds = parent.position.x + parent.GetComponent<RectTransform>().sizeDelta.x;
                //if (transform.position.x > rightBounds)
                //    transform.position = new Vector2(rightBounds, transform.position.y);
                //float lowerBounds = parent.position.y - parent.GetComponent<RectTransform>().sizeDelta.y; 
                //if (transform.position.y < lowerBounds)
                //    transform.position = new Vector2(transform.position.x, lowerBounds);
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

    //ty https://stackoverflow.com/questions/3176602/how-to-force-a-number-to-be-in-a-range-in-c/3176617
    public static float Clamp(float value, float min, float max)
    {
        return (value < min) ? min : (value > max) ? max : value;
    }

}
