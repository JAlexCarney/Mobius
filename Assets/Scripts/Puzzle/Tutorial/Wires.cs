using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Wires : MonoBehaviour
{

    // Wires to be drawn.
    public GameObject fixedWire1;
    public GameObject fixedWire2;
    public GameObject fixedWire3;
    public GameObject moveableWire1;
    public GameObject moveableWire2;
    public GameObject moveableWire3;

    // Objects to drag wires.
    public GameObject wireDragger1;
    public GameObject wireDragger2;
    public GameObject wireDragger3;

    // Dragger start positions.
    public Vector3 startPos1;
    public Vector3 startPos2;
    public Vector3 startPos3;

    // Sources and sockets for the fixed partner solution.
    public GameObject fixedSource1;
    public GameObject fixedSocket1;
    public GameObject fixedSource2;
    public GameObject fixedSocket2;
    public GameObject fixedSource3;
    public GameObject fixedSocket3;

    // Sources and sockets for solution input.
    public GameObject source1;
    public GameObject socket1;
    public GameObject source2;
    public GameObject socket2;
    public GameObject source3;
    public GameObject socket3;

    // Event to invoke upon success or failure.
    public UnityEvent winEvent;
    public UnityEvent failEvent;

    // Start is called before the first frame update
    void Start()
    {
        // Draw fixed wires to represent partner solution.
        DrawWire(fixedSource1.transform.localPosition, fixedSocket1.transform.localPosition, fixedWire1);
        DrawWire(fixedSource2.transform.localPosition, fixedSocket2.transform.localPosition, fixedWire2);
        DrawWire(fixedSource3.transform.localPosition, fixedSocket3.transform.localPosition, fixedWire3);

        startPos1 = wireDragger1.transform.position;
        startPos2 = wireDragger2.transform.position;
        startPos3 = wireDragger3.transform.position;

    }

    private void Update()
    {
        DrawWireDynamic(source1.transform.localPosition, wireDragger1.transform.localPosition + new Vector3 (0, 85, 0), moveableWire1);
        DrawWireDynamic(source2.transform.localPosition, wireDragger2.transform.localPosition + new Vector3(0, 85, 0), moveableWire2);
        DrawWireDynamic(source3.transform.localPosition, wireDragger3.transform.localPosition + new Vector3(0, 85, 0), moveableWire3);
    }

    public void PlaceWire(string placeInput)
    {
        List<string> placeInputSplit = Util.Split(placeInput, '+');
        Debug.Log(placeInputSplit[0] + ' ' + placeInputSplit[1] + ' ' + placeInputSplit[2]);

        GameObject source = GameObject.Find(placeInputSplit[0]);
        Debug.Log(source.name);

        GameObject socket = GameObject.Find(placeInputSplit[1]);
        Debug.Log(socket.name);

        GameObject wire = GameObject.Find(placeInputSplit[2]);
        Debug.Log(wire.name);

        Debug.Log("Now placing the wire from " + source + " in " + socket + ".");

        if(socket.GetComponent<WireData>().currentSource == null)
        {
            source.GetComponent<WireData>().currentSocket = socket;
            socket.GetComponent<WireData>().currentSource = source;

            Draggable.heldObj.GetComponent<Draggable>().startPos = socket.transform.position;
        }

    }

    public void PickUpWire(string pickUpInput)
    {
        List<string> inputSplit = Util.Split(pickUpInput, '+');
        Debug.Log(inputSplit[0] + ' ' + inputSplit[1]);

        GameObject wireSource = GameObject.Find(inputSplit[0]);

        GameObject socketTrue = wireSource.GetComponent<WireData>().currentSocket;
        Debug.Log("Picked up wire connected to this source -> " + inputSplit[0]);

        // If the selected source has a current socket.
        if (socketTrue)
        {
            Debug.Log("This source is currently connected to a socket. Resetting.");

            // Clear the socket's source.
            Debug.Log(socketTrue.GetComponent<WireData>().currentSource.name);
            socketTrue.GetComponent<WireData>().currentSource = null;
            Debug.Log("This source is no longer bound to a socket.");

            // Clear the source's socket.
            Debug.Log(socketTrue.name);
            wireSource.GetComponent<WireData>().currentSocket = null;
            Debug.Log("This socket is no longer bound to a source.");

            // Reset the source's start position to the wire source.
            if (inputSplit[1] == "startPos1")
            {
                Draggable.heldObj.GetComponent<Draggable>().startPos = startPos1;
                Debug.Log("This source is now reset, and it's connected socket is cleared.");
            }

            if (inputSplit[1] == "startPos2")
            {
                Draggable.heldObj.GetComponent<Draggable>().startPos = startPos2;
                Debug.Log("This source is now reset, and it's connected socket is cleared.");
            }

            if (inputSplit[1] == "startPos3")
            {
                Draggable.heldObj.GetComponent<Draggable>().startPos = startPos3;
                Debug.Log("This source is now reset, and it's connected socket is cleared.");
            }


        }
    }

    public GameObject DrawWire(Vector2 pos1, Vector2 pos2, GameObject wire)
    {

        //wire.GetComponent<RectTransform>().SetParent(this.transform);
        //wire.transform.SetSiblingIndex(1);

        // Take one screen postions and subtract it component wise from the other, x-x y-y
        Vector2 delta = pos1 - pos2;

        // Get length and angle of vector
        float length = delta.magnitude;
        float angle = Vector2.SignedAngle(new Vector2(1, 0), delta);

        // Place object at midpoint and rotate at angle of new vector and stretch between the points.
        Vector2 midpoint = (pos1 + pos2) / 2;

        Vector3 position = new Vector3(midpoint.x, midpoint.y, 0f);

        RectTransform lineRect = wire.GetComponent<RectTransform>();

        lineRect.anchoredPosition = position;
        lineRect.localEulerAngles = new Vector3(0, 0, angle);

        lineRect.sizeDelta = new Vector2(length, lineRect.sizeDelta.y);

        wire.transform.localScale = new Vector3(1f, 1f, 1f);

        return wire;
    }

    public GameObject DrawWireDynamic(Vector2 pos1, Vector2 pos2, GameObject wire)
    {

        //wire.GetComponent<RectTransform>().SetParent(this.transform);
        //wire.transform.SetSiblingIndex(1);

        // Take one screen postions and subtract it component wise from the other, x-x y-y
        Vector2 delta = pos1 - pos2;

        // Get length and angle of vector
        float length = delta.magnitude;
        float angle = Vector2.SignedAngle(new Vector2(1, 0), delta);

        // Place object at midpoint and rotate at angle of new vector and stretch between the points.
        Vector2 midpoint = (pos1 + pos2) / 2;

        Vector3 position = new Vector3(midpoint.x, midpoint.y, 0f);

        RectTransform lineRect = wire.GetComponent<RectTransform>();

        lineRect.anchoredPosition = position;
        lineRect.localEulerAngles = new Vector3(0, 0, angle);

        lineRect.sizeDelta = new Vector2(length, lineRect.sizeDelta.y);

        wire.transform.localScale = new Vector3(1f, 1f, 1f);

        return wire;
    }

    public void CheckSolution()
    {
        WireData source1Data = source1.GetComponent<WireData>();
        WireData source2Data = source2.GetComponent<WireData>();
        WireData source3Data = source3.GetComponent<WireData>();

        if(source1Data.currentSocket == source1Data.correctSocket &&
            source2Data.currentSocket == source2Data.correctSocket &&
            source3Data.currentSocket == source3Data.correctSocket)
        {
            winEvent.Invoke();
        } else
        {
            failEvent.Invoke();
        }

        
    }
}
