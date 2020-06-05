using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Whale : MonoBehaviour, IPointerDownHandler
{
    private Animator anim;
    private SoundManager soundManager;
    private RectTransform bone; 
    private int count = 0; 

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        //find the bone
        bone = GameObject.Find("BonePickUp").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    public void OnPointerDown(PointerEventData d)
    {
        //shake that bone off
        if (count < 3)
        {
            soundManager.Play("boneBreak");
            anim.SetBool("Clicked", true);
            Invoke("setClickedFalse", 1f);
            bone.rotation = Quaternion.Euler(0, 0, 15*count);

        }

        //give bone to player
        else if (count == 3)
        {
            GetComponent<Button>().enabled = true;
        }

        //whale no longer does shit
        else
        {
            this.GetComponent<Button>().enabled = false;
            this.enabled = false;
        }
    }

    public void setClickedFalse()
    {
        count++;
        anim.SetBool("Clicked", false);
    }
}
