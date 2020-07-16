using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDragger : MonoBehaviour
{
    public GameObject star;
    public GameObject starDragger;

    private RectTransform starRect;
    private RectTransform draggerRect;

    private float draggerX;
    private float draggerY;

    private float starX;
    private float starY;

    private void Start()
    {
        draggerRect = starDragger.GetComponent<RectTransform>();
        starRect = star.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        draggerX = draggerRect.anchoredPosition.x;
        draggerY = draggerRect.anchoredPosition.y;

        starX = starRect.anchoredPosition.x;
        starY = starRect.anchoredPosition.y;

        starRect.anchoredPosition = new Vector3(draggerX, draggerY, 0);
        draggerRect .anchoredPosition = new Vector3(starX, starY, 0);
    }
}
