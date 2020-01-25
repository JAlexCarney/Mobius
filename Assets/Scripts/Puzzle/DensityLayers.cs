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
    public Sprite empty;

    private Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
    private string[] currentSolution = new string[7];
    private int currentIndex = 0;

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
        sprites.Add("empty", empty);

        EmptyTube();
    }

    // compare corect solution array with current solution array
    // sludges if incorrect (only layers that are wrong)
    private void CheckSolution(string lastLiquid)
    {

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
