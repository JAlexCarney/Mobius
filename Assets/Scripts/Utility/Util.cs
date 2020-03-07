using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Util
{
    public static bool CheckBounds(GameObject obj, Vector3 touchPos)
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

}
