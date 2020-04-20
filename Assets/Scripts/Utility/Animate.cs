using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animate : MonoBehaviour
{

    //hi if this anim plays in front of something clickable UNCLICK raycast target in image component

    public Sprite[] frames;
    private int currentFrame;
    private int lastIndex;

    public int framesPerSecond = 30;
    Image imageComponent;
    //public bool play; ---> play when the canvas is showing
    //bool loop = true;

    // Start is called before the first frame update
    void Start()
    {
        imageComponent = this.GetComponent<Image>();
        currentFrame = 0;
        lastIndex = 0;
        imageComponent.sprite = frames[0]; //set image
        imageComponent.color = new Color32(255, 255, 255, 255);
    }

    // Update is called once per frame
    //this does NOT work with 1 frame per second !!! aint dealin w that srry
    void Update()
    {
        float index = Time.time - (int)Time.time; //get partial seconds since last second
        index = (int)(index * framesPerSecond); //get current frame
        Debug.Log("current frame: " + currentFrame + " index: " + index + " time: " + (Time.time - (int)Time.time));
        if (lastIndex != index)
        {
            lastIndex = (int)index;
            currentFrame++;
            if (currentFrame >= frames.Length)
                currentFrame = 0;

            Debug.Log(currentFrame);
            imageComponent.sprite = frames[this.currentFrame];
            imageComponent.color = new Color32(255, 255, 225, 255); //for some reason everything gets set to 0 otherwise

        }
    }
}
