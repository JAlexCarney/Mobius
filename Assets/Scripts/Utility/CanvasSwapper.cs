using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSwapper : MonoBehaviour
{
    public GameObject currentCanvas;
    struct BackCanvas {
        public GameObject obj;
        public bool lookingEnabled;
    }

    private Stack<BackCanvas> backStack;
    public GameObject inventory;
    public GameObject movement;
    public GameObject fade;

    private MovementManager movementManager;

    private Image fadeImage;
    private void Start()
    {
        fadeImage = fade.GetComponent<Image>();
        movementManager = movement.GetComponent<MovementManager>();
        backStack = new Stack<BackCanvas>();
    }

    private readonly int delay = 25;
    private int count = 0;
    private bool fadingOut = false;
    private bool fadingIn = false;
    private string goingTo = "";
    private bool lookingEnabledAfterFade = false;
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
                if (lookingEnabledAfterFade)
                {
                    SwitchCanvasMaintainUIWithLooking(goingTo);
                }
                else
                {
                    SwitchCanvasMaintainUIWithoutLooking(goingTo);
                }
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

    public void ClearBackStack()
    {
        backStack.Clear();
        movementManager.DisableBack();
    }

    // depricated!!
    public void SwitchCanvasNoUI(string newCanvas)
    {
        GameObject canvasToActivate = GameObject.Find(newCanvas);
        Util.ActivateChildren(canvasToActivate);
        Util.DeactivateChildren(currentCanvas);
        Util.DeactivateChildren(inventory);
        Util.DeactivateChildren(movement);
        currentCanvas = canvasToActivate;
    }

    public void SwitchCanvasNoUIWithLooking(string newCanvas)
    {
        GameObject canvasToActivate = GameObject.Find(newCanvas);
        Util.ActivateChildren(canvasToActivate);
        Util.DeactivateChildren(currentCanvas);
        Util.DeactivateChildren(movement);
        Util.DeactivateChildren(inventory);

        // push to backStack
        movementManager.EnableBack();
        BackCanvas bc = new BackCanvas
        {
            obj = currentCanvas,
            lookingEnabled = movementManager.lookingEnabled
        };
        backStack.Push(bc);
        currentCanvas = canvasToActivate;

        // set looking
        movementManager.CenterAndEnableLooking();
    }

    public void SwitchCanvasNoUIWithoutLooking(string newCanvas)
    {
        GameObject canvasToActivate = GameObject.Find(newCanvas);
        Util.ActivateChildren(canvasToActivate);
        Util.DeactivateChildren(currentCanvas);
        Util.DeactivateChildren(inventory);
        Util.DeactivateChildren(movement);

        // push to backStack
        movementManager.EnableBack();
        BackCanvas bc = new BackCanvas
        {
            obj = currentCanvas,
            lookingEnabled = movementManager.lookingEnabled
        };
        backStack.Push(bc);
        currentCanvas = canvasToActivate;

        // set looking
        movementManager.DisableLooking();
    }

    // depricated!!
    public void SwitchCanvasMaintainUI(string newCanvas)
    {
        GameObject canvasToActivate = GameObject.Find(newCanvas);
        Util.ActivateChildren(canvasToActivate);
        Util.DeactivateChildren(currentCanvas);
        Util.ActivateChildren(inventory);
        Util.ActivateChildren(movement);
        currentCanvas = canvasToActivate;
    }

    public void SwitchCanvasMaintainUIWithLooking(string newCanvas)
    {
        GameObject canvasToActivate = GameObject.Find(newCanvas);
        Util.ActivateChildren(canvasToActivate);
        Util.DeactivateChildren(currentCanvas);
        Util.ActivateChildren(inventory);
        Util.ActivateChildren(movement);

        // push to backStack
        movementManager.EnableBack();
        BackCanvas bc = new BackCanvas
        {
            obj = currentCanvas,
            lookingEnabled = movementManager.lookingEnabled
        };
        backStack.Push(bc);
        currentCanvas = canvasToActivate;

        // set looking
        movementManager.CenterAndEnableLooking();
    }

    public void SwitchCanvasMaintainUIWithoutLooking(string newCanvas)
    {
        GameObject canvasToActivate = GameObject.Find(newCanvas);
        Util.ActivateChildren(canvasToActivate);
        Util.DeactivateChildren(currentCanvas);
        Util.ActivateChildren(inventory);
        Util.ActivateChildren(movement);

        // push to backStack
        movementManager.EnableBack();
        BackCanvas bc = new BackCanvas
        {
            obj = currentCanvas,
            lookingEnabled = movementManager.lookingEnabled
        };
        backStack.Push(bc);
        currentCanvas = canvasToActivate;

        // set looking
        movementManager.DisableLooking();
    }

    public void SwitchCanvasMaintainUIAndFade(string newCanvas)
    {
        fadingOut = true;
        goingTo = newCanvas;
        fade.SetActive(true);
    }

    public void SwitchCanvasMaintainUIAndFadeWithLooking(string newCanvas)
    {
        fadingOut = true;
        goingTo = newCanvas;
        fade.SetActive(true);
        lookingEnabledAfterFade = true;
    }

    public void SwitchCanvasMaintainUIAndFadeWithoutLooking(string newCanvas)
    {
        fadingOut = true;
        goingTo = newCanvas;
        fade.SetActive(true);
        lookingEnabledAfterFade = false;
    }

    public void ReturnToPreviousCanvas()
    {
        if (!movementManager.backIsDisabled)
        {
            BackCanvas backCanvas = backStack.Pop();
            GameObject canvasToActivate = backCanvas.obj;
            if (backCanvas.lookingEnabled)
            {
                movementManager.CenterAndEnableLooking();
            }
            else
            {
                movementManager.DisableLooking();
            }

            Util.DeactivateChildren(currentCanvas);
            Util.ActivateChildren(canvasToActivate);
            Util.ActivateChildren(inventory);
            Util.ActivateChildren(movement);

            currentCanvas = canvasToActivate;

            if (backStack.Count == 0)
            {
                movementManager.DisableBack();
            }
        }
    }
}
