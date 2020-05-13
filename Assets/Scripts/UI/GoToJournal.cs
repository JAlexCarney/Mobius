using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToJournal : MonoBehaviour
{
    private bool isGoingToJournal = false;
    private Vector3 startPos;
    private Vector3 journalButtonPosition;
    private int counter = 0;

    private void Start()
    {
        journalButtonPosition = GameObject.Find("OpenJournal").transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGoingToJournal)
        {
            if (counter == 60)
            {
                counter = 0;
                isGoingToJournal = false;
                GetComponent<Image>().color = Color.clear;
                
            }
            else
            {
                counter++;
                transform.position = Vector3.Lerp(startPos, journalButtonPosition, 1 + Mathf.Pow((float)counter / 30f - 1, 3));
            }
        }
    }

    public void Go()
    {
        startPos = transform.position;
        isGoingToJournal = true;
    }
}
