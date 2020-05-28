using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public int framesInLoop;

    private Vector3 startPos;
    private Vector3 endPos;
    private int count;

    public void Start()
    {
        startPos = GetComponent<RectTransform>().anchoredPosition;
        endPos = new Vector3(GetComponent<RectTransform>().anchoredPosition.x - 1600,
                        GetComponent<RectTransform>().anchoredPosition.y,
                        0);
        count = 0;
    }

    public void FixedUpdate()
    {
        if (count == framesInLoop)
        {
            Vector2 temp = transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition;
            transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition;
            transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = temp;
            count = 0;
        }
        GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(startPos, endPos, (float)count / framesInLoop);
        count++;
    }

}
