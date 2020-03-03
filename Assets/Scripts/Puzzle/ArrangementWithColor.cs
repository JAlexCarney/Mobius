using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArrangementWithColor : MonoBehaviour
{
    public UnityEvent winEvent;

    private List<GameObject> nodes;
    private MonoNode[] monoNodes;
    
    public void Start()
    {
        monoNodes = GetComponentsInChildren<MonoNode>();
        nodes = new List<GameObject>();
        foreach (MonoNode monoNode in monoNodes)
        {
            nodes.Add(monoNode.gameObject);
        }
    }

    public void Update()
    {
        // Check if the left mouse button was raised
        if (DraggableWithColor.justReleased)
        {
            if (DraggableWithColor.holding)
            {
                // Check if the mouse was clicked over a UI element
                GameObject node = GetNode(DraggableWithColor.heldObj);
                if (node)
                {
                    DraggableWithColor.justReleased = false;

                    DraggableWithColor heldDrag = DraggableWithColor.heldObj.GetComponent<DraggableWithColor>();
                    
                    heldDrag.startPos = heldDrag.gameObject.transform.position;

                    node.GetComponent<MonoNode>().currentObject = DraggableWithColor.heldObj;

                    CheckSolution();
                    
                    Debug.Log(DraggableWithColor.heldObj + " -> " + node);
                }
            }
        }
    }

    private void CheckSolution()
    {
        Debug.Log("Checking...");
        foreach (MonoNode monoNode in monoNodes)
        {
            if (monoNode.correctObject != monoNode.currentObject)
            {
                Debug.Log("Lose");
                return;
            }
        }
        Debug.Log("Win");
        Win();
    }

    private void Win()
    {
        winEvent.Invoke();
    }

    private GameObject GetNode(GameObject dropedObj)
    {
        GameObject snapNode = null;
        foreach (GameObject node in nodes)
        {
            if (CheckBounds(dropedObj, node.transform.position))
            {
                snapNode = node;
            }
        }
        return snapNode;
    }

    public bool CheckBounds(GameObject obj, Vector3 touchPos)
    {
        Vector3 pos = obj.transform.position;
        Vector3 delta = touchPos - pos;
        float width = obj.GetComponent<RectTransform>().rect.width;
        float height = obj.GetComponent<RectTransform>().rect.height;
        if (delta.x < width / 2 && delta.x > -width / 2 &&
            delta.y < height / 2 && delta.y > -height / 2)
        {
            return true;
        }
        return false;
    }
}
