using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    public bool isWire = false;

    private TopVisualFolllow movingVisual;
    private Transform parent;

    private Touch touch;

    private void Start()
    {
        label = gameObject.name;
        startPos = transform.position;
        movingVisual = GameObject.Find("MovingVisualCanvas").GetComponent<TopVisualFolllow>();
        parent = transform.parent;
    }

    // FixedUpdate is called once per each 1/50th of a second
    void FixedUpdate()
    {
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
        }
        if (isHeld)
        {
            if (isWire)
            {
                if (Input.touchCount == 0)
                {
                    Debug.Log("Checking : " + GameObject.Find("WireMoveBox"));
                    if (Util.CheckBounds(GameObject.Find("WireMoveBox"), Input.mousePosition))
                    {
                        transform.position = Input.mousePosition;
                    }
                    else 
                    {
                        transform.position = new Vector3(Input.mousePosition.x, transform.position.y, transform.position.z);
                    }
                }
                else
                {
                    if (Util.CheckBounds(GameObject.Find("WireMoveBox"), touch.position))
                    {
                        transform.position = touch.position;
                    }
                    else
                    {
                        transform.position = new Vector3(touch.position.x, transform.position.y, transform.position.z);
                    }
                }
            }
            else 
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
        else if (isGoingBack)
        {
            if (counter == 30)
            {
                counter = 0;
                isGoingBack = false;
                //movingVisual.StopTracking(gameObject);
                transform.parent = parent;
            }
            else
            {
                counter++;
                transform.position = Vector3.Lerp(dropPos, startPos, 1 + Mathf.Pow((float)counter/30f - 1,3));

                //transform.position = Vector3.Lerp(dropPos, startPos, easingFunct((float)counter/30));

            }
        }
    }

    //https://gist.github.com/Fonserbc/3d31a25e87fdaa541ddf from this god bless u sweet soul
    public static float easingFunct(float k)
    {
        return 1f - Mathf.Cos(k * Mathf.PI / 2f);
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
        Invoke("Drop", .01f);
    }

    public void Hold()
    {
        if (!holding && !isGoingBack)
        {
            isHeld = true;
            holding = true;
            held = label;
            heldObj = gameObject;
            if (!isWire)
            {
                transform.parent = movingVisual.gameObject.transform;
            }
        }
    }

    public void Drop()
    {
        isHeld = false;
        holding = false;
        held = "";
        heldObj = null;
        isGoingBack = true;
        justReleased = false;
    }
}
