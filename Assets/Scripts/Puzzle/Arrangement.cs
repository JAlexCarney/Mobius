using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Check()
    {

    }

    void Scramble()
    {

    }
}
