using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Arrangable : MonoBehaviour, IPointerDownHandler
{
    public Vector2Int correctPos;
    public Vector2Int currentPos;
    private static readonly float swapDelay = 90f;
    private static GameObject selected = null;
    private bool swapping = false;
    private Vector3 startPos;
    private Vector3 endPos;
    private int counter = 0;

    // Update is called once per frame
    void Update()
    {
        if (swapping)
        {
            if (counter == swapDelay)
            {
                counter = 0;

                swapping = false;
            }
            else
            {
                counter++;
                transform.position = Vector3.Lerp(startPos, endPos, (float)counter / swapDelay);
            }
        }
    }


    public void OnPointerDown(PointerEventData d)
    {
        if (!swapping)
        {
            if (selected != gameObject && selected != null)
            {
                Swap();
            }
            else if (selected == null)
            {
                Select();
            }
            else
            {
                Deselect();
            }
        }
    }

    private void Swap()
    {
        Arrangable other = selected.GetComponent<Arrangable>();

        this.swapping = true;
        other.swapping = true;
        this.startPos = this.transform.position;
        this.endPos = selected.transform.position;
        other.startPos = this.endPos;
        other.endPos = this.startPos;

        Vector2Int tmp = this.currentPos;
        this.currentPos = other.currentPos;
        other.currentPos = tmp;

        other.Deselect();
    }

    private void Select()
    {
        selected = this.gameObject;
        selected.GetComponent<Image>().color = new Color(0.5f, 0.5f, 1f);
    }

    private void Deselect()
    {
        selected.GetComponent<Image>().color = Color.white;
        selected = null;
    }
}
