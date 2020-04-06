using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NegativeOnClick : MonoBehaviour, IPointerDownHandler
{
    private Animator anim;
    private SoundManager soundManager; 
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            //do  things
        }
    }
    
    public void OnPointerDown(PointerEventData d)
    {
        
        soundManager.Play("penScratch");
        anim.SetBool("Clicked", true);
        Invoke("setClickedFalse", 1f);
    }

    public void setClickedFalse()
    {
        anim.SetBool("Clicked", false);
    }
}
