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

    // Public variables for debug.
    public float currentRotation;
    public RectTransform mirrorRect;
    public int index;

    // Array to store possible rotation values.
    private readonly List<int> rotationVectors = new List<int> { 45, 135, 225, 315 };

    private bool isRotating;

    // Start is called before the first frame update
    void Start()
    {
        prismReference = this.GetComponentInParent<PrismReference>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isDown)
        {
            thresholdFrames++;
        }

        if (isRotating)
        {
            isRotating = false;

            // Gets the current rotation vector of the mirror.
            mirrorRect = this.GetComponent<RectTransform>();
            currentRotation = mirrorRect.localEulerAngles.z;

            Debug.Log(currentRotation);
            Debug.Log(rotationVectors[3]);

            // Find the current vector in the vector array and update.
            int currentRotationInt = System.Convert.ToInt32(currentRotation);
            index = rotationVectors.IndexOf(currentRotationInt);
            Debug.Log(index);

            // If the vector is at the end of the array, reset to the beginning, otherwise increment.
            if (index == 0)
            {
                currentRotation = rotationVectors[3];
            }
            else
            {
                currentRotation = rotationVectors[index - 1];
                Debug.Log(currentRotation);
            }

            Vector3 updatedRotation = new Vector3(0f, 0f, currentRotation);

            mirrorRect.localEulerAngles = updatedRotation;

            if (mirrorRect.localEulerAngles.z == 45)
            {
                this.orientation = "NW";
            }

            else if (mirrorRect.localEulerAngles.z == 135)
            {
                this.orientation = "SW";
            }

            else if (mirrorRect.localEulerAngles.z == 225)
            {
                this.orientation = "SE";
            }

            else if (mirrorRect.localEulerAngles.z == 315)
            {
                this.orientation = "NE";
            }
        }

    }

    public void OnPointerDown(PointerEventData d)
    {
        thresholdFrames = 0;
        isDown = true;
    }

    public void OnPointerUp(PointerEventData d)
    {
        isDown = false;

        if (thresholdFrames < 30)
        {
            if (this.type == "mirror")
            {
                RotateMirror();
                prismReference.BoardUpdate();
            }
        }
    }

    public void RotateMirror()
    {
        isRotating = true;
    }
}
