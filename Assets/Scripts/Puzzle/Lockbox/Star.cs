using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Star : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int starNumber; 
    private bool isHeld = false;
    public bool isGoingBack = false;
    private int counter = 0;
    public int currentLoc = 0; 
    public Vector3 startPos;
    public Vector3 dropPos;
    public string label;
    public static bool justReleased = false;
    public int swapID;
    static public bool holding = false;
    static public string held = "";
    static public GameObject heldObj = null;

    private TopVisualFolllow movingVisual;
    private Transform parent;

    private Touch touch;
    private LockBoxReference lockBoxReference; 

    private void Start()
    {
        label = gameObject.name;
        startPos = transform.position;
        movingVisual = GameObject.Find("MovingVisualCanvas").GetComponent<TopVisualFolllow>();
        parent = transform.parent;
        lockBoxReference = GetComponentInParent<LockBoxReference>();

        //update star w position
        lockBoxReference.starLocations.Add(starNumber, currentLoc);
    }

    // FixedUpdate is called once per each 1/50th of a second
    void FixedUpdate()
    {
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
        }
        if (isHeld)
        {
            int nextLoc = getNextLoc(); 
            Vector3 nextLocPosition = lockBoxReference.locations[nextLoc].position;
            if (Input.touchCount == 0)
            {
                //get closest point on line 
                transform.position = ProjectPointOnLineSegment(lockBoxReference.locations[currentLoc].position, nextLocPosition, Input.mousePosition);
            }
            else
            {
                transform.position = ProjectPointOnLineSegment(lockBoxReference.locations[currentLoc].position, nextLocPosition, Input.mousePosition);
            }

            //determine if star should go to next point if dropped or stay at current one
            if (Vector3.Distance(transform.position, nextLocPosition) < Vector3.Distance(transform.position, startPos))
            {
                startPos = nextLocPosition;
                currentLoc = getNextLoc(); 
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

                //update location
                lockBoxReference.starLocations[starNumber] = currentLoc;
                
                //check solution
                lockBoxReference.CheckSolution(); 
            }
            else
            {
                counter++;
                transform.position = Vector3.Lerp(dropPos, startPos, 1 + Mathf.Pow((float)counter / 30f - 1, 3));

                //transform.position = Vector3.Lerp(dropPos, startPos, easingFunct((float)counter/30));

            }
        }
    }

    //https://gist.github.com/Fonserbc/3d31a25e87fdaa541ddf from this god bless u sweet soul
    public static float easingFunct(float k)
    {
        return 1f - Mathf.Cos(k * Mathf.PI / 2f);
    }

    private void ReturnToStartPosition()
    {
        transform.position = startPos;
    }

    public void OnPointerDown(PointerEventData d)
    {
        //Debug.Log("Clicked");
        Hold();
    }

    public void OnPointerUp(PointerEventData d)
    {
        //Debug.Log("released");
        dropPos = transform.position;
        justReleased = true;
        Invoke("Drop", .01f);
    }

    public void Hold()
    {
        if (!holding && !isGoingBack)
        {
            isHeld = true;
            holding = true;
            held = label;
            heldObj = gameObject;
            transform.parent = movingVisual.gameObject.transform;
        }
    }

    public void Drop()
    {
        isHeld = false;
        holding = false;
        held = "";
        heldObj = null;
        isGoingBack = true;
        justReleased = false;
    }

    //there are 8 points on the star circle, find which point the star is headed to 
    //use when currently dragging
    private int getNextLoc()
    {
        //get surrounding locs
        int left = currentLoc + 1;
        if (left > 7) left = 0;
        int right = currentLoc - 1;
        if (right < 0) right = 7;

        return Vector3.Distance(Input.mousePosition, lockBoxReference.locations[left].position)
            < Vector3.Distance(Input.mousePosition, lockBoxReference.locations[right].position) ? left : right;  
    }

    //This function returns a point which is a projection from a point to a line segment.
    //If the projected point lies outside of the line segment, the projected point will 
    //be clamped to the appropriate line edge.
    //If the line is infinite instead of a segment, use ProjectPointOnLine() instead.
    //from http://wiki.unity3d.com/index.php/3d_Math_functions?_ga=2.233495002.533369051.1596576068-1185623105.1596576068
    private Vector3 ProjectPointOnLineSegment(Vector3 linePoint1, Vector3 linePoint2, Vector3 point)
    {

        Vector3 vector = linePoint2 - linePoint1;

        Vector3 projectedPoint = ProjectPointOnLine(linePoint1, vector.normalized, point);

        int side = PointOnWhichSideOfLineSegment(linePoint1, linePoint2, projectedPoint);

        //The projected point is on the line segment
        if (side == 0)
        {

            return projectedPoint;
        }

        if (side == 1)
        {

            return linePoint1;
        }

        if (side == 2)
        {

            return linePoint2;
        }

        //output is invalid
        return Vector3.zero;
    }
    private Vector3 ProjectPointOnLine(Vector3 linePoint, Vector3 lineVec, Vector3 point)
    {

        //get vector from point on line to point in space
        Vector3 linePointToPoint = point - linePoint;

        float t = Vector3.Dot(linePointToPoint, lineVec);

        return linePoint + lineVec * t;
    }
    private int PointOnWhichSideOfLineSegment(Vector3 linePoint1, Vector3 linePoint2, Vector3 point)
    {

        Vector3 lineVec = linePoint2 - linePoint1;
        Vector3 pointVec = point - linePoint1;

        float dot = Vector3.Dot(pointVec, lineVec);

        //point is on side of linePoint2, compared to linePoint1
        if (dot > 0)
        {

            //point is on the line segment
            if (pointVec.magnitude <= lineVec.magnitude)
            {

                return 0;
            }

            //point is not on the line segment and it is on the side of linePoint2
            else
            {

                return 2;
            }
        }

        //Point is not on side of linePoint2, compared to linePoint1.
        //Point is not on the line segment and it is on the side of linePoint1.
        else
        {

            return 1;
        }
    }

}
