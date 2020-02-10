using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopVisualFolllow : MonoBehaviour
{
    public GameObject visualPrefab;
    private static Dictionary<string, Transform[]> visuals = new Dictionary<string, Transform[]>();

    // Update is called once per frame
    void Update()
    {
        foreach (Transform[] visual in visuals.Values)
        {
            visual[0].position = visual[1].position;
        }
    }

    public void StartTracking(GameObject obj)
    {
        // create visual
        GameObject visual = Instantiate(visualPrefab);

        // make it a child of this canvas so that it will be rendered on top of everything
        visual.transform.parent = this.transform;

        // Get the source and target RectTransform components
        RectTransform objRectTransform = obj.GetComponent<RectTransform>();
        RectTransform visualRectTransform = visual.GetComponent<RectTransform>();

        // These four properties are to be copied
        visualRectTransform.anchorMin = objRectTransform.anchorMin;
        visualRectTransform.anchorMax = objRectTransform.anchorMax;
        visualRectTransform.anchoredPosition = objRectTransform.anchoredPosition;
        visualRectTransform.sizeDelta = objRectTransform.sizeDelta;

        visual.transform.localScale = obj.transform.localScale;
        visual.transform.eulerAngles = obj.transform.eulerAngles;

        visual.GetComponent<Image>().sprite = obj.GetComponent<Image>().sprite;

        // add visual to the dictionary to begin tracking
        visuals[obj.name] = new Transform[2];
        visuals[obj.name][0] = visual.transform;
        visuals[obj.name][1] = obj.transform;
    }

    public void StopTracking(GameObject obj)
    {
        Destroy(visuals[obj.name][0].gameObject);
        visuals.Remove(obj.name);
    }
}
