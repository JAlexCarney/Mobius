using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageFinder : MonoBehaviour
{
    public int page = -1;
    JournalHandler journalHandler;


    public void Start()
    {
        journalHandler = GameObject.Find("JournalCanvas").GetComponent<JournalHandler>();
    }

    public void JumpToPage() 
    {
        if (page == -1)
        {
            journalHandler.Open();
        }
        else 
        {
            journalHandler.OpenToPage(page);
        }
    }
}
