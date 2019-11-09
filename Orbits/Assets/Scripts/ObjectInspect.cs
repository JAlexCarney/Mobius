using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInspect : MonoBehaviour
{
    public void InspectObject(string objectName)
    {
        GameObject object_ = GameObject.Find(objectName);
        GameObject objectButton = GameObject.Find(objectName + "Button");
        Util.ActivateChildren(object_);
        if (objectButton)
        {
            objectButton.GetComponent<Image>().enabled = false;
        }
    }

    public void HideObject(string objectName)
    {
        GameObject object_ = GameObject.Find(objectName);
        GameObject objectButton = GameObject.Find(objectName + "Button");
        Util.DeactivateChildren(object_);
        if (objectButton)
        {
            objectButton.GetComponent<Image>().enabled = true;
        }
    }

    // 2 args separated by plus in inspector
    // 1. object to replace
    // 2. object to show
    public void ObjectReplace(string names)
    {
        List<string> split = Util.Split(names, '+');

        string objectToReplaceName = split[0] + "Button";
        string objectToShowName = split[1] + "Button";

        Util.ObjectSwap(objectToReplaceName, objectToShowName);
    }
}
