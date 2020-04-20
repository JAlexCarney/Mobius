using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    public InventoryHandler inv;
    private bool placed = false;

    public void Place(string objToPlaceOn)
    {
        Debug.Log("An attempt was made");
        Debug.Log(objToPlaceOn);
        Debug.Log(this.name);
        GameObject obj = GameObject.Find(objToPlaceOn);
        Vector3 pos = obj.transform.position;
        if (!placed)
        {
            transform.position = pos;
            placed = true;
            Draggable drag = GetComponent<Draggable>();
            if (drag)
            {
                drag.startPos = pos;
                inv.Remove(name);
            }
        }
    }
}
