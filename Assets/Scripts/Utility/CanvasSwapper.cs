using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSwapper : MonoBehaviour
{
    public GameObject currentCanvas;
    public GameObject prevCanvas;
    public GameObject inventory;
    public GameObject map;

    public void SwitchCanvasNoUI(string newCanvas)
    {
        GameObject canvasToActivate = GameObject.Find(newCanvas);
        Util.ActivateChildren(canvasToActivate);
        Util.DeactivateChildren(currentCanvas);
        Util.DeactivateChildren(inventory);
        prevCanvas = currentCanvas;
        currentCanvas = canvasToActivate;
        map.SetActive(false);
    }

    public void SwitchCanvasMaintainUI(string newCanvas)
    {
        GameObject canvasToActivate = GameObject.Find(newCanvas);
        Util.ActivateChildren(canvasToActivate);
        Util.DeactivateChildren(currentCanvas);
        Util.ActivateChildren(inventory);
        map.SetActive(true);
        prevCanvas = currentCanvas;
        currentCanvas = canvasToActivate;
    }

    public void ReturnToPreviousCanvas()
    {
      string backUp = prevCanvas.transform.name;
      SwitchCanvasMaintainUI(backUp);
    }
}
