using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Mono : MonoBehaviour
{
    public DraggableWithColor[] set1;
    public DraggableWithColor[] set2;
    public DraggableWithColor[] set3;

    public UnityEvent winEvent;

    private bool started = false;

    // Start is called before the first frame update
    void FixedUpdate()
    {
        foreach (DraggableWithColor obj in set1)
        {
            obj.mono = GetComponent<Mono>();
        }
        foreach (DraggableWithColor obj in set2)
        {
            obj.mono = GetComponent<Mono>();
        }
        foreach (DraggableWithColor obj in set3)
        {
            obj.mono = GetComponent<Mono>();
        }

        if (!started)
        {
            bool set1Correct = true;
            bool set2Correct = true;
            bool set3Correct = true;
            while (set1Correct || set2Correct || set3Correct)
            {
                Swap(set1[Random.Range(0, set1.Length)], set1[Random.Range(0, set1.Length)]);
                Swap(set2[Random.Range(0, set2.Length)], set2[Random.Range(0, set2.Length)]);
                Swap(set3[Random.Range(0, set3.Length)], set3[Random.Range(0, set3.Length)]);

                set1Correct = true;
                foreach (DraggableWithColor obj in set1)
                {
                    if (obj.id != obj.winId)
                    {
                        set1Correct = false;
                    }
                }
                set2Correct = true;
                foreach (DraggableWithColor obj in set2)
                {
                    if (obj.id != obj.winId)
                    {
                        set2Correct = false;
                    }
                }
                set3Correct = true;
                foreach (DraggableWithColor obj in set3)
                {
                    if (obj.id != obj.winId)
                    {
                        set3Correct = false;
                    }
                }
            }
            started = true;
        }
    }

    void Swap(DraggableWithColor one, DraggableWithColor two)
    {
        Vector3 temp = one.transform.position;
        one.transform.position = two.transform.position;
        two.transform.position = temp;

        one.startPos = one.transform.position;
        two.startPos = two.transform.position;

        int tempID = one.id;
        one.id = two.id;
        two.id = tempID;
    }

    public void Check()
    {
        bool win = true;
        foreach (DraggableWithColor obj in set1)
        {
            if(obj.id != obj.winId)
            {
                win = false;
            }
        }
        foreach (DraggableWithColor obj in set2)
        {
            if (obj.id != obj.winId)
            {
                win = false;
            }
        }
        foreach (DraggableWithColor obj in set3)
        {
            if (obj.id != obj.winId)
            {
                win = false;
            }
        }
        if (win)
        {
            Debug.Log("WIN!!!");
            winEvent.Invoke();
        }
    }
}
