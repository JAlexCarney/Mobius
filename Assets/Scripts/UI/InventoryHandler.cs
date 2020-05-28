using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryHandler : MonoBehaviour
{
    private GameObject openButton = null;
    private GameObject openedInventory = null;
    private SoundManager soundManager;

    public int numSlots;
    private string[] items;
    private GameObject[] slots;
    private GameObject[] itemObjs;
    private bool open = false;
    private bool animating = false;
    private bool isCollecting = false;

    private string labelToCollect;
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
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        Debug.Log("Inventory Start");

        foreach (Pickupable pickupable in pickupableObjs)
        {
            pickupDict[pickupable.label] = pickupable.invSprite;
        }


        slots = new GameObject[numSlots];
        itemObjs = new GameObject[numSlots];
        items = new string[numSlots];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = "";
            slots[i] = transform.Find("Opened").Find("Background").Find("Slot" + i).gameObject;
            itemObjs[i] = slots[i].transform.Find("Item").gameObject;
        }
        openButton = transform.Find("Open").gameObject;
        openedInventory = transform.Find("Opened").gameObject;
    }

    public void Open()
    {
        if (!open && !animating)
        {
            soundManager.Play("bagOpen");
            Util.ActivateChildren(openedInventory);
            Animation anim = openedInventory.GetComponent<Animation>();
            anim["InventoryOpen"].speed = 1;
            anim["InventoryOpen"].time = 0;
            anim.Play("InventoryOpen");
            animating = true;
            Invoke("OpenDelayed", 0.5f);

        }
    }

    public void Toggle()
    {
        if (!open && !animating)
        {
            Open();
        }
        else if(!animating)
        {
            Close();
        }
    }

    public void Close()
    {
        if (open && !animating)
        {
            soundManager.Play("bagOpen");
            Animation anim = openedInventory.GetComponent<Animation>();
            anim["InventoryOpen"].speed = -1;
            anim["InventoryOpen"].time = anim["InventoryOpen"].length;
            anim.Play("InventoryOpen");
            animating = true;
            Invoke("CloseDelayed", 0.5f);
        }
    }

    public void CloseDelayed()
    {
        Util.DeactivateChildren(openedInventory);
        open = false;
        animating = false;
    }

    private bool firstOpening = true;
    private Vector3[] positions = new Vector3[10]; 
    public void OpenDelayed()
    {
        animating = false;
        open = true;
        if (firstOpening)
        {
            for (int i = 0; i < itemObjs.Length; i++)
            {
                positions[i] = itemObjs[i].transform.position;
            }
            firstOpening = false;
        }
        for (int i = 0; i < itemObjs.Length; i++)
        {
            itemObjs[i].GetComponent<Draggable>().startPos = positions[i];
        }

        if (isCollecting && GameObject.Find(collecting))
        {
            // Get reference to object being collected
            GameObject tmp = GameObject.Find(collecting);

            // Add reference to Open Slot
            int index = 0;
            while (items[index] != "") { index++; }
            items[index] = labelToCollect;
            GameObject item = itemObjs[index];
            item.GetComponent<Draggable>().label = labelToCollect;

            // play animation and destroy Pickup Object
            item.transform.position = tmp.transform.position;
            item.GetComponent<Draggable>().dropPos = tmp.transform.position;
            item.GetComponent<Draggable>().isGoingBack = true;
            item.GetComponent<Image>().sprite = pickupDict[labelToCollect];
            item.GetComponent<Image>().color = Color.white;
            Destroy(tmp);
        }
    }

    public void Remove(string objToRemove)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].ToLower() == objToRemove.ToLower())
            {
                items[i] = "";
                itemObjs[i].GetComponent<Draggable>().label = "";
                itemObjs[i].GetComponent<Image>().color = Color.clear;
            }
        }
        // reorder inventory slots
        Invoke("UpdateInventory", 0.5f);
    }

    private void UpdateInventory()
    {
        List<string> currentItems = new List<string>();
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != "")
            {
                currentItems.Add(items[i]);
            }
        }
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != "")
            {
                items[i] = "";
                itemObjs[i].GetComponent<Draggable>().label = "";
                itemObjs[i].GetComponent<Image>().color = Color.clear;
            }
        }
        int index = 0;
        foreach (string currentItem in currentItems)
        {
            items[index] = currentItem;
            itemObjs[index].GetComponent<Draggable>().label = currentItem;
            itemObjs[index].GetComponent<Image>().color = Color.white;
            itemObjs[index].GetComponent<Image>().sprite = pickupDict[currentItem];
            index++;
        }
        if (Empty())
        {
            Close();
        }
    }

    public bool Empty()
    {
        foreach(string item in items)
        {
            if (item != "") { return false; }
        }
        return true;
    }

    public void Collect(string labelAndObj)
    {
        // parse input
        List<string> input = Util.Split(labelAndObj, '+');
        labelToCollect = input[0];
        collecting = input[1];

        // make sure inventory is opened
        if (!open)
        {
            Open();
            isCollecting = true;
            return;
        }
        
        // Get reference to object being collected
        GameObject tmp = GameObject.Find(collecting);

        // Add reference to Open Slot
        int index = 0;
        while (items[index] != "") { index++; }
        items[index] = labelToCollect;
        GameObject item = itemObjs[index];
        item.GetComponent<Draggable>().label = labelToCollect;

        // play animation and destroy Pickup Object
        item.transform.position = tmp.transform.position;
        item.GetComponent<Draggable>().dropPos = tmp.transform.position;
        item.GetComponent<Draggable>().isGoingBack = true;
        item.GetComponent<Image>().sprite = pickupDict[labelToCollect];
        item.GetComponent<Image>().color = Color.white;
        Destroy(tmp);
    }
}