using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightNode
{
    public LightNode next;
    public LightNode prev;
    public string color;
    public GameObject beam;
    public Vector2 position;
    public int castFromPrism = 0;
}
