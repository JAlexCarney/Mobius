using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalHandler : MonoBehaviour
{
    //List<List<string>>
    public GameObject IndicatorPrefab;
    //private Text leftText;
    //private Text rightText;
    private List<string> hints;

    public GameObject journalNodePrefab = null;  
    private GameObject[][] JournalNodes = null;

    private void Start()
    {
        // get parent folder for node objects
        GameObject nodes = transform.Find("Nodes").gameObject;

        // initialize nodes array
        JournalNodes = new GameObject[6][];
        for (int i = 0; i < JournalNodes.Length; i++)
        {
            JournalNodes[i] = new GameObject[7];
        }

        // initialize constants
        float nodeWidth = nodes.GetComponent<RectTransform>().sizeDelta.x/ JournalNodes.Length;
        float nodeHeight = nodes.GetComponent<RectTransform>().sizeDelta.y/ JournalNodes[0].Length;

        // reset x to left of canvas
        float x =  0;
        
        // create journal entry nodes
        for (int i = 0; i < JournalNodes.Length; i++)
        {
            // reset y value to top of canvas
            float y = 0;
            for (int j = 0; j < JournalNodes[i].Length; j++)
            {
                // create a temporary prefab node
                GameObject tmp = Instantiate(journalNodePrefab);

                // Get the rectTransform object
                RectTransform rectT = tmp.GetComponent<RectTransform>();

                // set the parent canvas of the node
                tmp.transform.parent = nodes.transform;

                // Set the created nodes position
                rectT.anchoredPosition = new Vector2(x, y);

                // return to original scale
                rectT.localScale = new Vector3(1f, 1f, 1f);

                // make clear
                tmp.GetComponent<Image>().color = Color.clear;

                // Set the size of the nodes
                rectT.sizeDelta = new Vector2(nodeWidth, nodeHeight);
                
                JournalNodes[i][j] = tmp;
                y -= nodeHeight;
            }
            x += nodeWidth;
        }
        /*
        GameObject tmp = Instantiate(journalNodePrefab);
        tmp.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        tmp.GetComponent<RectTransform>().sizeDelta = new Vector2(nodeWidth, nodeHeight);
        tmp.transform.parent = transform;
        */
    }

    // fill up that dang list list
    public void AddEntry(string entry)
    {
        
    }

    void Display()
    {
        
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
            textComponent.text += " - " + hint;

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
