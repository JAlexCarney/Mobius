using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DensityLayers : MonoBehaviour
{
    public string[] correctSolution = new string[7];
    public GameObject[] liquidLayersVisual;
    public GameObject mobiusPiece;
    public Sprite saturn;
    public Sprite starMatter;
    public Sprite honey;
    public Sprite mercury;
    public Sprite water;
    public Sprite mapleSyrup;
    public Sprite sludge;
    public Sprite empty;
    public UnityEvent winEvent;

    private Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
    private Dictionary<string, int> nameToLvl = new Dictionary<string, int>();
    private string[] currentSolution;
    private int currentIndex = 0;
    private int solutionIndex = 0;
    private bool canDrain = true;

    // Start is called before the first frame update
    void Start()
    {
        // inistialize sprites dictionary
        sprites.Add("saturn", saturn);
        sprites.Add("starMatter", starMatter);
        sprites.Add("honey", honey);
        sprites.Add("mercury", mercury);
        sprites.Add("water", water);
        sprites.Add("mapleSyrup", mapleSyrup);
        sprites.Add("sludge", sludge);
        sprites.Add("empty", empty);

        currentSolution = new string[correctSolution.Length];

        for (int i = 0; i < correctSolution.Length; i++)
        {
            nameToLvl[correctSolution[i]] = i;
        }

        EmptyTube();
    }

    private void CheckSolutionV2(string lastLiquid)
    {
        // make sure tube isn't already sludged.
        if (currentIndex != 0) { if (currentSolution[currentIndex - 1] == "sludge") { SludgeTube(); return; } }
        //check if the liquid just pored is denser than any of the privious liquids.
        for (int i = currentIndex - 1; i >= 0; i--)
        {
            if (nameToLvl[lastLiquid] < nameToLvl[currentSolution[i]])
            {
                SludgeTube();
                return;
            }
        }
        //Debug.Log(currentIndex);
    }

    // compare corect solution array with current solution array
    // sludges if incorrect (only layers that are wrong)
    private void CheckSolution(string lastLiquid)
    {
        //print("User poured " + lastLiquid + ".");

        // If this is the first liquid poured into the tube.
        if (currentIndex == 0)
        {
            // Set the index for the solution equal to the location of the poured liquid.
            solutionIndex = Array.IndexOf(correctSolution, lastLiquid);
            //print(solutionIndex);

            if (solutionIndex == 6)
            {
                solutionIndex = 0;
            }
            return;
        }

        // Nothing goes on top of the last element.
        if (solutionIndex == 6)
        {
            //print("Incorrect");
            SludgeTube();
        }

        string correctLiquid = correctSolution[solutionIndex + 1];
        string prevLiquid = currentSolution[solutionIndex];
        string prevCorrect = correctSolution[solutionIndex];
        //print("The tube expected " + correctLiquid);

        // If the values don't match, create sludge.
        if (correctLiquid != lastLiquid && prevLiquid != prevCorrect)
        {
            //print("Incorrect");
            SludgeTube();
        }

        // Otherwise increment the solution index to begin checking the next value.
        solutionIndex++;
        //print("New Solution Index: " + solutionIndex);

    }


    void End()
    {
        print("Win");
        winEvent.Invoke();
    }

    bool CompareSolution()
    {
        for (int i = 0; i < currentSolution.Length; i++)
        {
            if (correctSolution[i] != currentSolution[i])
            {
                //Debug.Log(currentSolution[i]);
                //Debug.Log(correctSolution[i]);
                return false;
            }
        }
        return true;
    }

    public void EmptyTube()
    {
        if (canDrain)
        {
            for (int i = 0; i < currentSolution.Length; i++)
            {
                currentSolution[i] = "empty";
            }
            currentIndex = 0;
            UpdateVisual();
        }
    }

    public void SludgeTube()
    {
        for (int i = 0; i <= currentIndex; i++)
        {
            currentSolution[i] = "sludge";
        }
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        int topIndex = 0;
        for (int i = 0; i < currentSolution.Length; i++)
        {
            liquidLayersVisual[i].GetComponent<Image>().sprite = sprites[currentSolution[i]];
            if (currentSolution[i] != "empty")
            {
                topIndex++;
            }
        }
        if (topIndex != 0)
        {
            if (currentSolution[topIndex - 1] != "sludge")
            {
                mobiusPiece.transform.position = liquidLayersVisual[topIndex].transform.position;
            }
            else
            {
                mobiusPiece.transform.position = liquidLayersVisual[0].transform.position;
            }
        }
        else
        {
            mobiusPiece.transform.position = liquidLayersVisual[0].transform.position;
        }
    }

    public void PourLiquid(string liquid)
    {
        if (currentIndex < currentSolution.Length && !Util.ArrayContainsString(currentSolution, liquid))
        {
            currentSolution[currentIndex] = liquid;

            // check the solution
            //CheckSolution(liquid);
            CheckSolutionV2(liquid);

            UpdateVisual();

            if (CompareSolution())
            {
                canDrain = false;
                End();
            }

            currentIndex++;
        }
    }
}
