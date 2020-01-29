using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalHandler : MonoBehaviour
{
    //List<List<string>>
    public GameObject IndicatorPrefab;
    private GameObject leftText;
    private GameObject rightText;
    private List<string> hints;

    private void Start()
    {
        leftText = GameObject.Find("LeftText");
        rightText = GameObject.Find("RightText");
        hints = new List<string>();
    }

    // fill up that dang list list
    void AddEntry(string entry)
    {
        hints.Add(entry);
    }

    void Display()
    {

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
