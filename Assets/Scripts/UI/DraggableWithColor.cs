using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableWithColor : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isHeld = false;
    public bool isGoingBack = false;
    private int counter = 0;
    public Vector3 startPos;
    public Vector3 dropPos;
    public string label;
    public static bool justReleased = false;
    static public bool holding = false;
    static public string held = "";
    static public GameObject heldObj = null;
    public GameObject inverse;

    private TopVisualFolllow movingVisual;
    private TopVisualFolllow movingVisualInverse;
    private Transform parent;
    private Vector3 offset;

    private Touch touch;

    private void Start()
    {
        label = gameObject.name;
        startPos = transform.position;
        movingVisual = GameObject.Find("MovingVisualCanvas").GetComponent<TopVisualFolllow>();
        movingVisualInverse = GameObject.Find("MovingVisualCanvasInverse").GetComponent<TopVisualFolllow>();
        parent = transform.parent;
        offset = new Vector3(-100f, 100f, 0f);
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
                transform.position = Input.mousePosition + offset;
                inverse.transform.position = Input.mousePosition + offset;
            }
            else
            {
                transform.position = touch.position + (Vector2)offset;
                inverse.transform.position = touch.position + (Vector2)offset;
            }
        }
        else if (isGoingBack)
        {
            if (counter == 30)
            {
                counter = 0;
                isGoingBack = false;
                //movingVisual.StopTracking(gameObject);
                inverse.transform.parent = parent;
                transform.parent = parent;
            }
            else
            {
                counter++;
                this.transform.position = Vector3.Lerp(dropPos, startPos, (float)counter / 30f);
                inverse.transform.position = Vector3.Lerp(dropPos, startPos, (float)counter / 30f);
            }
        }
    }

    private void ReturnToStartPosition()
    {
        transform.position = startPos;
    }

    public void OnPointerDown(PointerEventData d)
    {
        Debug.Log("Clicked");
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
            
            // alter scale
            this.transform.localScale = new Vector3(2.0f, 2.0f, 1.0f);
            this.transform.eulerAngles = new Vector3(0.0f, 0.0f, 30.0f);
            // alter inverse scale
            inverse.transform.localScale = new Vector3(2.0f, 2.0f, 1.0f);
            inverse.transform.eulerAngles = new Vector3(0.0f, 0.0f, 30.0f);

            //GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
            transform.parent = movingVisual.gameObject.transform;
            inverse.transform.parent = movingVisualInverse.gameObject.transform;
        }
    }

    public void Drop()
    {
        isHeld = false;
        holding = false;
        held = "";
        heldObj = null;
        isGoingBack = true;

        // return scale to normal
        this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        // return inverse scale to normal
        inverse.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        inverse.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

        //GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
        justReleased = false;
    }
}
