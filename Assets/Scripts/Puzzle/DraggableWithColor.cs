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
    public DraggableWithColor[] set;


    private TopVisualFolllow movingVisual;
    private TopVisualFolllow movingVisualInverse;
    private Transform parent;
    private Vector3 offset;

    private DraggableWithColor swapingWith;

    private Touch touch;

    private static int idCounter = 0;
    public int id;
    public int winId;

    public Mono mono;

    private void Start()
    {
        label = gameObject.name;
        startPos = transform.position;
        movingVisual = GameObject.Find("MovingVisualCanvas").GetComponent<TopVisualFolllow>();
        parent = transform.parent;
        inverse = transform.GetChild(0).gameObject;
        offset = new Vector3(-100f, 100f, 0f);
        id = idCounter;
        winId = idCounter;
        idCounter++;
        swapingWith = null;
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
                transform.position = Input.mousePosition;// + offset;
                inverse.transform.position = Input.mousePosition;// + offset;
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
                transform.parent = parent;
                inverse.transform.parent = transform;
                mono.Check();
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
        Hold();
    }

    public void OnPointerUp(PointerEventData d)
    {
        dropPos = transform.position;
        justReleased = true;

        bool swapable = false;
        foreach (DraggableWithColor obj in set)
        {
            Debug.Log("Checking... " + obj.gameObject.name + " and " + gameObject.name);
            if (Util.CheckBounds(obj.gameObject, transform.position) && obj.gameObject != gameObject)
            {
                Debug.Log("Found Swaper");
                swapable = true;
                swapingWith = obj;
                break;
            }
        }

        if (swapable)
        {
            Invoke("Swap", 0.1f);
        }
        else
        {
            Invoke("Drop", 0.1f);
        }
    }

    public void Hold()
    {
        if (!holding && !isGoingBack)
        {
            isHeld = true;
            holding = true;
            held = label;
            heldObj = gameObject;

            movingVisualInverse = GameObject.Find("MovingVisualCanvasInverse").GetComponent<TopVisualFolllow>();
            transform.parent = movingVisualInverse.gameObject.transform;
            inverse.transform.parent = movingVisual.gameObject.transform;
        }
    }

    public void Drop()
    {
        isHeld = false;
        holding = false;
        held = "";
        heldObj = null;
        isGoingBack = true;
        swapingWith = null;
        justReleased = false;
    }

    public void Swap()
    {
        Debug.Log("Swap");

        isHeld = false;
        holding = false;
        held = "";
        heldObj = null;

        isGoingBack = true;
        swapingWith.isGoingBack = true;

        swapingWith.dropPos = swapingWith.transform.position;
        dropPos = transform.position;
        
        swapingWith.startPos = startPos;
        startPos = swapingWith.transform.position;

        justReleased = false;
        int temp = id;
        id = swapingWith.id;
        swapingWith.id = temp;
    }
}
