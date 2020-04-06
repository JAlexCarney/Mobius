using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    private JournalHandler journalHandler;
    private CanvasSwapper canvasSwapper;
    private Dictionary<string, Queue<string>> sceneToPendingHints = new Dictionary<string, Queue<string>>();

    // Start is called before the first frame update
    void Start()
    {
        Util.DeactivateChildren(this.gameObject);
        journalHandler = GameObject.Find("JournalCanvas").GetComponent<JournalHandler>();
        canvasSwapper = GameObject.Find("CanvasSwapper").GetComponent<CanvasSwapper>();
    }

    public void DisplayCheck()
    {
        if (sceneToPendingHints.ContainsKey(canvasSwapper.currentCanvas.name))
        {
            Util.ActivateChildren(this.gameObject);
        }
        else
        {
            Util.DeactivateChildren(this.gameObject);
        }
    }

    public void IndicateHint(string hintText)
    {
        Debug.Log(hintText);
        if (!sceneToPendingHints.ContainsKey(canvasSwapper.currentCanvas.name))
        {
            sceneToPendingHints[canvasSwapper.currentCanvas.name] = new Queue<string>();
        }
        sceneToPendingHints[canvasSwapper.currentCanvas.name].Enqueue(hintText);
        DisplayCheck();
    }

    public void GetHint()
    {
        //journalHandler.AddEntry("2+" + sceneToPendingHints[canvasSwapper.currentCanvas.name].Dequeue());
        if (sceneToPendingHints[canvasSwapper.currentCanvas.name].Count == 0)
        {
            sceneToPendingHints.Remove(canvasSwapper.currentCanvas.name);
        }
        DisplayCheck();
    }
}
