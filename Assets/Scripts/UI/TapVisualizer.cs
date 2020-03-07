using UnityEngine;
using UnityEngine.UI;

public class TapVisualizer : MonoBehaviour
{
    public GameObject tapPrefab;
    private Vector3 placeTapped;

    public void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool tapped = false;
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                placeTapped = new Vector3(touch.position.x, touch.position.y, -5f);
            }
            else if (Input.touchCount == 0)
            {
                placeTapped = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -5f); ;
            }
            foreach (Transform child in transform)
            {
                if (Util.CheckBounds(child.gameObject, placeTapped))
                {
                    tapped = true;
                }
            }
            if (!tapped)
            {
                Invoke("CreateTap", 0.1f);
            }
        }
    }

    public void CreateTap()
    {
        GameObject tapObj = Instantiate(tapPrefab, transform);
        tapObj.transform.position = placeTapped;
        Vector3 v = Random.insideUnitSphere.normalized;
        tapObj.transform.eulerAngles = new Vector3(0f,0f,Random.Range(0, 360));
        tapObj.GetComponent<Image>().color = new Color(v.x, v.y, v.z, 0.1f);
        Destroy(tapObj, 0.25f);
    }
}
