using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrismReference : MonoBehaviour
{
    // References to all interactable objects on board.
    public GameObject[][] prismReference = new GameObject[7][];
    public GameObject[] row0;
    public GameObject[] row1;
    public GameObject[] row2;
    public GameObject[] row3;
    public GameObject[] row4;
    public GameObject[] row5;
    public GameObject[] row6;

    public GameObject blue;
    public GameObject cyan;
    public GameObject green;
    public GameObject magenta;
    public GameObject red;
    public GameObject white;
    public GameObject yellow;

    [System.Serializable]
    public struct ElementImage
    {
        public string name;
        public Sprite image;
    }

    public Dictionary<string, GameObject> lightPrefabs;
    private Stack<GameObject> lightBeams = new Stack<GameObject>();

    private Dictionary<string, Sprite> mirrorImages;
    private Dictionary<string, Sprite> prismImages;

    public ElementImage[] mirrorImagesArray;
    public ElementImage[] prismImagesArray;
   

    private void Start()
    {
        lightPrefabs = new Dictionary<string, GameObject>();
        mirrorImages = new Dictionary<string, Sprite>();
        prismImages = new Dictionary<string, Sprite>();

        lightPrefabs.Add("blue", blue);
        lightPrefabs.Add("cyan", cyan);
        lightPrefabs.Add("green", green);
        lightPrefabs.Add("magenta", magenta);
        lightPrefabs.Add("red", red);
        lightPrefabs.Add("white", white);
        lightPrefabs.Add("yellow", yellow);

        prismReference[0] = row0;
        prismReference[1] = row1;
        prismReference[2] = row2;
        prismReference[3] = row3;
        prismReference[4] = row4;
        prismReference[5] = row5;
        prismReference[6] = row6;

        for (int i = 0; i < mirrorImagesArray.Length; i++)
        {
            mirrorImages[mirrorImagesArray[i].name] = mirrorImagesArray[i].image;
        }

        for (int i = 0; i < prismImagesArray.Length; i++)
        {
            prismImages[prismImagesArray[i].name] = prismImagesArray[i].image;
        }

        for (int i = 0; i < prismReference.Length; i++)
        {
            for (int j = 0; j < prismReference.Length; j++)
            {

                if (prismReference[i][j] != null)
                {
                    GameObject current = prismReference[i][j];
                    current.GetComponent<PrismElement>().row = i;
                    current.GetComponent<PrismElement>().column = j;
                }
                else
                {
                    //Debug.Log("Corner");
                }
            }
        }

        BoardUpdate();
    }

    public void BoardUpdate()
    {
        List<PrismElement> sources = new List<PrismElement>();

        ClearBeams();
        ClearColors();

        for (int i = 0; i < prismReference.Length; i++)
        {
            for (int j= 0; j < prismReference.Length; j++)
            {
                PrismElement current = prismReference[i][j].GetComponent<PrismElement>();

                if (prismReference[i][j].GetComponent<PrismElement>().type == "source")
                {
                    sources.Add(current);
                }

            }
        }

        foreach (PrismElement source in sources)
        {
            LightNode lightPath = new LightNode();
            lightPath.position = source.gameObject.GetComponent<RectTransform>().anchoredPosition;
            lightPath.color = source.colorToCast;
            lightPath = CalculateLight(source.row, source.column, source.orientation, lightPath);
            CastLight(lightPath);
        }
    }

    public LightNode CalculateLight(int sourceRow, int sourceCol, string sourceDir, LightNode path)
    {
        LightNode head = path;
        LightNode current = head;
        bool blocked = false;
        int col = sourceCol;
        int row = sourceRow;
        string direction = sourceDir;
        Debug.Log(sourceRow + " " + sourceCol);

        while (!blocked)
        {
            // Search through the reference array to find an object.
            if (direction == "left")
            {
                col--;
            }

            else if (direction == "right")
            {
                col++;
            }

            else if (direction == "up")
            {
                row--;
            }

            else if (direction == "down")
            {
                row++;
            }

            PrismElement currentElement = prismReference[row][col].GetComponent<PrismElement>();
            /*Debug.Log(currentElement.name);
            Debug.Log(direction);
            Debug.Log("Type: " + currentElement.type);
            Debug.Log("Column: " + col);
            Debug.Log("Row: " + row); */

            // How light interacts will mirrors.
            if (currentElement.type == "mirror")
            {
                string mirrorOrientation = currentElement.orientation;

                if (mirrorOrientation == "NE")
                {
                    if (direction == "left")
                    {
                        direction = "up";
                    }

                    else if (direction == "down")
                    {
                        direction = "right";
                    }

                    else if (direction == "up" || direction == "right")
                    {
                        blocked = true;
                    }

                }

                else if (mirrorOrientation == "SE")
                {
                    if (direction == "left")
                    {
                        direction = "down";
                    }

                    else if (direction == "up")
                    {
                        direction = "right";
                    }

                    else if (direction == "down" || direction == "right")
                    {
                        blocked = true;
                    }
                }

                else if (mirrorOrientation == "SW")
                {
                    if (direction == "right")
                    {
                        direction = "down";
                    }

                    else if (direction == "up")
                    {
                        direction = "left";
                    }

                    else if (direction == "down" || direction == "left")
                    {
                        blocked = true;
                    }
                }

                else if (mirrorOrientation == "NW")
                {
                    if (direction == "right")
                    {
                        direction = "up";
                    }

                    else if (direction == "down")
                    {
                        direction = "left";
                    }

                    else if (direction == "up" || direction == "left")
                    {
                        blocked = true;
                    }
                }

                // Declare next light node in list.
                if (!blocked)
                {
                    LightNode next = new LightNode();
                    next.position = currentElement.gameObject.GetComponent<RectTransform>().anchoredPosition;
                    next.color = current.color;
                    current.next = next;
                    current = next;

                    // Change the color of the mirror.
                    currentElement.GetComponentInChildren<Image>().sprite = mirrorImages[next.color];
                }

            }

            else if (currentElement.type == "prism")
            {

            }

            else if (currentElement.type == "wall" || currentElement.type == "source" || currentElement.type == "socket")
            {
                blocked = true;
                
            }

        }

        // Declare light node where the beam stops casting.
        LightNode end = new LightNode();
        end.position = prismReference[row][col].gameObject.GetComponent<RectTransform>().anchoredPosition;
        end.color = current.color;
        current.next = end;

        return head;
    }

    // Iterate through given path of light nodes to cast light path.
    public void CastLight(LightNode path)
    {
        LightNode current = path;

        while(current.next != null)
        {
            GameObject beam = DrawLight(current.position, current.next.position, lightPrefabs[current.color]);
            //Debug.Log(current.color);
            //Debug.Log(current.position);
            //Debug.Log(current.next.color);
            //Debug.Log(current.next.position);
            lightBeams.Push(beam);
            current = current.next;
        }
    }

    public GameObject DrawLight(Vector2 pos1, Vector2 pos2, GameObject prefab)
    {
        // Spawn an image object as a little square with 2 screen positions
        GameObject line = Instantiate(prefab);
        //Debug.Log(pos1.ToString());
        //Debug.Log(pos2.ToString());
        
        // Take one screen postions and subtract it component wise from the other, x-x y-y
        Vector2 delta = pos1 - pos2;

        // Get length and angle of vector
        float length = delta.magnitude;
        float angle = Vector2.SignedAngle(new Vector2(1, 0), delta);

        // Place object at midpoint and rotate at angle of new vector and stretch between the points.
        Vector2 midpoint = (pos1 + pos2) / 2;

        Vector3 position = new Vector3(midpoint.x, midpoint.y, 0f);

        line.GetComponent<RectTransform>().SetParent(this.transform);
        line.transform.SetSiblingIndex(1);

        RectTransform lineRect = line.GetComponent<RectTransform>();

        lineRect.anchoredPosition = position;
        lineRect.localEulerAngles = new Vector3(0, 0, angle);

        lineRect.sizeDelta = new Vector2(length, lineRect.sizeDelta.y);

        return line;
    }

    public void ClearBeams()
    {
        while (lightBeams.Count != 0)
        {
            Destroy(lightBeams.Pop());
        }
    }

    public void ClearColors()
    {
        for (int i = 0; i < prismReference.Length; i++)
        {
            for (int j = 0; j < prismReference.Length; j++)
            {
                PrismElement current = prismReference[i][j].GetComponent<PrismElement>();

                if (current.type == "mirror")
                {
                    current.GetComponentInChildren<Image>().sprite = mirrorImages["none"];
                }

                else if (current.type == "prism")
                {
                    current.GetComponentInChildren<Image>().sprite = prismImages["none"];
                }
            }
        }
    }

    public string PrimaryColorCombine(string color1, string color2)
    {
        string comboColor = " ";

        if (color1 == "red")
        {
            if (color2 == "blue")
            {
                comboColor = "magenta";
            }

            else if (color2 == "green")
            {
                comboColor = "yellow";
            }
        }

        else if (color1 == "blue")
        {
            if (color2 == "red")
            {
                comboColor = "magenta";
            }

            else if (color2 == "green")
            {
                comboColor = "cyan";
            }
        }

        else if (color1 == "green")
        {
            if (color2 == "blue")
            {
                comboColor = "cyan";
            }

            else if (color2 == "red")
            {
                comboColor = "yellow";
            }
        }

        return comboColor;
    }

}
