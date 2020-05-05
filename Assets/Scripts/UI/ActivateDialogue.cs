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
public class ActivateDialogue : MonoBehaviour, IPointerDownHandler
{

    public Text dialogue;


    public Activate activate;


    private bool inCycle = false; //track if text is displaying
    public static float delay = 0.025f;
    public static float fadeOutTime = 3f; 
    public static float timeToRead = 2f;

    private string[] wordsInDialogue;
    private string originalDialogue;
    private Color originalColor;
    private Color clearColor; //same as original but with 0 for alpha

    // Start is called before the first frame update
    void Start()
    {
        originalDialogue = dialogue.text;
        originalColor = dialogue.color;
        clearColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        dialogue.gameObject.SetActive(false); //make sure it's invisible
    }

    //Check if obj is awake for activate on awake!
    public void Update()
    {
        dialogue.gameObject.SetActive(inCycle); //make sure it's invisible
        //start showing text
        if (!inCycle && activate == Activate.onEnter)
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

    //activate on click
    public void OnPointerDown(PointerEventData d)
    {
        //start showing text
        if (!inCycle && activate == Activate.onClick)
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
        for (int i = 0; i < originalDialogue.Length; i++)
        {
            dialogue.text = originalDialogue.Substring(0, i);
            yield return new WaitForSeconds(delay);
        }
        //for (int i = 0; i < wordsInDialogue.Length; i++) //word by word
        //{
        //    dialogue.text += wordsInDialogue[i] + " ";
        //    yield return new WaitForSeconds(delay);
        //}
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
            dialogue.color = Color.Lerp(originalColor, clearColor, Mathf.Min(1, t / fadeOutTime));
            yield return null;
        }
        //reset all
        Reset();
    }

    //if you leave the scene, reset everything
    private void OnDisable()
    {
        if (!this.gameObject.activeInHierarchy) //this double checks that the GAMEOBJECT is inactive, not just the component
        {
            Reset();
        }

    }

    private void Reset()
    {
        Destroy(dialogue); //eeehhhhh
        dialogue.color = originalColor;
        dialogue.text = originalDialogue;
        inCycle = false;
        this.enabled = false;

    }

    public enum Activate
    {
        onClick,
        onEnter
    }

}
