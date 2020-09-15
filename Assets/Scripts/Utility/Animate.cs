using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animate : MonoBehaviour
{

    //hi if this anim plays in front of something clickable UNCLICK raycast target in image component
    public Sprite[] frames;
    public int framesPerSecond = 30;
    public bool play = true;
    public bool loop = true;

    private int currentFrame;
    private int lastIndex;
    Image imageComponent;

    // Start is called before the first frame update
    void Start()
    {
        imageComponent = this.GetComponent<Image>();
        currentFrame = -1;
        lastIndex = 0;
        imageComponent.sprite = frames[0]; //set image
        imageComponent.color = new Color32(255, 255, 255, 255);
    }

    // Update is called once per frame
    //this does NOT work with 1 frame per second !!! aint dealin w that srry
    void Update()
    {
        
        if (play)
        {
               
            float index = Time.time - (int)Time.time; //get partial seconds since last second
            index = (int)(index * framesPerSecond); //get current frame

            //if there is any change in frame index update it!!
            if (lastIndex != index)
            {
                lastIndex = (int)index;
                currentFrame++;

                if (currentFrame >= frames.Length)
                {
                    if (loop) //reset anim
                    {
                        currentFrame = 0;
                    }
                    else //stop anim & display last frame
                    {
                        currentFrame = frames.Length - 1;
                        play = false;
                    }
                }

                imageComponent.sprite = frames[this.currentFrame];
                imageComponent.color = new Color32(255, 255, 225, 255); //for some reason everything gets set to 0 otherwise
            }
        }
        else if (!loop) //start at beginning

        {
            currentFrame = -1;
            lastIndex = 0;
        }
    }

    public void PlayAnimation() 
    {
        imageComponent = this.GetComponent<Image>();
        currentFrame = -1;
        lastIndex = 0;
        imageComponent.sprite = frames[0]; //set image
        imageComponent.color = new Color32(255, 255, 255, 255);
        play = true;
        loop = false;
    }

}
