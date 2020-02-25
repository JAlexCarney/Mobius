using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryHandler : MonoBehaviour
{
    private int numItems = 0;
    private int selectedSlot = -1;
    private GameObject openButton = null;
    private GameObject openedInventory = null;

    public int numSlots;
    private string[] items;
    private GameObject[] slots;
    private GameObject[] itemObjs;

    private string collecting;

    [System.Serializable]
    public struct Pickupable {
        public string label;
        public Sprite invSprite;
    }

    public Pickupable[] pickupableObjs;
    private Dictionary<string, Sprite> pickupDict = new Dictionary<string, Sprite>();

    public void Start()
    {
        foreach(Pickupable pickupable in pickupableObjs)
        {
            pickupDict[pickupable.label] = pickupable.invSprite;
        }


        slots = new GameObject[numSlots];
        itemObjs = new GameObject[numSlots];
        items = new string[numSlots];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = "";
            slots[i] = transform.Find("Opened").Find("Slot" + i).gameObject;
            itemObjs[i] = slots[i].transform.Find("Item").gameObject;
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

    public void Remove(string objToRemove)
    {
        for (int i = 0; i < items.Length; i++)
        {
            //Debug.Log(items[i] + " == " + objToRemove);
            if (items[i].ToLower() == objToRemove.ToLower())
            {
                items[i] = "";
                itemObjs[i].GetComponent<Draggable>().label = "";
                itemObjs[i].GetComponent<Image>().color = Color.clear;
                //Debug.Log("Object Removed");
            }
        }
    }

    public void SetCollecting(string obj)
    {
        collecting = obj;
    }

    public void Collect(string labelAndObj)
    {
        Open();
        
        // parse input
        List<string> input = Util.Split(labelAndObj, '+');
        string label = input[0];
        collecting = input[1];

        Debug.Log(label);
        GameObject tmp = GameObject.Find(collecting);
        Debug.Log(tmp);
        items[numItems] = label;
        numItems++;
        GameObject item = itemObjs[numItems - 1];
        item.GetComponent<Draggable>().label = label;
        item.transform.position = tmp.transform.position;
        item.GetComponent<Draggable>().dropPos = tmp.transform.position;
        item.GetComponent<Draggable>().isGoingBack = true;

        item.GetComponent<Image>().sprite = pickupDict[label];
        item.GetComponent<Image>().color = Color.white;
        Destroy(tmp);
    }

    // Deprecated functions
    /*
    public void SelectItem(int slot)
    {
        if (selectedSlot != -1)
        {
            DeselectItem(selectedSlot);
        }
        if (items[slot] != "")
        {
            GameObject slotObject = GameObject.Find("Slot" + slot);
            GameObject highlight = slots[slot].transform.Find("Highlight").gameObject;
            highlight.GetComponent<Image>().enabled = true;
            selectedSlot = slot;
        }
    }

    public void DeselectItem(int slot)
    {
        if (items[slot] != "")
        {
            GameObject highlight = slots[slot].transform.Find("Highlight").gameObject;

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
    }*/
}