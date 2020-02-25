using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalHandler : MonoBehaviour
{
    //List<List<string>>
    public GameObject IndicatorPrefab;
    private Text leftPage;
    private RectTransform rightPage;
    private List<string> hints;
    private List<GameObject> imageHints; 

    private void Start()
    {
        //get text children of the journal canvas
        //unity can't find inactive children using Find() :(
        Text ts = gameObject.GetComponentInChildren<Text>(true);
        if (ts != null && ts.gameObject.name.Equals("LeftPage"))
        {
            leftPage = ts;
        }

        //get right page (rect transform bc it holds images)
        RectTransform[] rt = gameObject.GetComponentsInChildren<RectTransform>(true);
        foreach (RectTransform rtChild in rt)
        {
            if (rtChild != null && rtChild.gameObject.name.Equals("RightPage"))
            {
                rightPage = rtChild;
            }

        }
        hints = new List<string>();
        imageHints = new List<GameObject>();
    }

    // fill up that dang list list & display indicator
    public void AddEntry(string entry)
    {
        if (!hints.Contains(entry))
        {
            hints.Add(entry);
            IndicateEntry();
            Display();
        }
   
    }

    //add image to the list
    public void AddImage(GameObject image)
    {
        if (!imageHints.Contains(image))
        {
            imageHints.Add(image);
            IndicateEntry();
            Instantiate(image, rightPage.GetComponent<Transform>());
        }
    }

    void Display()
    {
        int hintIndex = 0;

        //add hints to leftText
        hintIndex = AddHintsToTextComponent(leftPage, hintIndex);

        //add leftover hints to rightText
        //AddHintsToTextComponent(rightPage, hintIndex);
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
    public void IndicateEntry()
    {
        // Instantiate(prefab)
        GameObject indicator = Instantiate(IndicatorPrefab);
        Animation anim = indicator.GetComponent<Animation>();
        anim.Play();
        Destroy(indicator, anim.GetClip("JournalEntry").averageDuration);
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