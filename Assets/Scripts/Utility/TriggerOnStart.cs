using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TriggerOnStart : MonoBehaviour
{
    public UnityEvent eventToTrigger;
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        eventToTrigger.Invoke();
    }


}
