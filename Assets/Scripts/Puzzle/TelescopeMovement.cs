using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TelescopeMovement : MonoBehaviour
{

    public GameObject background;
    public GameObject telescope;

    // Variables for scrolling the background.
    private float backgroundX;
    private float backgroundY;
    public float telescopeOffset = 10;

    // Variables for keeping the telescope view in bounds.
    private float backgroundWidth;
    private float backgroundHeight;

    private float teleWidth;
    private float teleHeight;

    private float backgroundEdgeTop;
    private float backgroundEdgeBottom;
    private float backgroundEdgeLeft;
    private float backgroundEdgeRight;

    private float teleEdgeTop;
    private float teleEdgeBottom;
    private float teleEdgeLeft;
    private float teleEdgeRight;

    void Start()
    {
        // Initialize all of the starting values.
        backgroundX = 0f;
        backgroundY = 0f;

        RectTransform bgRect = background.GetComponent<RectTransform>();
        RectTransform teleRect = telescope.GetComponent<RectTransform>();

        bgRect.anchoredPosition = new Vector3(backgroundX, backgroundY, 0);

        backgroundWidth = bgRect.sizeDelta.x;
        backgroundHeight = bgRect.sizeDelta.y;

        teleWidth = teleRect.sizeDelta.x;
        teleHeight = teleRect.sizeDelta.y;

        backgroundEdgeTop = -(backgroundHeight / 2);
        backgroundEdgeBottom = backgroundHeight / 2;
        backgroundEdgeLeft = backgroundWidth / 2;
        backgroundEdgeRight = -(backgroundWidth / 2);

        teleEdgeTop = -(teleHeight / 2);
        teleEdgeBottom = teleHeight / 2;
        teleEdgeLeft = teleWidth / 2;
        teleEdgeRight = -(teleWidth / 2);

    }

    void Update()
    {
        // Scroll the background behind the viewport of the telescope.
        background.GetComponent<RectTransform>()
            .anchoredPosition = new Vector3(backgroundX, backgroundY, 0);

        // Update bounds values.
        teleEdgeTop = backgroundY - (teleHeight / 2);
        teleEdgeBottom = backgroundY + (teleHeight / 2);
        teleEdgeLeft = backgroundX + (teleWidth / 2);
        teleEdgeRight = backgroundX - (teleWidth / 2);

    }

    public void MoveUp()
    {
        backgroundY-=telescopeOffset;

        // Ensure that the top edge of the telescope view doesn't
        // exceed the edge of the background.
        if(teleEdgeTop <= backgroundEdgeTop)
        {
            backgroundY = backgroundEdgeTop + (teleHeight / 2);
        }
    }

    public void MoveDown()
    {
        backgroundY+=telescopeOffset;

        // Ensure that the bottom edge of the telescope view doesn't
        // exceed the edge of the background.
        if (teleEdgeBottom >= backgroundEdgeBottom)
        {
            backgroundY = backgroundEdgeBottom - (teleHeight / 2);
        }
    }

    public void MoveLeft()
    {
        backgroundX+=telescopeOffset;

        // Ensure that the left edge of the telescope view doesn't
        // exceed the edge of the background.
        if (teleEdgeLeft >= backgroundEdgeLeft)
        {
            backgroundX = backgroundEdgeLeft - (teleWidth / 2);
        }
    }

    public void MoveRight()
    {
        backgroundX-=telescopeOffset;

        // Ensure that the right edge of the telescope view doesn't
        // exceed the edge of the background.
        if (teleEdgeRight <= backgroundEdgeRight)
        {
            backgroundX = backgroundEdgeRight + (teleWidth / 2);
        }
    }
}
