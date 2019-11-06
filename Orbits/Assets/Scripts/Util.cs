using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
