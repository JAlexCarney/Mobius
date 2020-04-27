using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismMaster : MonoBehaviour
{
    // Information object for prism reflection.
    struct PrismObject
    {
        public bool isMirror;
        public bool isPrism;
        public bool isSource;
        public bool isLit;

        public string facing;
        public string color;

        public PrismObject(bool isMirror, bool isPrism, bool isSource, bool isLit, string facing, string color)
        {
            this.isMirror = isMirror;
            this.isPrism = isPrism;
            this.isSource = isSource;
            this.isLit = isLit;

            this.facing = facing;
            this.color = color;
        }
    }

    // 2D Array to represent the prism board.
    public int dim = 5;
    PrismObject [,] prisms;

    private void Start()
    {
        // Initialize entire array as empty prism objects.
        for (int i = 0; i < dim; i++)
        {
            for (int j = 0; j < dim; j++)
            {
                prisms[i, j] = new PrismObject();
            }
        }

        // Initialize unique prism objects depending on player.
        if (Util.player == 1)
        {
            prisms[0, 0] = new PrismObject(true, false, false, false, "right", "white");
            prisms[0, 1] = new PrismObject(false, false, true, true, "right", "red");
            prisms[1, 2] = new PrismObject(false, true, false, true, "right", "red");

        }
    }

    public void UpdateDisplay()
    {

    }

    public void DebugCall()
    {

    }
}
