using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInspect : MonoBehaviour
{
    public void InspectObject(string objectName)
    {
        GameObject object_ = GameObject.Find(objectName);
        GameObject objectButton = GameObject.Find(objectName + "Button");
        Util.ActivateChildren(object_);
        objectButton.GetComponent<Image>().enabled = false;
    }

    public void HideObject(string objectName)
    {
        GameObject object_ = GameObject.Find(objectName);
        GameObject objectButton = GameObject.Find(objectName + "Button");
        Util.DeactivateChildren(object_);
        objectButton.GetComponent<Image>().enabled = true;
    }
}
