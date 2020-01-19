using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryHandler : MonoBehaviour
{
    private int numItems = 0;
    private int selectedSlot = -1;
    private string[] items;
    private GameObject openButton = null;
    private GameObject openedInventory = null;

    public void Start()
    {
        items = new string[5];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = "";
        }
        openButton = transform.Find("Open").gameObject;
        openedInventory = transform.Find("Opened").gameObject;
    }

    public void Open()
    {
        Util.ActivateChildren(openedInventory);
        openButton.SetActive(false);
    }

    public void Close()
    {
        Util.DeactivateChildren(openedInventory);
        openButton.SetActive(true);
    }

    public void Collect(string name)
    {
        Debug.Log(name);
        GameObject thing = GameObject.Find(name);
        items[numItems] = name;
        GameObject slot = GameObject.Find("Slot" + numItems);
        numItems++;
        GameObject item = slot.transform.Find("Item").gameObject;
        item.GetComponent<Image>().sprite = thing.GetComponent<Image>().sprite;
        item.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        Destroy(thing);
    }

    public void SelectItem(int slot)
    {
        if (selectedSlot != -1)
        {
            DeselectItem(selectedSlot);
        }
        if (items[slot] != "")
        {
            GameObject slotObject = GameObject.Find("Slot" + slot);
            GameObject highlight = slotObject.transform.Find("Highlight").gameObject;
            highlight.GetComponent<Image>().enabled = true;
            selectedSlot = slot;
        }
    }

    public void DeselectItem(int slot)
    {
        if (items[slot] != "")
        {
            GameObject slotObject = GameObject.Find("Slot" + slot);
            GameObject highlight = slotObject.transform.Find("Highlight").gameObject;

            highlight.GetComponent<Image>().enabled = false;
            selectedSlot = -1;
        }
    }

    // multiple args seperated by +
    // 1. required object
    // 2. object to replace
    // 3. object to show
    public void ObjectConditional(string names)
    {
        List<string> split = Util.Split(names, '+');

        string objectRequiredName = split[0];
        string objectToReplaceName = split[1];
        string objectToShowName = split[2];

        if (selectedSlot != -1)
        {
            if (String.Compare(items[selectedSlot], objectRequiredName) == 0)
            {
                Util.ObjectSwap(objectToReplaceName, objectToShowName);
            }
        }
    }
}
