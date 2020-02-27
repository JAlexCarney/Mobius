using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementManager : MonoBehaviour
{
    public CanvasSwapper canvasSwapper;
    public GameObject LeftButton;
    public GameObject RightButton;
    public GameObject BackButton;
    public bool backIsDisabled = false;
    public bool leftIsDisabled = false;
    public bool rightIsDisabled = false;
    public bool lookingEnabled = true;
    
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

    public void CenterAndEnableLooking()
    {
        EnableLeft();
        EnableRight();
        SetPosition(1);
        lookingEnabled = true;
    }

    public void DisableLooking()
    {
        DisableLeft();
        DisableRight();
        SetPosition(1);
        lookingEnabled = false;
    }

    public void LookLeft()
    {
        if (!leftIsDisabled && lookingEnabled)
        {
            int pos = curPos - 1;
            if (pos >= 0)
            {
                SetPosition(pos);
                if (pos == 0)
                {
                    DisableLeft();
                }
                if (rightIsDisabled)
                {
                    EnableRight();
                }
            }
        }
    }

    public void LookRight()
    {
        if (!rightIsDisabled && lookingEnabled)
        {
            int pos = curPos + 1;
            if (pos <= 2)
            {
                SetPosition(pos);
                if (pos == 2)
                {
                    DisableRight();
                }
                Debug.Log(leftIsDisabled);
                if (leftIsDisabled)
                {
                    EnableLeft();
                }
            }
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

    public void DisableBack()
    {
        backIsDisabled = true;
        BackButton.GetComponent<Image>().color = Color.gray;
        BackButton.GetComponent<Button>().interactable = false;
    }

    public void EnableBack()
    {
        backIsDisabled = false;
        BackButton.GetComponent<Image>().color = Color.white;
        BackButton.GetComponent<Button>().interactable = true;
    }

    private void EnableRight()
    {
        RightButton.GetComponent<Image>().color = Color.white;
        rightIsDisabled = false;
        RightButton.GetComponent<Button>().interactable = true;
    }

    private void DisableRight()
    {
        RightButton.GetComponent<Image>().color = Color.gray;
        rightIsDisabled = true;
        RightButton.GetComponent<Button>().interactable = false;
    }

    private void EnableLeft()
    {
        LeftButton.GetComponent<Image>().color = Color.white;
        leftIsDisabled = false;
        LeftButton.GetComponent<Button>().interactable = true;
    }

    private void DisableLeft()
    {
        LeftButton.GetComponent<Image>().color = Color.gray;
        LeftButton.GetComponent<Button>().interactable = false;
        leftIsDisabled = true;
    }
}
