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
    private Dictionary<int, List<string>> entryDict = new Dictionary<int, List<string>>();
    private Dictionary<int, List<GameObject>> sketchesDict = new Dictionary<int, List<GameObject>>();
    private List<GameObject> imageHints; 

    private int currentPage;
    private int lastPage = 4; 

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

        //set current page
        currentPage = 1;
    }

    // fill up that dang list list & display indicator
    public void AddEntry(string entry)
    {
        //get page number
        List<string> pageNumberAndEntry = Util.Split(entry, '+');
        int pageNumber = System.Int32.Parse(pageNumberAndEntry[0]);

        Debug.Log(pageNumber);

        //if page doesn't exist yet, add it
        if (!entryDict.ContainsKey(pageNumber))
        {
            entryDict.Add(pageNumber, new List<string>());
            sketchesDict.Add(pageNumber, new List<GameObject>());
        }

        //get list of entries inpage
        List<string> pageEntries = entryDict[pageNumber];

        //add entry to page if it is not a duplicate
        if (!pageEntries.Contains(entry))
        {
            pageEntries.Add(pageNumberAndEntry[1]);
            IndicateEntry();
            RefreshJournal();
        }
    }

    //add image to the list
    public void AddImage(GameObject image)
    {
        //get page number
        int pageNumber = image.GetComponent<ImageHint>().pageNumber;

        //if page doesn't exist yet, add it
        if (!entryDict.ContainsKey(pageNumber))
        {
            entryDict.Add(pageNumber, new List<string>());
            sketchesDict.Add(pageNumber, new List<GameObject>());
        }

        //get list of images in that page
        List<GameObject> pageSketches = sketchesDict[pageNumber];

        //add image to page if it is not a duplicate
        if (!pageSketches.Contains(image))
        {
            //!!important!! instatiate in rightPage!!!!
            GameObject instantiatedImage = Instantiate(image, rightPage.transform);
            
            pageSketches.Add(instantiatedImage);

            IndicateEntry();
            RefreshJournal();
        }
    }

    public void FlipLeft(GameObject leftButton)
    {
        //get left page
        currentPage--;
        Debug.Log("hahahha");
        RefreshJournal();

        //grey out / disable left button if at end

    }

    public void FlipRight(GameObject rightButton)
    {
        currentPage++;

        Debug.Log("HAHAHAH");
        RefreshJournal();

        //grey out / disable right button if at end 

    }

    //update the journal to display the current page
    public void RefreshJournal()
    {
        //empty left text
        leftPage.text = "";

        //deactivate the open page of images (GetComponents only gets active children!)
        ImageHint[] images = rightPage.GetComponentsInChildren<ImageHint>();
        foreach (ImageHint child in images)
        {
            Debug.Log(child.name);
            child.gameObject.SetActive(false);
        }

        //get gameObjects and String of left/right page
        List<string> refreshedText = entryDict[currentPage];
        List<GameObject> refreshedImages = sketchesDict[currentPage];

        //add refreshed text to left page
        foreach (string text in refreshedText)
        {
            Debug.Log(text);

            //add hint to text
            leftPage.text += text;

            //Canvas.ForceUpdateCanvases();

            //add new line
            leftPage.text += "\n";

            Debug.Log(leftPage.text);
        }

        //activate images
        foreach (GameObject image in refreshedImages)
        {
            Debug.Log(image.name);
            image.SetActive(true);
        }
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

    // make journal visible
    public void Open()
    {
        //use canvas swapper to open the journal
        GameObject.Find("CanvasSwapper").GetComponent<CanvasSwapper>().SwitchCanvasNoUI("JournalCanvas");
    }
}