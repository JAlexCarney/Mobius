using System.Collections;
using System.Collections.Generic;
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
    private List<string> showing;
    private Dictionary<string, GameObject> entryObjs;
    private int currentSpread = 0;
    private readonly int spreadCount = 3;
    private bool[] spreadsToSee;

    // Start is called before the first frame update
    void Start()
    {
        journalButton = GameObject.Find("InventoryManager").transform.GetChild(2).gameObject;
        showing = new List<string>();
        entryObjs = new Dictionary<string, GameObject>();
        spreadsToSee = new bool[spreadCount];
        for (int i = 0; i < spreadsToSee.Length; i++)
        {
            spreadsToSee[i] = false;
        }
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

        // update journal open button to reflect new information
        journalButton.GetComponent<Image>().sprite = journalButtonNewMsgImg;
        journalButton.GetComponent<Animation>().Play();
    }

    public void Show(string names)
    {
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
                    FindNewEntry(name);
                }
            }
            
        }
        else
        {
            if (!showing.Contains(names))
            {
                newEntry = true;
                showing.Add(names);
                FindNewEntry(name);
            }
        }

        if (newEntry == true)
        {
            IndicateEntry();
        }

        RefreshJournal();
    }

    public void FlipLeft()
    {
        currentSpread--;
        spreadsToSee[currentSpread] = false;
        RefreshJournal();
        soundManager.Play("pageTurn");
    }

    public void FlipRight()
    {
        currentSpread++;
        spreadsToSee[currentSpread] = false;
        RefreshJournal();
        soundManager.Play("pageTurn");
    }

    public void FindNewEntry(string name)
    {
        for (int i = 0; i < spreadCount; i++)
        {
            GameObject spread = transform.Find("Spread" + i).gameObject;
            for (int j = 0; j < spread.transform.childCount; j++)
            {
                Transform draggable = spread.transform.GetChild(0);
                for (int k = 0; k < draggable.childCount; k++)
                {
                    GameObject entry = draggable.transform.GetChild(k).gameObject;
                    if (name == entry.name)
                    {
                        spreadsToSee[i] = true;
                        return;
                    }
                }
                Transform nonDraggable = spread.transform.GetChild(1);
                for (int k = 0; k < nonDraggable.childCount; k++)
                {
                    GameObject entry = nonDraggable.transform.GetChild(k).gameObject;
                    if (name == entry.name)
                    {
                        spreadsToSee[i] = true;
                        return;
                    }
                }
            }
            Util.DeactivateChildren(spread);
        }
    }

    //update the journal to display the current page
    public void RefreshJournal()
    {
        for (int i = 0; i < spreadCount; i++)
        {
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
        Debug.Log("Journal entries showing : ");
        foreach (string shown in showing)
        {
            Debug.Log(shown);
        }

        spreadsToSee[currentSpread] = false;

        RefreshJournal();

        soundManager.Play("journalOpen");

        //use canvas swapper to open the journal
        GameObject.Find("CanvasSwapper").GetComponent<CanvasSwapper>().OpenJournal();

        RefreshJournal();
    }
}