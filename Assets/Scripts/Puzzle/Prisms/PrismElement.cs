using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismElement : MonoBehaviour
{

    public string type;
    public string orientation;
    public string colorToCast;
    public PrismReference prismReference;
    public int row;
    public int column;

    // Start is called before the first frame update
    void Start()
    {
        prismReference = this.GetComponentInParent<PrismReference>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
