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

    public Sprite displayTrue;
    public Sprite displayNeutral;
    public Sprite displayFalse;
    public GameObject buttonBlocker;

    public Sprite lightOn;
    public Sprite lightOff;

    public GameObject[] indicatorLetters;
    public GameObject[] indicatorNumbers;

    private bool acceptingNewLetters = true;

    private void Start()
    {
        //Util.player = 2;

        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        if(Util.player == 1)
        {
            SetState(player1CodesToDisplay[0]);
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
                    //soundManager.Play("vendingDrop");
                    SetState(player1CodesToDisplay[1]);
                    gameState = 2;
                    return true;
                }
            }

            if(gameState == 2)
            {
                if (solution[0] == player1CodesToCheck[1][0] && solution[1] == player1CodesToCheck[1][1]
                    || solution[0] == player1CodesToCheck[1][1] && solution[1] == player1CodesToCheck[1][0])
                {
                    //soundManager.Play("vendingDrop");
                    SetState(player1CodesToDisplay[2]);
                    gameState = 3;
                    return true;
                }
            }

            if(gameState == 3)
            {
                if (solution[0] == player1CodesToCheck[2][0] && solution[1] == player1CodesToCheck[2][1]
                    || solution[0] == player1CodesToCheck[2][1] && solution[1] == player1CodesToCheck[2][0])
                {
                    //soundManager.Play("vendingDrop");
                    SetVictoryState();
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
                    //soundManager.Play("vendingDrop");
                    SetState(player2CodesToDisplay[0]);
                    gameState = 2;
                    return true;
                }
            }

            if (gameState == 2)
            {
                if (solution[0] == player2CodesToCheck[1][0] && solution[1] == player2CodesToCheck[1][1]
                    || solution[0] == player2CodesToCheck[1][1] && solution[1] == player2CodesToCheck[1][0])
                {
                    //soundManager.Play("vendingDrop");
                    SetState(player2CodesToDisplay[1]);
                    gameState = 3;
                    return true;
                }
            }

            if (gameState == 3)
            {
                if (solution[0] == player2CodesToCheck[2][0] && solution[1] == player2CodesToCheck[2][1]
                    || solution[0] == player2CodesToCheck[2][1] && solution[1] == player2CodesToCheck[2][0])
                {
                    //soundManager.Play("vendingDrop");
                    SetState(player2CodesToDisplay[2]);
                    SetVictoryState();
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
