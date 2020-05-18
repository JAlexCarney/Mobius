using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PrismElement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public string type;
    public string orientation;
    public string colorToCast;
    public List<string> colorsToCast;
    public List<string> directionsToCast; 
    public PrismReference prismReference;
    public int row;
    public int column;

    private int thresholdFrames;
    private bool isDown;

    public int currentRotation;
    public RectTransform mirrorRect;
    public int index;
    private bool isRotating;

    static GameObject held = null;
    private bool isGoingBack = false;
    private int counter = 0;
    private TopVisualFolllow movingVisual;
    private Transform parent;
    private Vector3 origin;
    private Vector3 droppedPos;
    private Touch touch;

    // Start is called before the first frame update
    void Start()
    {
        prismReference = this.GetComponentInParent<PrismReference>();

        colorsToCast = new List<string>();
        directionsToCast = new List<string>();

        if (this.orientation == "NW")
        {
            this.currentRotation = 45;
        }

        else if (this.orientation == "SW")
        {
            this.currentRotation = 135;
        }

        else if (this.orientation == "SE")
        {
            this.currentRotation = 225;
        }

        else if (this.orientation == "NE")
        {
            this.currentRotation = 325;
        }

        movingVisual = GameObject.Find("MovingVisualCanvas").GetComponent<TopVisualFolllow>();
        parent = transform.parent;
        origin = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Determine clicking vs. dragging.
        if(isDown)
        {
            thresholdFrames++;
        }

        // Rotate the Mirror.
        if (isRotating)
        {
            currentRotation += 5;

            int newZ = currentRotation % 360;
            //Debug.Log("NewZ: " + newZ);

            Vector3 updatedRotation = new Vector3(0, 0, newZ);
            this.GetComponent<RectTransform>().localEulerAngles = updatedRotation;

            if (newZ >= 44 && newZ <= 46)
            {
                this.orientation = "NW";
                //Debug.Log(this.orientation + " " + currentRotation);
                prismReference.BoardUpdate();
                isRotating = false;
            }

            else if (newZ >= 134 && newZ <= 136)
            {
                this.orientation = "SW";
                //Debug.Log(this.orientation + " " + currentRotation);
                prismReference.BoardUpdate();
                isRotating = false;
            }

            else if (newZ >= 224 && newZ <= 226)
            {
                this.orientation = "SE";
                //Debug.Log(this.orientation + " " + currentRotation);
                prismReference.BoardUpdate();
                isRotating = false;
            }

            else if (newZ >= 314 && newZ <= 316)
            {
                this.orientation = "NE";
                //Debug.Log(this.orientation + " " + currentRotation);
                prismReference.BoardUpdate();
                isRotating = false;
            }
        }

        // Holding and moving mirror.
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
        }

        if (held == this.gameObject && thresholdFrames >= 15)
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
                prismReference.BoardUpdate();
                transform.parent = parent;
                //Debug.Log(row + " " + column);
            }
            else
            {
                counter++;
                transform.position = Vector3.Lerp(droppedPos, origin, (float)counter / 30f);
            }
        }
    }

    public void OnPointerDown(PointerEventData d)
    {
        if (held == null) {

            if (!isRotating && !isGoingBack)
            {
                thresholdFrames = 0;
                isDown = true;

                if (this.type == "mirror")
                {
                    held = this.gameObject;
                    origin = held.transform.position;
                    held.transform.parent = movingVisual.gameObject.transform;
                }
            }
        }
        
    }

    public void OnPointerUp(PointerEventData d)
    {
        if (!isRotating && !isGoingBack && held == this.gameObject)
        {
            isDown = false;

            if (thresholdFrames < 15)
            {
                if (this.type == "mirror")
                {
                    RotateMirror();
                }
            }

            else if (thresholdFrames >= 15)
            {
                PrismElement element = prismReference.Swappable(this, this.transform.position);

                if (element.row != -1)
                {
                    SwapMirror(element);
                }
                else
                {
                    DropMirror();
                }
            }
        }
    }

    public void RotateMirror()
    {
        isRotating = true;
        held = null;
    }

    public void DropMirror()
    {
        isGoingBack = true;
        droppedPos = this.transform.position;
        //Debug.Log("Mirror was dropped.");
        held = null;
    }

    public void SwapMirror(PrismElement toSwap)
    {
        droppedPos = this.transform.position;
        toSwap.droppedPos = toSwap.transform.position;

        Vector3 tempPos = origin;
        int tempRow = row;
        int tempCol = column;

        origin = toSwap.origin;
        row = toSwap.row;
        column = toSwap.column;
        
        toSwap.origin = tempPos;
        toSwap.row = tempRow;
        toSwap.column = tempCol;

        isGoingBack = true;
        toSwap.isGoingBack = true;
        toSwap.transform.parent = movingVisual.gameObject.transform;

        prismReference.prismReference[row][column] = this.gameObject;
        prismReference.prismReference[toSwap.row][toSwap.column] = toSwap.gameObject;

        held = null;
    }

    public void Socket(string objName)
    {
        PrismElement pe = GameObject.Find(objName).GetComponent<PrismElement>();
        pe.row = row;
        pe.column = column;
        pe.gameObject.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        prismReference.prismReference[row][column] = pe.gameObject;

        prismReference.BoardUpdate();
    }
}
