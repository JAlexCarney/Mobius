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
            Debug.Log(t.gameObject.name);
            string name = t.gameObject.name; 
            if (t != null && name.Equals("LeftText"))
                leftText = t;
            else if (t != null && name.Equals("RightText"))
                rightText = t; 
        }

        Debug.Log(leftText);
        Debug.Log(rightText);

        hints = new List<string>();
    }

    // fill up that dang list list
    public void AddEntry(string entry)
    {
        hints.Add(entry);
        Debug.Log(leftText);
        Text textComp = leftText.GetComponent<Text>();
        
        TextGenerator t = textComp.cachedTextGenerator;
        string result = textComp.text.Substring(0, t.characterCountVisible);
        Debug.Log("Generated " + t.characterCountVisible + " characters");
        Debug.Log("Visible string is: ");
        Debug.Log(result);

    }

    void Display()
    {
        Debug.Log("hahahahhaaha");
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
    void Open()
    {
        // Display
    }
}
