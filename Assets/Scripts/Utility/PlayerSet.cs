using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSet : MonoBehaviour
{
    public int player;

    // Start is called before the first frame update
    void Start()
    {
        Util.player = player;
    }
}
