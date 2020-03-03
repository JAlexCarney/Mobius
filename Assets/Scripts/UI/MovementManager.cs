using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public CanvasSwapper canvasSwapper;
    
    // 0 -> left
    // 1 -> center
    // 2 -> right
    private int curPos;

    // Start is called before the first frame update
    void Start()
    {
        // 1 = room center
        curPos = 1;
    }

    public void LookLeft()
    {
        int pos = curPos - 1;
        if (pos >= 0)
        {
            SetPosition(pos);
        }
    }

    public void LookRight()
    {
        int pos = curPos + 1;
        if (pos <= 2)
        {
            SetPosition(pos);
        }
    }

    public void SetPosition(int pos)
    {
        // set room left
        if (pos == 0)
        {
            GameObject background = canvasSwapper.currentCanvas.transform.GetChild(0).gameObject;
            background.GetComponent<RectTransform>().anchoredPosition = new Vector3(400, 0, 0);
            curPos = pos;
        }
        // set room center
        else if (pos == 1)
        {
            GameObject background = canvasSwapper.currentCanvas.transform.GetChild(0).gameObject;
            background.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            curPos = pos;
        }
        // set room right
        else if (pos == 2)
        {
            GameObject background = canvasSwapper.currentCanvas.transform.GetChild(0).gameObject;
            background.GetComponent<RectTransform>().anchoredPosition = new Vector3(-400, 0, 0);
            curPos = pos;
        }
    }
}
