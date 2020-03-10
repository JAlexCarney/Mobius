using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VendingCodeEnterer : MonoBehaviour
{

    public string[] stateOrder;

    public string displayCode;

    public GameObject display;
    public GameObject displayVal;

    public GameObject[] indicatorLetters;
    public GameObject[] indicatorNumbers;


    public void displayUpdate(string value)
    {
        // Updates display code.
        displayCode += value;
        Text displayText = displayVal.GetComponent<Text>();
        displayText.text = displayCode; 

    }

    bool checkSolution(string solution)
    {
        return false;
    }

    void colorReset(bool solution)
    {
        // Change color based on solution correctness, then invoke wipe string.

    }

    void wipeString()
    {
        displayCode = string.Empty;
    }
}
