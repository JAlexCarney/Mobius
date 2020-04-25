using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{

    private GameObject currentlyDisplayedText;
    private bool pause; //to prevent the same click enabling and disabling text in 1 frame

    // Start is called before the first frame update
    void Start()
    {
        currentlyDisplayedText = null;
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause && Input.GetMouseButtonDown(0) && currentlyDisplayedText != null)
        {
            Debug.Log("omfffg");
            currentlyDisplayedText.SetActive(false);
            currentlyDisplayedText = null;
        }
        pause = false;

    }

    public void displayText(GameObject text)
    {
        if (currentlyDisplayedText == null)
        {
            Debug.Log("booo");
            pause = true; 
            currentlyDisplayedText = text;
            text.SetActive(true);
        }
    }
}
