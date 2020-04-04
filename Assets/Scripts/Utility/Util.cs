using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Util
{

    public static void DeactivateChildren(GameObject canvas)
    {
        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            canvas.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public static void ActivateChildren(GameObject canvas)
    {
        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            canvas.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public static List<string> Split(string string_, char seperator)
    {
        List<string> split = new List<string>();
        string temp = "";
        foreach (char c in string_)
        {
            if (c == seperator)
            {
                split.Add(temp);
                temp = "";
            }
            else
            {
                temp += c;
            }
        }
        split.Add(temp);
        return split;
    }

    public static int Mod(int x, int m)
    {
        return (x % m + m) % m;
    }

    public static void ObjectSwap(string objectToReplaceName, string objectToShowName)
    {
        GameObject objectToReplaceButton = GameObject.Find(objectToReplaceName);
        GameObject objectToShowButton = GameObject.Find(objectToShowName);
        try
        {
            objectToReplaceButton.GetComponent<Image>().enabled = false;
            objectToShowButton.GetComponent<Image>().enabled = true;
        }
        catch (Exception e)
        {
            Debug.Log("Object Not Found");
            Debug.Log("Exception: " + e);
        }
    }

    public static bool ArrayContainsString(string[] array, string key)
    {
        foreach (string test in array)
        {
            if (test == key) { return true; }
        }
        return false;
    }

    //ty https://stackoverflow.com/questions/3176602/how-to-force-a-number-to-be-in-a-range-in-c/3176617
    public static float Clamp(float value, float min, float max)
    {
        return (value < min) ? min : (value > max) ? max : value;
    }

    //from https://stackoverflow.com/questions/42043017/check-if-ui-elements-recttransform-are-overlapping
    public static bool RectOverlaps(RectTransform rectTrans1, RectTransform rectTrans2)
    {
        Rect rect1 = new Rect(rectTrans1.localPosition.x, rectTrans1.localPosition.y, rectTrans1.rect.width, rectTrans1.rect.height);
        Rect rect2 = new Rect(rectTrans2.localPosition.x, rectTrans2.localPosition.y, rectTrans2.rect.width, rectTrans2.rect.height);
        Debug.Log(rect1);
        Debug.Log(rect2);
        return rect1.Overlaps(rect2);
    }

}
