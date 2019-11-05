using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour
{
    private int numItems = 0;

    public void Colect(string name)
    {
        Debug.Log(name);
        GameObject thing = GameObject.Find(name);
        numItems++;
        GameObject slot = GameObject.Find("Slot" + numItems);
        GameObject item = slot.transform.Find("Item").gameObject;
        item.GetComponent<Image>().sprite = thing.GetComponent<Image>().sprite;
        item.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        Destroy(thing);
    }
}
