using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class JournalHandler : MonoBehaviour
{
    // variables for journal indication
    public Sprite journalButtonImg;
    public Sprite journalButtonNewMsgImg;
    private GameObject journalButton;
    public GameObject IndicatorPrefab;
    public SoundManager soundManager;
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject OpenAnim;
    public GameObject FlipAnim; //forwards is left, backwards is right.
    private List<string> showing;
    private Dictionary<string, GameObject> entryObjs;
    private int currentSpread = 0;
    private int spreadCount = 4;
    private bool[] spreadsToSee;

    // Start is called before the first frame update
    void Start()
    {
        journalButton = GameObject.Find("InventoryManager").transform.GetChild(2).gameObject;
        showing = new List<string>();
        entryObjs = new Dictionary<string, GameObject>();
        spreadsToSee = new bool[13];
        for (int i = 0; i < spreadsToSee.Length; i++)
        {
            spreadsToSee[i] = false;
        }
    }

    // display indicator that an entry has been placed
    public void IndicateEntry(int page)
    {
        // Instantiate(prefab)
        soundManager.Play("penScratch");
        GameObject indicator = Instantiate(IndicatorPrefab);
        indicator.GetComponent<PageFinder>().page = page;
        Animation anim = indicator.GetComponent<Animation>();
        anim.Play();
        Destroy(indicator, 4f);

        // update journal open button to reflect new information
        journalButton.GetComponent<Image>().sprite = journalButtonNewMsgImg;
        journalButton.GetComponent<Animation>().Play();
    }

    public void Show(string names)
    {
        int page = -1;
        bool newEntry = false;
        if (names.Contains("+"))
        {
            List<string> namesSplit = Util.Split(names, '+');
            foreach (string name in namesSplit)
            {
                if (!showing.Contains(name))
                {
                    showing.Add(name);
                    newEntry = true;
                    page = FindNewEntry(name);
                }
            }
            
        }
        else
        {
            if (!showing.Contains(names))
            {
                newEntry = true;
                showing.Add(names);
                page = FindNewEntry(names);
            }
        }

        Debug.Log("PAGE FOUND = " + page);

        if (newEntry == true)
        {
            IndicateEntry(page);
        }

        RefreshJournal();

        // TURN THOSE FUCKING ARROWS OFF GOD DAMMMMMIIITTT
        leftButton.SetActive(false);
        rightButton.SetActive(false);
    }

    public void FlipLeft()
    {
        currentSpread--;
        spreadsToSee[currentSpread] = false;
        Animation anim = FlipAnim.GetComponent<Animation>();
        AnimationState flip = anim["journalFlip"];
        anim.Play();
        flip.speed = 1;
        flip.time = 0;
        Invoke("RefreshJournal", flip.length/2);
        soundManager.Play("pageTurn");
    }

    public void FlipRight()
    {
        currentSpread++;
        spreadsToSee[currentSpread] = false;
        Animation anim = FlipAnim.GetComponent<Animation>();
        AnimationState flip = anim["journalFlip"];
        Debug.Log(flip);
        anim.Play();
        flip.speed = -1;
        flip.time = flip.length;
        Invoke("RefreshJournal", flip.length/2);
        soundManager.Play("pageTurn");
    }

    public void JumpToPage(int spread)
    {
        if (currentSpread < spread)
        {
            currentSpread = spread;
            Animation anim = FlipAnim.GetComponent<Animation>();
            AnimationState flip = anim["journalFlip"];
            anim.Play();
            flip.speed = -1;
            flip.time = flip.length;
            Invoke("RefreshJournal", flip.length / 2);
            soundManager.Play("pageTurn");
        }
        else if (currentSpread > spread)
        {
            currentSpread = spread;
            Animation anim = FlipAnim.GetComponent<Animation>();
            AnimationState flip = anim["journalFlip"];
            Debug.Log(flip);
            anim.Play();
            flip.speed = -1;
            flip.time = flip.length;
            Invoke("RefreshJournal", flip.length / 2);
            soundManager.Play("pageTurn");
        }
    }

    public int FindNewEntry(string name)
    {
        Debug.Log("Correct Name = " + name);
        Debug.Log("spreadCount : " + spreadCount);
        for (int i = 0; i < spreadCount; i++)
        {
            GameObject spread = transform.Find("Spread" + i).gameObject;
            for (int j = 0; j < spread.transform.childCount; j++)
            {
                Transform draggable = spread.transform.GetChild(0);
                for (int k = 0; k < draggable.childCount; k++)
                {
                    GameObject entry = draggable.transform.GetChild(k).gameObject;
                    Debug.Log(entry.name);
                    if (name == entry.name)
                    {
                        return i;
                    }
                }
                Transform nonDraggable = spread.transform.GetChild(1);
                for (int k = 0; k < nonDraggable.childCount; k++)
                {
                    GameObject entry = nonDraggable.transform.GetChild(k).gameObject;
                    Debug.Log(entry.name);
                    if (name == entry.name)
                    {
                        return i;
                    }
                }
            }
            Util.DeactivateChildren(spread);
        }
        Debug.Log("ENTRY NOT FOUND");
        return -1;
    }

    //update the journal to display the current page
    public void RefreshJournal()
    {
        for (int i = 0; i < spreadCount; i++)
        {
            Debug.Log("Spread" + i);
            Util.DeactivateChildren(transform.Find("Spread" + i).gameObject);
        }
        Transform spread = transform.Find("Spread" + currentSpread);
        Util.ActivateChildren(spread.gameObject);
        Transform draggable = spread.transform.GetChild(0);
        for (int k = 0; k < draggable.childCount; k++)
        {
            GameObject entry = draggable.transform.GetChild(k).gameObject;
            if (!showing.Contains(entry.name))
            {
                entry.SetActive(false);
            }
            else
            {
                entry.SetActive(true);
            }
        }
        Transform nonDraggable = spread.transform.GetChild(1);
        for (int k = 0; k < nonDraggable.childCount; k++)
        {
            GameObject entry = nonDraggable.transform.GetChild(k).gameObject;
            if (!showing.Contains(entry.name))
            {
                entry.SetActive(false);
            }
            else
            {
                entry.SetActive(true);
            }
        }
        if (currentSpread == 0)
        {
            leftButton.SetActive(false);
            rightButton.SetActive(true);
        }
        else if (currentSpread == spreadCount - 1)
        {
            leftButton.SetActive(true);
            rightButton.SetActive(false);
        }
        else
        {
            leftButton.SetActive(true);
            rightButton.SetActive(true);
        }

        bool spreadToSee = false;
        for (int i = 0; i < spreadsToSee.Length; i++)
        {
            if (spreadsToSee[i])
            {
                spreadToSee = true;
            }
        }
        if (!spreadToSee)
        {
            journalButton.GetComponent<Image>().sprite = journalButtonImg;
            journalButton.GetComponent<Animation>().Stop();
        }
        else
        {
            journalButton.GetComponent<Image>().sprite = journalButtonNewMsgImg;
            journalButton.GetComponent<Animation>().Play();
        }
    }

    // make journal visible
    public void Open()
    {
        GameObject.Find("InventoryManager").GetComponent<InventoryHandler>().ResetPositions();
        Debug.Log("Journal entries showing : ");
        foreach (string shown in showing)
        {
            Debug.Log(shown);
        }

        spreadsToSee[currentSpread] = false;

        //RefreshJournal();

        Animation anim = OpenAnim.GetComponent<Animation>();
        anim.Play();
        anim["journalOpen"].speed = 1;
        anim["journalOpen"].time = 0;
        GameObject.Find("CanvasSwapper").GetComponent<CanvasSwapper>().DisableUI();
        Invoke("PlayOpeningSound", 0.333f);
        Invoke("FinishOpen", 1.0f);
    }

    public void OpenToPage(int page)
    {
        currentSpread = page;

        Debug.Log("Journal entries showing : ");
        foreach (string shown in showing)
        {
            Debug.Log(shown);
        }

        spreadsToSee[currentSpread] = false;

        //RefreshJournal();

        Animation anim = OpenAnim.GetComponent<Animation>();
        anim.Play();
        anim["journalOpen"].speed = 1;
        anim["journalOpen"].time = 0;
        GameObject.Find("CanvasSwapper").GetComponent<CanvasSwapper>().DisableUI();
        Invoke("PlayOpeningSound", 0.333f);
        Invoke("FinishOpen", 1.0f);
    }

    public void Close()
    {
        Animation anim = OpenAnim.GetComponent<Animation>();
        anim.Play();
        anim["journalOpen"].speed = -1;
        anim["journalOpen"].time = anim["journalOpen"].length;
        soundManager.Play("journalOpen");
        Invoke("FinishClose", 0.08f);
    }

    public void PlayOpeningSound()
    {
        soundManager.Play("journalOpen");
    }

    public void FinishOpen()
    {
        //use canvas swapper to open the journal
        GameObject.Find("CanvasSwapper").GetComponent<CanvasSwapper>().OpenJournal();
        //OpenAnim.GetComponent<Animation>().Stop();

        RefreshJournal();
    }

    public void FinishClose()
    {
        GameObject.Find("CanvasSwapper").GetComponent<CanvasSwapper>().CloseJournal();
        //OpenAnim.GetComponent<Animation>().Stop();
    }

    public void ShowLoopOne()
    {
        List<GameObject> spreadsToShow = new List<GameObject>
        {
            transform.GetChild(5).gameObject,
            transform.GetChild(6).gameObject,
            transform.GetChild(7).gameObject,
            transform.GetChild(8).gameObject
        };
        string names = "";
        foreach (GameObject spread in spreadsToShow)
        {
            for (int i = 0; i < spread.transform.childCount; i++)
            {
                Transform t = spread.transform.GetChild(i);
                for (int j = 0; j < t.childCount; j++)
                {
                    if (names != "")
                    {
                        names += "+";
                    }

                    names += t.GetChild(j).name;
                }
            }
        }
        spreadCount = 8;
        Show(names);
        
    }

    public void ShowLoopTwo()
    {
        List<GameObject> spreadsToShow = new List<GameObject>
        {
            transform.GetChild(5).gameObject,
            transform.GetChild(6).gameObject,
            transform.GetChild(7).gameObject,
            transform.GetChild(8).gameObject,
            transform.GetChild(9).gameObject,
            transform.GetChild(10).gameObject,
            transform.GetChild(11).gameObject,
            transform.GetChild(12).gameObject
        };
        string names = "";
        foreach (GameObject spread in spreadsToShow)
        {
            for (int i = 0; i < spread.transform.childCount; i++)
            {
                Transform t = spread.transform.GetChild(i);
                for (int j = 0; j < t.childCount; j++)
                {
                    if (names != "")
                    {
                        names += "+";
                    }

                    names += t.GetChild(j).name;
                }
            }
        }
        spreadCount = 12;
        Show(names);
    }
}