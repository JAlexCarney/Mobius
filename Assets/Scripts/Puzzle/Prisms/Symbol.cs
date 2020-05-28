using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Symbol : MonoBehaviour
{
    public string currentColorString = "off";
    private int currentColorInt = 0;

    public Sprite off;
    public Sprite red;
    public Sprite green;
    public Sprite blue;
    public Sprite yellow;
    public Sprite cyan;
    public Sprite magenta;

    public bool locked = false;

    private string[] colors = new string[7];
    private Sprite[] colorSprites = new Sprite[7];
    
    // Start is called before the first frame update
    void Start()
    {
        currentColorString = "off";
        currentColorInt = 0;

        colors[0] = "off";
        colorSprites[0] = off;

        colors[1] = "red";
        colorSprites[1] = red;

        colors[2] = "yellow";
        colorSprites[2] = yellow;

        colors[3] = "green";
        colorSprites[3] = green;

        colors[4] = "cyan";
        colorSprites[4] = cyan;

        colors[5] = "blue";
        colorSprites[5] = blue;

        colors[6] = "magenta";
        colorSprites[6] = magenta;
        
    }

    public void Toggle()
    {
        if (!locked)
        {
            currentColorInt = (currentColorInt + 1) % 7;
            currentColorString = colors[currentColorInt];
            GetComponent<Image>().sprite = colorSprites[currentColorInt];
            GetComponentInParent<SymbolCodeCheck>().Check();
        }
    }
}
