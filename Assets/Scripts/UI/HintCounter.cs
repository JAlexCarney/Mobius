using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintCounter : MonoBehaviour
{
    private int count = 0;
    private HintManager hintManager;

    [System.Serializable]
    public struct Hint
    {
        public string hintText;
        // in seconds
        public float delay;
    }
    public Hint[] hints;

    public void Start()
    {
        hintManager = GameObject.Find("HintManager").GetComponent<HintManager>();
    }

    public void FixedUpdate()
    {
        count++;
        foreach (Hint hint in hints)
        {
            //Debug.Log(count + " " + (int)hint.delay*50);
            if (count == (int)hint.delay*50)
            {
                hintManager.IndicateHint(hint.hintText);
            }
        }
    }
}
