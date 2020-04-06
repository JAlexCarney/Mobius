using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VendingCodeEnterer : MonoBehaviour
{

    public string[] codestoCheck;
    public string[] codestoDisplay;

    public string displayCode;
    public int displayCountDebug;

    public GameObject display;

    public Sprite displayTrue;
    public Sprite displayNeutral;
    public Sprite displayFalse;

    public Sprite lightOn;
    public Sprite lightOff;

    public GameObject[] indicatorLetters;
    public GameObject[] indicatorNumbers;

    private void Start()
    {
        SetState(codestoDisplay[0]);
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
        displayCode += value;
        display.GetComponentInChildren<Text>().text = displayCode;

        displayCountDebug = displayCode.Length;

        // If a complete code is input, checks if the solution is correct.
        if(displayCode.Length == 2)
        {
            colorReset(CheckSolution(displayCode));

            Invoke("DisplayReset", 1);
        }

    }

    bool CheckSolution(string solution)
    {
        print("Check Solution: " + solution);

        
        return false;
    }

    void colorReset(bool solution)
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

    void DisplayReset()
    {
        print("Resetting Display");

        // Reset displayCode and reset display to gray.
        displayCode = string.Empty;
        display.GetComponentInChildren<Text>().text = displayCode;
        display.GetComponent<Image>().sprite = displayNeutral;
    }
}
