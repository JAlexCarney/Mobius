using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CodeEnterer : MonoBehaviour
{
    public int codeLength;
    public int[] correctCode;
    public Sprite[] symbols;

    private int[] code;
    private int currentSymbolIndex = 0;

    public void Start()
    {
        code = new int[codeLength];
        for (int i = 0; i < codeLength; i++)
        {
            code[i] = 0;
        }
    }

    public void NextSymbol(int index)
    {
        currentSymbolIndex = Util.Mod(code[index] + 1, symbols.Length);
        GameObject symbol = transform.Find("Slot" + index).gameObject;
        symbol.GetComponent<Image>().sprite = symbols[currentSymbolIndex];
        code[index] = currentSymbolIndex;
    }

    public void PreviousSymbol(int index)
    {
        currentSymbolIndex = Util.Mod(code[index] - 1, symbols.Length);
        GameObject symbol = transform.Find("Slot" + index).gameObject;
        symbol.GetComponent<Image>().sprite = symbols[currentSymbolIndex];
        code[index] = currentSymbolIndex;
    }

    public void CheckCode(string name) {
        for (int i = 0; i < codeLength; i++)
        {
            if (code[i] != correctCode[i])
            {
                return;
            }
        }
        Debug.Log("Correct!");
        ObjectInspect inspector = GameObject.Find("ObjectInspect").GetComponent<ObjectInspect>();
        inspector.HideObject(gameObject.name);
        inspector.InspectObject(name);
    }
}
