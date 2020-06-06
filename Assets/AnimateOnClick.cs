using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnimateOnClick : MonoBehaviour, IPointerDownHandler
{
    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        anim.SetTrigger("Active");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
