using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalManager : MonoBehaviour
{
    // variables for journal indication
    public Sprite journalButtonImg;
    public Sprite journalButtonNewMsgImg;
    private GameObject journalButton;
    public GameObject IndicatorPrefab;
    public SoundManager soundManager;
    private List<string> showing;
    private Dictionary<string, GameObject> entryObjs;
    private int currentSpread = 0;
    private readonly int spreadCount = 2;

    // Start is called before the first frame update
    void Start()
    {
        journalButton = GameObject.Find("InventoryManager").transform.GetChild(2).gameObject;
        showing = new List<string>();
        entryObjs = new Dictionary<string, GameObject>();

        //InitializeJournal();
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

    public void Show(string name)
    {
        showing.Add(name);
        RefreshJournal();
    }

    public void FlipLeft()
    {
        currentSpread--;
        RefreshJournal();
        soundManager.Play("pageTurn");
    }

    public void FlipRight()
    {
        currentSpread++;
        RefreshJournal();
        soundManager.Play("pageTurn");
    }

    public void InitializeJournal()
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
                    entryObjs.Add(entry.name, entry);
                }
                Transform nonDraggable = spread.transform.GetChild(1);
                for (int k = 0; k < nonDraggable.childCount; k++)
                {
                    GameObject entry = nonDraggable.transform.GetChild(k).gameObject;
                    entryObjs.Add(entry.name, entry);
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
        }
        Transform nonDraggable = spread.transform.GetChild(1);
        for (int k = 0; k < nonDraggable.childCount; k++)
        {
            GameObject entry = nonDraggable.transform.GetChild(k).gameObject;
            if (!showing.Contains(entry.name))
            {
                entry.SetActive(false);
            }
        }
    }
}
