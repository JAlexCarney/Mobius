using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Source : MonoBehaviour
{

    // Variables for light casting.
    public RectTransform start;
    public RectTransform end;
    public GameObject linePrefab;
    // Sources always have a set color, so there is no need for dynamic color changing.

    public void Start()
    {
        CastLight(start.anchoredPosition, end.anchoredPosition, linePrefab);
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

}
