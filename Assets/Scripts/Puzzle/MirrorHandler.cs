using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorHandler : MonoBehaviour
{

    // Array to store possible rotation values.
    private Vector3[] rotationVectors = new Vector3[] {
            new Vector3(0, 0, 45),
            new Vector3(0, 0, 135),
            new Vector3(0, 0, 225),
            new Vector3(0, 0, 315)
        };

    // Variables for rotation in FixedUpdate.
    bool isRotating = false;

    public void FixedUpdate()
    {
        if (isRotating)
        {

        }
    }

    public void UpdateMaster(GameObject mirror)
    {

    }

    public void RotateMirror()
    {

        isRotating = true;
    }
}
