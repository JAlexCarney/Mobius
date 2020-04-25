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

    private GameObject dialogue;
    private DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log(this.GetComponentInChildren<Text>(true).gameObject);
        dialogue = this.GetComponentInChildren<Text>(true).gameObject;
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>(); 
    }

    public void OnPointerDown(PointerEventData d)
    {
        dialogueManager.displayText(dialogue);
    }

}
