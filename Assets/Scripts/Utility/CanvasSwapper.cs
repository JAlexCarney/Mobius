using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSwapper : MonoBehaviour
{
    public GameObject currentCanvas;
    public GameObject prevCanvas;
    public GameObject inventory;
    public GameObject map;
    public GameObject movement;
    public GameObject fade;

    private Image fadeImage;
    private void Start()
    {
        fadeImage = fade.GetComponent<Image>();
    }

    private readonly int delay = 15;
    private int count = 0;
    private bool fadingOut = false;
    private bool fadingIn = false;
    private string goingTo = "";
    void Update()
    {
        if (fadingOut)
        {
            count++;
            fadeImage.color = new Vector4(0.15f, 0.15f, 0.15f, (float)count / delay);
            if (count == delay)
            {
                fadingOut = false;
                fadingIn = true;
                SwitchCanvasMaintainUI(goingTo);
                count = 0;
            }
        }
        else if (fadingIn)
        {
            fadeImage.color = new Vector4(0.15f, 0.15f, 0.15f, 1f - ((float)count / delay));
            count++;
            if (count == delay)
            {
                fadingIn = false;
                goingTo = "";
                count = 0;
                fade.SetActive(false);
            }
        }
    }

    public void SwitchCanvasNoUI(string newCanvas)
    {
        GameObject canvasToActivate = GameObject.Find(newCanvas);
        Util.ActivateChildren(canvasToActivate);
        Util.DeactivateChildren(currentCanvas);
        Util.DeactivateChildren(inventory);
        prevCanvas = currentCanvas;
        currentCanvas = canvasToActivate;
        map.SetActive(false);
        movement.SetActive(false);
    }

    public void SwitchCanvasMaintainUI(string newCanvas)
    {
        GameObject canvasToActivate = GameObject.Find(newCanvas);
        Util.ActivateChildren(canvasToActivate);
        Util.DeactivateChildren(currentCanvas);
        Util.ActivateChildren(inventory);
        map.SetActive(true);
        movement.SetActive(true);
        prevCanvas = currentCanvas;
        currentCanvas = canvasToActivate;
    }

    public void SwitchCanvasMaintainUIAndFade(string newCanvas)
    {
        fadingOut = true;
        goingTo = newCanvas;
        fade.SetActive(true);
    }

    public void ReturnToPreviousCanvas()
    {
        string backUp = prevCanvas.transform.name;
        prevCanvas = currentCanvas;
        SwitchCanvasMaintainUI(backUp);
    }
}
