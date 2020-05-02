using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// <summary>
/// TO USE: 
/// attach this to a game object
/// have its dialogue be a child of the object
/// thats it lmao
/// </summary>
public class Dialogue : MonoBehaviour, IPointerDownHandler
{

    private Text dialogue; 

    private bool inCycle; //track if text is displaying
    public static float delay = 0.3f;
    public static float fadeOutTime = 3f; 
    public static float timeToRead = 2f;

    private string[] wordsInDialogue;
    private string originalDialogue;
    private Color originalColor; 

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(this.GetComponentInChildren<Text>(true).gameObject);
        dialogue = this.GetComponentInChildren<Text>(true);
        originalDialogue = dialogue.text;
        originalColor = dialogue.color;
    }


    public void OnPointerDown(PointerEventData d)
    {
        if (!inCycle)
        {
            inCycle = true;
            dialogue.gameObject.SetActive(true);

            //get each word in the dialogue, to show one at a time
            wordsInDialogue = dialogue.text.Split(' ');
            dialogue.text = "";


            //show text
            StartCoroutine(ShowDialogue());
        }
    }

    //show words one at a time
    IEnumerator ShowDialogue()
    {
        for (int i = 0; i < wordsInDialogue.Length; i++)
        {
            dialogue.text += wordsInDialogue[i] + " ";
            yield return new WaitForSeconds(delay);
        }
        Invoke("startFade", timeToRead);
    }

    //this is just a wrapper function so coroutine can be invoked l o l idk if theres a cleaner way of doing it
    private void startFade()
    {
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        //slowly fade out lmao thats it
        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
        {
            dialogue.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeOutTime));
            yield return null;
        }
        //reset all
        dialogue.gameObject.SetActive(false);
        dialogue.color = originalColor;
        inCycle = false;
    }

    //if you leave the scene, reset everything
    private void OnDisable()
    {
        if (!this.gameObject.activeInHierarchy) //this double checks that the GAMEOBJECT is inactive, not just the component
        {
            Debug.Log("boop");
            dialogue.gameObject.SetActive(false);
            dialogue.color = originalColor;
            dialogue.text = originalDialogue;
            inCycle = false;
        }

    }
}
