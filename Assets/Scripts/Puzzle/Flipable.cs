using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Flipable : MonoBehaviour
{
    public Sprite backSide;
    private int delay = 60;
    private int count = 0;
    private bool isFlipping = false;
    private Vector3 from = new Vector3(0,0,0);
    private Vector3 to = new Vector3(0, 360, 0);

    // Update is called once per frame
    void Update()
    {
        if (isFlipping)
        {
            if (count == delay / 2)
            {
                GetComponent<Image>().sprite = backSide;
            }
            else if (count == delay)
            {
                isFlipping = false;
            }
            else
            {
                count++;
                transform.eulerAngles = Vector3.Lerp(from, to, count/delay);
            }
        }
    }

    public void Flip()
    {
        isFlipping = true;
    }
}
