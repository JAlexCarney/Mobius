using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSwapper : MonoBehaviour
{
    public GameObject currentCanvas;
    public GameObject inventory;

    public void SwitchCanvasNoInventory(string newCanvas)
    {
        GameObject canvasToActivate = GameObject.Find(newCanvas);
        Util.ActivateChildren(canvasToActivate);
        Util.DeactivateChildren(currentCanvas);
        currentCanvas = canvasToActivate;
        inventory.SetActive(false);
    }

    public void SwitchCanvasKeepInventory(string newCanvas)
    {
        GameObject canvasToActivate = GameObject.Find(newCanvas);
        Util.ActivateChildren(canvasToActivate);
        Util.DeactivateChildren(currentCanvas);
        inventory.SetActive(true);
        currentCanvas = canvasToActivate;
    }
}
