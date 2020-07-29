using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wires : MonoBehaviour
{

    // Wires to be drawn
    public GameObject fixedWire1;
    public GameObject fixedWire2;
    public GameObject fixedWire3;
    public GameObject wire1;
    public GameObject wire2;
    public GameObject wire3;

    // Sources and sockets for the fixed partner solution.
    public GameObject fixedSource1;
    public GameObject fixedSocket1;
    public GameObject fixedSource2;
    public GameObject fixedSocket2;
    public GameObject fixedSource3;
    public GameObject fixedSocket3;

    // Sources and sockets for solution checking.
    public GameObject source1;
    public GameObject socket1;
    public GameObject source2;
    public GameObject socket2;
    public GameObject source3;
    public GameObject socket3;

    // Start is called before the first frame update
    void Start()
    {
        DrawWire(fixedSource1.transform.localPosition, fixedSocket1.transform.localPosition, fixedWire1);
        DrawWire(fixedSource2.transform.localPosition, fixedSocket2.transform.localPosition, fixedWire2);
        DrawWire(fixedSource3.transform.localPosition, fixedSocket3.transform.localPosition, fixedWire3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceWire(string sourceWire)
    {

    }

    public GameObject DrawWire(Vector2 pos1, Vector2 pos2, GameObject wire)
    {

        wire.GetComponent<RectTransform>().SetParent(this.transform);
        wire.transform.SetSiblingIndex(1);

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
}
