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
        leftText = transform.Find("LeftText").gameObject.GetComponent<Text>();
        rightText = transform.Find("RightText").gameObject.GetComponent<Text>();
        hints = new List<string>();
    }

    // fill up that dang list list
    void AddEntry(string entry)
    {
        hints.Add(entry);
        Display();
    }

    void Display()
    {
        leftText.text = "";
        rightText.text = "";
        foreach (string entry in hints)
        {
            leftText.text += " - " + entry + "\n";
        }
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
        }
    }
}
