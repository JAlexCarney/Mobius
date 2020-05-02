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

    public GameObject CastLight(Vector2 pos1, Vector2 pos2, GameObject prefab)
    {
        // Spawn an image object as a little square with 2 screen positions
        GameObject line = Instantiate(prefab);

        // Take one screen postions and subtract it component wise from the other, x-x y-y
        Vector2 delta = pos1 - pos2;

        // Get length and angle of vector
        float length = delta.magnitude;
        float angle = Vector2.SignedAngle(new Vector2(1, 0), delta);

        // Place object at midpoint and rotate at angle of new vector and stretch between the points.
        Vector2 midpoint = (pos1 + pos2) / 2;

        Vector3 position = new Vector3(midpoint.x, midpoint.y, 0f);

        line.transform.parent = this.transform.parent;
        line.transform.SetSiblingIndex(1);

        RectTransform lineRect = line.GetComponent<RectTransform>();

        lineRect.anchoredPosition = position;
        lineRect.localEulerAngles = new Vector3(0, 0, angle);

        lineRect.sizeDelta = new Vector2(length, lineRect.sizeDelta.y);

        return line;
    }

    public GameObject CastLight(Vector2 dest, GameObject prefab)
    {
        RectTransform thisRect = this.GetComponent<RectTransform>();
        Vector2 startVector = new Vector2(thisRect.anchoredPosition.x, thisRect.anchoredPosition.y);

        GameObject line = CastLight(startVector, dest, prefab);

        return line;
    }

    public void RedirectLight()
    {
        
    }

    public void RotateMirror()
    {

        isRotating = true;
    }
}
