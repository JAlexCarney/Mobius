using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalHandler : MonoBehaviour
{
    //List<List<string>>
    public GameObject IndicatorPrefab;
    private Text leftText;
    private Text rightText;
    private List<string> hints;

    private void Start()
    {
        //get text children of the journal canvas
        //unity can't find inactive children using Find() :(
        Text[] ts = gameObject.GetComponentsInChildren<Text>(true);
        foreach (Text t in ts)
        {
            string name = t.gameObject.name;
            if (t != null && name.Equals("LeftText"))
            {
                leftText = t;
            }
            else if (t != null && name.Equals("RightText"))
            {
                rightText = t;
            }
        }

        hints = new List<string>();
    }

    // fill up that dang list list
    public void AddEntry(string entry)
    {
        hints.Add(entry);
    }

    void Display()
    {
        int hintIndex = 0;

        //add hints to leftText
        hintIndex = AddHintsToTextComponent(leftText, hintIndex);

        //add leftover hints to rightText
        AddHintsToTextComponent(rightText, hintIndex);
    }

    /// <summary>
    /// This adds hints to the param TextComponent until there is no more room to display hints. 
    /// </summary>
    /// <param name="textComponent">leftText or rightText</param>
    /// <param name="hintIndex">The index to start at in hints</param>
    /// <returns>The index of the next hint to display after textComponent is filled.
    /// Returns hints.length + 1 if the end of the hints list is reached.</returns>
    int AddHintsToTextComponent(Text textComponent, int hintIndex)
    {
        textComponent.text = ""; 

        for (; hintIndex < hints.Count; hintIndex++)
        {
            string hint = hints[hintIndex];

            //add hint to text
            textComponent.text += hint;

            //Update everything so things don't break below lol
            Canvas.ForceUpdateCanvases();

            //this helps u check what is currently being displayed (ty google)
            TextGenerator t = textComponent.cachedTextGenerator;

            //if the hint isn't fully displayed, remove it and stop adding to textComponent
            int textLength = textComponent.text.Length;
            if (t.characterCountVisible < textLength)
            {
                textComponent.text = textComponent.text.Remove(textLength - hint.Length);
                break;
            }

            //add new line
            textComponent.text += "\n";

        }
        return hintIndex; 
    }

    // display indicator that an entry has been placed
    public void IndicateEntry(string entry)
    {
        // Instantiate(prefab)
        if (!hints.Contains(entry))
        {
            GameObject indicator = Instantiate(IndicatorPrefab);
            Animation anim = indicator.GetComponent<Animation>();
            anim.Play();
            Destroy(indicator, anim.GetClip("JournalEntry").averageDuration);
            AddEntry(entry);
            Debug.Log(anim.GetClip("JournalEntry").averageDuration);
            Debug.Log(entry);
        }
    }

    // make journal invisible
    void Close()
    {

    }

    // make journal visible
    public void Open()
    {
        //use canvas swapper to open the journal
        GameObject.Find("CanvasSwapper").GetComponent<CanvasSwapper>().SwitchCanvasNoUI("JournalCanvas");


        //call Display
        Display();
        //^I am doing this here since I haven't found a way to check if numHints > spaceAllocatedForText in an inactive canvas
        //since inactive canvases don't update
        //if there is an easier way please lmk i am n00b who desperately googles things
    }
}
