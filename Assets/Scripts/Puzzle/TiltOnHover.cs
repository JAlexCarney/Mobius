using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltOnHover : MonoBehaviour
{
    public GameObject target;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CheckBounds(transform.position))
        {
            transform.eulerAngles = new Vector3(0f, 0f, 50f);
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
    }

    public bool CheckBounds(Vector3 objectPos)
    {
        Vector3 targetPos = target.transform.position;
        Vector3 delta = objectPos - targetPos;
        float width = target.GetComponent<RectTransform>().rect.width;
        float height = target.GetComponent<RectTransform>().rect.height;
        if (delta.x < width / 2 && delta.x > -width / 2 &&
            delta.y < height / 2 && delta.y > -height / 2)
        {
            return true;
        }
        return false;
    }
}
