using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PrismElement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public string type;
    public string orientation;
    public string colorToCast;
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
    private Vector3 destination;
    private Touch touch;

    // Start is called before the first frame update
    void Start()
    {
        prismReference = this.GetComponentInParent<PrismReference>();

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
            Debug.Log("NewZ: " + newZ);

            Vector3 updatedRotation = new Vector3(0, 0, newZ);
            this.GetComponent<RectTransform>().localEulerAngles = updatedRotation;

            if (newZ >= 44 && newZ <= 46)
            {
                this.orientation = "NW";
                Debug.Log(this.orientation + " " + currentRotation);
                prismReference.BoardUpdate();
                isRotating = false;
            }

            else if (newZ >= 134 && newZ <= 136)
            {
                this.orientation = "SW";
                Debug.Log(this.orientation + " " + currentRotation);
                prismReference.BoardUpdate();
                isRotating = false;
            }

            else if (newZ >= 224 && newZ <= 226)
            {
                this.orientation = "SE";
                Debug.Log(this.orientation + " " + currentRotation);
                prismReference.BoardUpdate();
                isRotating = false;
            }

            else if (newZ >= 314 && newZ <= 316)
            {
                this.orientation = "NE";
                Debug.Log(this.orientation + " " + currentRotation);
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
                transform.parent = parent;
            }
            else
            {
                counter++;
                transform.position = Vector3.Lerp(destination, origin, (float)counter / 30f);
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
                DropMirror();
            }
        }
    }

    public void RotateMirror()
    {
        isRotating = true;
    }

    public void DropMirror()
    {
        isGoingBack = true;
        destination = this.transform.position;
        held = null;
    }
}
