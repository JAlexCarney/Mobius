using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DensityLayers : MonoBehaviour
{
    public string[] correctSolution = new string[7];
    public GameObject[] liquidLayersVisual;
    public Sprite milk;
    public Sprite oil;
    public Sprite honey;
    public Sprite soap;
    public Sprite gas;
    public Sprite water;
    public Sprite mapleSyrup;
    public Sprite sludge;
    public Sprite empty;

    private Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
    private string[] currentSolution = new string[7];
    private int currentIndex = 0;
    private int solutionIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // inistialize sprites dictionary
        sprites.Add("milk", milk);
        sprites.Add("oil", oil);
        sprites.Add("honey", honey);
        sprites.Add("soap", soap);
        sprites.Add("gas", gas);
        sprites.Add("water", water);
        sprites.Add("mapleSyrup", mapleSyrup);
        sprites.Add("sludge", sludge);
        sprites.Add("empty", empty);

        EmptyTube();
    }

    // compare corect solution array with current solution array
    // sludges if incorrect (only layers that are wrong)
    private void CheckSolution(string lastLiquid)
    {
      print("User poured " + lastLiquid + ".");

      // If this is the first liquid poured into the tube.
      if(currentIndex == 0)
      {
        // Set the index for the solution equal to the location of the poured liquid.
        solutionIndex = Array.IndexOf(correctSolution, lastLiquid);
        print(solutionIndex);

        if (solutionIndex == 6)
        {
          solutionIndex = 0;
        }
        return;
      }

      // Nothing goes on top of the last element.
      if (solutionIndex == 6)
      {
        print("Incorrect");
        SludgeTube();
      }

      string correctLiquid = correctSolution[solutionIndex+1];
      string prevLiquid = currentSolution[solutionIndex];
      string prevCorrect = correctSolution[solutionIndex];
      print("The tube expected " + correctLiquid);

      // If the values don't match, create sludge.
      if (correctLiquid != lastLiquid && prevLiquid != prevCorrect)
      {
        print("Incorrect");
        SludgeTube();
      }

      // Otherwise increment the solution index to begin checking the next value.
      solutionIndex++;
      print("New Solution Index: " + solutionIndex);

    }

    public void EmptyTube()
    {
        for (int i = 0; i < currentSolution.Length; i++)
        {
            currentSolution[i] = "empty";
        }
        currentIndex = 0;
        UpdateVisual();
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
        for(int i = 0; i < currentSolution.Length; i++)
        {
            liquidLayersVisual[i].GetComponent<Image>().sprite = sprites[currentSolution[i]];
        }
    }

    public void PourLiquid(string liquid)
    {
        if (currentIndex < currentSolution.Length)
        {
            currentSolution[currentIndex] = liquid;

            // check the solution
            CheckSolution(liquid);

            UpdateVisual();

            currentIndex++;
        }
    }
}
