using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VendingCodeEnterer : MonoBehaviour
{

    public SoundManager soundManager;

    public string[] player1CodesToCheck;
    public string[] player1CodesToDisplay;
    public string[] player2CodesToCheck;
    public string[] player2CodesToDisplay;

    public int gameState = 1;

    public string displayCode;
    public int displayCountDebug;

    public GameObject display;

    // objects given for correct answers!
    private int count = 0;
    private float animTime = 30f;
    private bool isFalling;
    private Vector3 startPos;
    private Transform fallingObj;
    public Transform obj1;
    public Transform obj2;
    public Transform obj3;
    public Sprite obj1Sprite;
    public Sprite obj2Sprite;
    public Transform objDestination;
    public GameObject door;

    public Sprite displayTrue;
    public Sprite displayNeutral;
    public Sprite displayFalse;
    public GameObject buttonBlocker;

    public Sprite lightOn;
    public Sprite lightOff;

    public GameObject[] indicatorLetters;
    public GameObject[] indicatorNumbers;

    private bool acceptingNewLetters = true;
    private int objNum = 0;

    private void Start()
    {

        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        if(Util.player == 1)
        {
            SetState(player1CodesToDisplay[0]);
        }
    }

    public void FixedUpdate()
    {
        if (isFalling)
        {
            if (count == animTime)
            {
                count = 0;
                isFalling = false;
                fallingObj.position = objDestination.position;
                if (objNum == 1)
                {
                    fallingObj.GetComponent<Image>().sprite = obj1Sprite;
                }
                else if (objNum == 2)
                {
                    fallingObj.GetComponent<Image>().sprite = obj2Sprite;
                }
                door.SetActive(false);
            }
            else if (count < animTime)
            {
                count++;
                fallingObj.position = Vector3.Lerp(startPos, objDestination.position, Mathf.Pow((float)count / animTime, 2));
                fallingObj.eulerAngles = Vector3.Lerp(new Vector3(0, 0, 0), objDestination.eulerAngles, Mathf.Pow((float)count / animTime, 2));
            }
        }
    }

    public void DispenseObj(Transform obj)
    {
        soundManager.Play("vendingDrop");
        isFalling = true;
        fallingObj = obj;
        startPos = obj.position;
        objNum++;
    }

    public void CollectObj()
    {
        Invoke("CollectObjDelayed", 0.5f);
        soundManager.Play("vendingSlap");
        buttonBlocker.SetActive(false);
    }

    public void CollectObjDelayed()
    {
        door.SetActive(true);
    }

    public void CollectObj(int objNum)
    {
        if (Util.player == 1)
        {
            if (objNum == 1)
            {
                CollectObj();
                SetState(player1CodesToDisplay[1]);
            }
            else if (objNum == 2)
            {
                CollectObj();
                SetState(player1CodesToDisplay[2]);
            }
            else if (objNum == 3)
            {
                CollectObj();
                SetVictoryState();
            }
        }
        else
        {
            if (objNum == 1)
            {
                CollectObj();
                SetState(player2CodesToDisplay[0]);
            }
            else if (objNum == 2)
            {
                CollectObj();
                SetState(player2CodesToDisplay[1]);
            }
            else if (objNum == 3)
            {
                CollectObj();
                SetState(player2CodesToDisplay[2]);
                SetVictoryState();
            }
        }
    }

    public void SetState(string state)
    {
        // Makes sure no other lights are turned on before activating new lights.
        foreach(GameObject indicator in indicatorLetters)
        {
            indicator.GetComponent<Image>().sprite = lightOff;
        }
        foreach (GameObject indicator in indicatorNumbers)
        {
            indicator.GetComponent<Image>().sprite = lightOff;
        }

        // Finds the lights corresponding to the proper code and turns them on.
        string letter = state[0].ToString();
        string number = state[1].ToString();

        GameObject letterIndicator = transform.Find("Indicators").Find("Indicator"+letter).gameObject;
        GameObject numberIndicator = transform.Find("Indicators").Find("Indicator"+number).gameObject;

        letterIndicator.GetComponent<Image>().sprite = lightOn;
        numberIndicator.GetComponent<Image>().sprite = lightOn;

    }

    public void DisplayUpdate(string value)
    {
        print("Display Update: " + value);

        // Updates display code.
        if(acceptingNewLetters)
        {
            displayCode += value;
            display.GetComponentInChildren<Text>().text = displayCode;

            displayCountDebug = displayCode.Length;
        }
        
        // If a complete code is input, checks if the solution is correct.
        if(displayCode.Length == 2)
        {
            ColorReset(CheckSolution(displayCode));

            acceptingNewLetters = false;
            Invoke("DisplayReset", 1);
        }

    }

    bool CheckSolution(string solution)
    {
        print("Check Solution: " + solution);

        if(Util.player == 1)
        {
            if(gameState == 1)
            {
                if (solution[0] == player1CodesToCheck[0][0] && solution[1] == player1CodesToCheck[0][1]
                    || solution[0] == player1CodesToCheck[0][1] && solution[1] == player1CodesToCheck[0][0])
                {
                    DispenseObj(obj1);
                    //SetState(player1CodesToDisplay[1]);
                    gameState = 2;
                    buttonBlocker.SetActive(true);
                    return true;
                }
            }

            if(gameState == 2)
            {
                if (solution[0] == player1CodesToCheck[1][0] && solution[1] == player1CodesToCheck[1][1]
                    || solution[0] == player1CodesToCheck[1][1] && solution[1] == player1CodesToCheck[1][0])
                {
                    DispenseObj(obj2);
                    //SetState(player1CodesToDisplay[2]);
                    gameState = 3;
                    buttonBlocker.SetActive(true);
                    return true;
                }
            }

            if(gameState == 3)
            {
                if (solution[0] == player1CodesToCheck[2][0] && solution[1] == player1CodesToCheck[2][1]
                    || solution[0] == player1CodesToCheck[2][1] && solution[1] == player1CodesToCheck[2][0])
                {
                    DispenseObj(obj3);
                    buttonBlocker.SetActive(true);
                    //SetVictoryState();
                    return true;
                }
            }

        }

        if(Util.player == 2)
        {
            if (gameState == 1)
            {
                if (solution[0] == player2CodesToCheck[0][0] && solution[1] == player2CodesToCheck[0][1]
                    || solution[0] == player2CodesToCheck[0][1] && solution[1] == player2CodesToCheck[0][0])
                {
                    DispenseObj(obj1);
                    //SetState(player2CodesToDisplay[0]);
                    gameState = 2;
                    buttonBlocker.SetActive(true);
                    return true;
                }
            }

            if (gameState == 2)
            {
                if (solution[0] == player2CodesToCheck[1][0] && solution[1] == player2CodesToCheck[1][1]
                    || solution[0] == player2CodesToCheck[1][1] && solution[1] == player2CodesToCheck[1][0])
                {
                    DispenseObj(obj2);
                    //SetState(player2CodesToDisplay[1]);
                    gameState = 3;
                    buttonBlocker.SetActive(true);
                    return true;
                }
            }

            if (gameState == 3)
            {
                if (solution[0] == player2CodesToCheck[2][0] && solution[1] == player2CodesToCheck[2][1]
                    || solution[0] == player2CodesToCheck[2][1] && solution[1] == player2CodesToCheck[2][0])
                {
                    DispenseObj(obj3);
                    buttonBlocker.SetActive(true);
                    //SetState(player2CodesToDisplay[2]);
                    //SetVictoryState();
                    return true;
                }
            }
        }

        
        return false;
    }

    void ColorReset(bool solution)
    {
        print("Setting Color: " + solution);

        // Change color based on solution correctness, then invoke wipe string.
        if(solution)
        {
            display.GetComponent<Image>().sprite = displayTrue;
        }
        else
        {
            display.GetComponent<Image>().sprite = displayFalse;
        }

    }

    void SetVictoryState()
    {
        Invoke("DisplayReset", 1);
        Invoke("InvokeDisplayTrue", 2);
        buttonBlocker.SetActive(true);
    }

    void InvokeDisplayTrue()
    {
        print("Setting Victorious State");

        // Avoid time overlapping issues by creating an invokable function.
        display.GetComponent<Image>().sprite = displayTrue;
    }

    void DisplayReset()
    {
        print("Resetting Display");

        // Reset displayCode and reset display to gray.
        displayCode = string.Empty;
        display.GetComponentInChildren<Text>().text = displayCode;
        display.GetComponent<Image>().sprite = displayNeutral;
        acceptingNewLetters = true;
    }
}
