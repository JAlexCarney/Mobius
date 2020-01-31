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


        //Canvas.ForceUpdateCanvases(); //force update for cachedtext to work IDK lol

        //TextGenerator t = leftText.cachedTextGenerator;
        //Debug.Log("Generated " + t.characterCountVisible + " characters");

        //string result = leftText.text.Substring(0, t.characterCountVisible);
        //Debug.Log("Visible string is: ");
        //Debug.Log(result);

    }

    void Display()
    {
        leftText.text = "";
        //add to text canvas
        foreach (string hint in hints)
        {
            leftText.text += hint + "\n";
        }

    }
    //Text textComp = GetComponent<Text>();
    //TextGenerator t = textComp.cachedTextGenerator;
    //string result = textComp.text.Substring(0, t.characterCountVisible);
    //Debug.Log("Generated " + t.characterCountVisible + " characters");
    //Debug.Log("Visible string is: ");
    //Debug.Log(result);
    // B A S I C A L L Y add entry 
    //then check if left text all filled up
    //if not, add /n + entry 
    //else, add to right
    //refresh?? idek 


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
