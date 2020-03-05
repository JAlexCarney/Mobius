using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalHandler : MonoBehaviour
{
    //List<List<string>>
    public GameObject IndicatorPrefab;
    public SoundManager soundManager;
    private Text leftPage;
    private RectTransform rightPage;
    private Dictionary<int, List<string>> entryDict = new Dictionary<int, List<string>>();
    private Dictionary<int, List<GameObject>> sketchesDict = new Dictionary<int, List<GameObject>>();
    private List<GameObject> imageHints; 

    private int currentPage;
    private int lastPage;

    public Transform bottomLeftCornerRightPage;
    public Transform topRightCornerRightPage;

    public Button leftButton;
    public Button rightButton;

    public GameObject intro;
    public GameObject map;

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

        //set current & last page
        currentPage = 1;
        lastPage = 1;

        //add first page
        addPage(currentPage);

        //add map & narrative intro to first page
        List<GameObject> firstPage = sketchesDict[currentPage];

        firstPage.Add(intro);
        firstPage.Add(map);

        Debug.Log("intro added");
    }

    // fill up that dang list list & display indicator
    public void AddEntry(string entry)
    {
        //get page number
        List<string> pageNumberAndEntry = Util.Split(entry, '+');
        int pageNumber = System.Int32.Parse(pageNumberAndEntry[0]);

        //if page doesn't exist yet, add it
        addPage(pageNumber);

        //get list of entries inpage
        List<string> pageEntries = entryDict[pageNumber];

        //add entry to page if it is not a duplicate
        if (!pageEntries.Contains(pageNumberAndEntry[1]))
        {
            pageEntries.Add(pageNumberAndEntry[1]);
            IndicateEntry();
        }
    }

    //add image to the list
    public void AddImage(GameObject image)
    {
        //get page number
        int pageNumber = image.GetComponent<ImageHint>().pageNumber;

        //if page doesn't exist yet, add it
        addPage(pageNumber);

        //get list of images in that page
        List<GameObject> pageSketches = sketchesDict[pageNumber];

        //add image to page if it is not a duplicate
        bool exists = false; 
        foreach(GameObject sketch in pageSketches)
        {
            if (sketch.name == (image.name + "(Clone)"))
                exists = true;
        }
        if (!exists)
        {
            //instantiate & make a child of rightPage
            GameObject instantiatedImage = Instantiate(image, rightPage.transform);

            //The weird fucking algorithm to determine the best spot to place an image lmao
            RectTransform imageRT = instantiatedImage.GetComponent<RectTransform>();

            float maxXPosition = topRightCornerRightPage.position.x;
            float maxYPosition = bottomLeftCornerRightPage.position.y;

            //if not in good position, just give TF up and put it somewhere random lol
            bool inGoodPosition = true;
            for (int i = 0; i < 20; i++)
            {
                inGoodPosition = true;
                //float randomXPosition = Random.Range(bottomLeftCornerRightPage.position.x, maxXPosition);
                //float randomYPosition = Random.Range(topRightCornerRightPage.position.y, maxYPosition);
                imageRT.transform.position = new Vector2(Random.Range(bottomLeftCornerRightPage.position.x, maxXPosition),
                    Random.Range(topRightCornerRightPage.position.y, maxYPosition));
                foreach (GameObject children in pageSketches)
                {
                    if (RectOverlaps(children.GetComponent<RectTransform>(), imageRT))
                    {
                        inGoodPosition = false;
                    }
                }
                if (inGoodPosition)
                    break;
            }

            //FUCK this shit!!!!!!!!!!!!!!!!!!!!!
            //bool inGoodPosition = true; 
            //for (float i = bottomLeftCornerRightPage.position.x; i < maxXPosition; i += 10)
            //{
            //    inGoodPosition = true; 
            //    for (float j = topRightCornerRightPage.position.y; j > maxYPosition; j -= 10)
            //    {
            //        imageRT.transform.position = new Vector2(i, j);
            //        foreach (GameObject children in pageSketches)
            //        {
            //            if (RectOverlaps(children.GetComponent<RectTransform>(), imageRT))
            //            {
            //                inGoodPosition = false; 
            //            }
            //        }
            //        if (inGoodPosition)
            //            break;
            //    }
            //    if (inGoodPosition)
            //        break;
            //}
            //Debug.Log(imageRT.transform.position);


            //save instantiated GameObject to list
            pageSketches.Add(instantiatedImage);

            IndicateEntry();
        }
    }

    //add a new page
    public void addPage(int pageNumber)
    {
        if (!entryDict.ContainsKey(pageNumber))
        {
            lastPage = System.Math.Max(lastPage, pageNumber);
            entryDict.Add(pageNumber, new List<string>());
            sketchesDict.Add(pageNumber, new List<GameObject>());
        }
    }

    //from https://stackoverflow.com/questions/42043017/check-if-ui-elements-recttransform-are-overlapping
    bool RectOverlaps(RectTransform rectTrans1, RectTransform rectTrans2)
    {
        Rect rect1 = new Rect(rectTrans1.localPosition.x, rectTrans1.localPosition.y, rectTrans1.rect.width, rectTrans1.rect.height);
        Rect rect2 = new Rect(rectTrans2.localPosition.x, rectTrans2.localPosition.y, rectTrans2.rect.width, rectTrans2.rect.height);
        return rect1.Overlaps(rect2);
    }

    public void FlipLeft()
    {
        //get left page
        currentPage--;
        RefreshJournal();
        soundManager.Play("pageTurn");
    }

    public void FlipRight()
    {
        currentPage++;
        Debug.Log("flip right!");
        soundManager.Play("pageTurn");
        RefreshJournal();
    }

    //update the journal to display the current page
    public void RefreshJournal()
    {
        Debug.Log(currentPage);
        //empty left text
        leftPage.text = "";

        //deactivate the open pages of images (GetComponents only gets active children!)
        ImageHint[] images = GetComponentsInChildren<ImageHint>();
        foreach (ImageHint child in images)
        {
            Debug.Log("DELETE: " + child.name);
            child.gameObject.SetActive(false);
        }

        ImageHint[] rightPageImages = this.rightPage.GetComponentsInChildren<ImageHint>();
        foreach (ImageHint child in rightPageImages)
        {
            Debug.Log("right page DELETE: " + child.name);
            child.gameObject.SetActive(false);
        }

        //get gameObjects and String of left/right page
        List<string> refreshedText = entryDict[currentPage];
        List<GameObject> refreshedImages = sketchesDict[currentPage];
        //Debug.Log("page " + 1 + ": ");
        //foreach (GameObject image in sketchesDict[1])
        //    Debug.Log(image.name);
        //Debug.Log("page " + 2 + ": ");
        //foreach (GameObject image in sketchesDict[2])
        //    Debug.Log(image.name);
        //add refreshed text to left page
        foreach (string text in refreshedText)
        {
            //Debug.Log(text);
            //add hint to text
            leftPage.text += text;

            //Canvas.ForceUpdateCanvases();

            //add new line
            leftPage.text += "\n";
        }

        //activate images
        foreach (GameObject image in refreshedImages)
        {
            image.SetActive(true);
        }

        //deactivate buttons based on page number (edge case for if only 1 page exists)
        rightButton.interactable = (currentPage < lastPage);
        leftButton.interactable = (currentPage > 1);
    }


    // display indicator that an entry has been placed
    public void IndicateEntry()
    {
        // Instantiate(prefab)
        soundManager.Play("penScratch");
        GameObject indicator = Instantiate(IndicatorPrefab);
        Animation anim = indicator.GetComponent<Animation>();
        anim.Play();
        Destroy(indicator, anim.GetClip("JournalEntry").averageDuration);
    }

    // make journal visible
    public void Open()
    {
        RefreshJournal();

        // Return to first Page
        while (currentPage != lastPage)
        {
            currentPage++;
            RefreshJournal();
        }
        while (currentPage != 1)
        {
            currentPage--;
            RefreshJournal();
        }

        soundManager.Play("journalOpen");
        
        //use canvas swapper to open the journal
        GameObject.Find("CanvasSwapper").GetComponent<CanvasSwapper>().OpenJournal();
    }
}