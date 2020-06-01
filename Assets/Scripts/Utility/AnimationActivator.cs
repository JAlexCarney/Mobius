using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationActivator : MonoBehaviour
{
    public void PlayAnimation()
    {
        GetComponent<Animation>().Play();
    }
}
