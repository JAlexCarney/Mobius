using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceMovementManager : MonoBehaviour
{
    public CanvasSwapper canvasSwapper;
    
    // -1, -2, -3 -> left
    // 0 -> center
    // 1, 2, 3 -> right
    private int curPos;

    // Start is called before the first frame update
    void Start()
    {
        // 0 = room center
        curPos = 0;
    }

    public void TurnLeft()
    {
        int pos = curPos - 1;

        // Wraparound
        if (pos < -3)
        {
            pos = 3;
        }

        print(pos);
        SetPosition(pos);
    }

    public void TurnRight()
    {
        int pos = curPos + 1;

        // Wraparound
        if(pos > 3)
        {
            pos = -3;
        }

        print(pos);
        SetPosition(pos);
    }

    public void SetPosition(int pos)
    {
        // set room left
        if (pos == -3)
        {
            GameObject background = canvasSwapper.currentCanvas.transform.GetChild(0).gameObject;
            background.GetComponent<RectTransform>().anchoredPosition = new Vector3(2400, 0, 0);
            curPos = pos;
        }
        else if (pos == -2)
        {
            GameObject background = canvasSwapper.currentCanvas.transform.GetChild(0).gameObject;
            background.GetComponent<RectTransform>().anchoredPosition = new Vector3(1600, 0, 0);
            curPos = pos;
        }
        else if (pos == -1)
        {
            GameObject background = canvasSwapper.currentCanvas.transform.GetChild(0).gameObject;
            background.GetComponent<RectTransform>().anchoredPosition = new Vector3(800, 0, 0);
            curPos = pos;
        }
        // set room center
        else if (pos == 0)
        {
            GameObject background = canvasSwapper.currentCanvas.transform.GetChild(0).gameObject;
            background.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            curPos = pos;
        }
        // set room right
        else if (pos == 1)
        {
            GameObject background = canvasSwapper.currentCanvas.transform.GetChild(0).gameObject;
            background.GetComponent<RectTransform>().anchoredPosition = new Vector3(-800, 0, 0);
            curPos = pos;
        }
        else if (pos == 2)
        {
            GameObject background = canvasSwapper.currentCanvas.transform.GetChild(0).gameObject;
            background.GetComponent<RectTransform>().anchoredPosition = new Vector3(-1600, 0, 0);
            curPos = pos;
        }
        else if (pos == 3)
        {
            GameObject background = canvasSwapper.currentCanvas.transform.GetChild(0).gameObject;
            background.GetComponent<RectTransform>().anchoredPosition = new Vector3(-2400, 0, 0);
            curPos = pos;
        }
    }
}
