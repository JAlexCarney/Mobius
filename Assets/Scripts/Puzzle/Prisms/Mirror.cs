using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mirror : MonoBehaviour
{

    // Public variables for debug.
    public float currentRotation;
    public RectTransform mirrorRect;
    public int index;

    // Array to store possible rotation values.
    private readonly List<int> rotationVectors = new List<int> {45, 135, 225, 315};

    // Variables for rotation in FixedUpdate.
    public bool isRotating = false;

    public void FixedUpdate()
    {
        if (isRotating)
        {
            isRotating = false;

            // Gets the current rotation vector of the mirror.
            mirrorRect = this.GetComponent<RectTransform>();
            currentRotation = mirrorRect.localEulerAngles.z;

            Debug.Log(currentRotation);
            Debug.Log(rotationVectors[3]);

            // Find the current vector in the vector array and update.
            int currentRotationInt = Convert.ToInt32(currentRotation);
            index = rotationVectors.IndexOf(currentRotationInt);
            Debug.Log(index);

            // If the vector is at the end of the array, reset to the beginning, otherwise increment.
            if(index == 0)
            {
                currentRotation = rotationVectors[3];
            }
            else
            {
                currentRotation = rotationVectors[index-1];
                Debug.Log(currentRotation);
            }

            Vector3 updatedRotation = new Vector3(0f, 0f, currentRotation);

            mirrorRect.localEulerAngles = updatedRotation;
        }
    }

    public void ContactColor()
    {

    }

    public void RotateMirror()
    {

        isRotating = true;
    }
}
