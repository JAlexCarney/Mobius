using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SymbolCodeCheck : MonoBehaviour
{
    public Symbol[] symbols;
    public int player;
    public UnityEvent winEvent;

    private string[] solution = new string[10];

    // Start is called before the first frame update
    void Start()
    {
        if (player == 1)
        {
            solution[0] = "cyan";
            solution[1] = "off";
            solution[2] = "off";
            solution[3] = "green";
            solution[4] = "off";
            solution[5] = "off";
            solution[6] = "off";
            solution[7] = "off";
            solution[8] = "blue";
            solution[9] = "off";
        }
        else
        {
            solution[0] = "off";
            solution[1] = "magenta";
            solution[2] = "off";
            solution[3] = "off";
            solution[4] = "off";
            solution[5] = "blue";
            solution[6] = "off";
            solution[7] = "red";
            solution[8] = "off";
            solution[9] = "off";
        }
    }

    public void Check()
    {
        bool correct = true;

        for (int i = 0; i < solution.Length; i++)
        {
            if (solution[i] != symbols[i].currentColorString)
            {
                correct = false;
            }
        }

        if (correct)
        {
            winEvent.Invoke();
            foreach (Symbol symbol in symbols)
            {
                symbol.locked = true;
            }
        }
    }
}
