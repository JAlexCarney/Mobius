using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IntroTransition : MonoBehaviour, IPointerDownHandler
{
    Animator animator;
    Animator transition;
    int frameCount;  


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        transition = GameObject.Find("Transition").GetComponent<Animator>();
        frameCount = 0;
    }

    private void FixedUpdate()
    {
        frameCount++; 
        if (frameCount == 644) 
        {
            transition.enabled = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
}
