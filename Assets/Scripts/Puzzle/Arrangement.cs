using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrangement : MonoBehaviour
{
    public int width;
    public int height;
    private Arrangable[][] arrangables;

    // Start is called before the first frame update
    void Start()
    {
        arrangables = new Arrangable[width][];
        for (int i = 0; i < width; i++)
        {
            arrangables[i] = new Arrangable[height];
        }

        foreach (Arrangable child in gameObject.GetComponentsInChildren<Arrangable>())
        {
            arrangables[child.currentPos.x][child.currentPos.y] = child;
        }

        Scramble(100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Check()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (arrangables[i][j].currentPos.x != arrangables[i][j].correctPos.x ||
                    arrangables[i][j].currentPos.y != arrangables[i][j].correctPos.y)
                {
                    return false;
                }
            }
        }
        return true;
    }

    void Scramble(int swaps)
    {
        for (int i = 0; i < swaps; i++)
        {
            Arrangable one = arrangables[Random.Range(0, width)][Random.Range(0, height)];
            Arrangable two = arrangables[Random.Range(0, width)][Random.Range(0, height)];


            Vector2Int tmppos = one.correctPos;
            one.correctPos = two.correctPos;
            two.correctPos = tmppos;

            // swap visuals
            Sprite tmpsprite = one.gameObject.GetComponent<Image>().sprite;
            one.gameObject.GetComponent<Image>().sprite = two.gameObject.GetComponent<Image>().sprite;
            two.gameObject.GetComponent<Image>().sprite = tmpsprite;
        }
    }
}
