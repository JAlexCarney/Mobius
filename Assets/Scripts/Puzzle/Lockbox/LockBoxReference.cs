using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class LockBoxReference : MonoBehaviour
{
    // Event to invoke upon success or failure.
    public UnityEvent winEvent;
    public UnityEvent failEvent;

    public Transform[] locations;
    public Dictionary<int, int> starLocations = new Dictionary<int, int>(); //(star, location) 
    [SerializeField] public Sol[] solution;
    

    public void CheckSolution()
    {


        bool soFarSoGood = true;
        for (int i = 0; i < solution.Length; i++)
        {
            Sol sol = solution[i]; 
            foreach (int star in sol.stars)
            {
                if (starLocations[star] != sol.location)
                {
                    soFarSoGood = false;
                    break;  
                }
            }
            if (!soFarSoGood)
                break; 
        }
        if (soFarSoGood)
        {
            winEvent.Invoke();
        }
        else 
        {
            failEvent.Invoke();
        }
    }

    //open lockbox

}

[System.Serializable]
public struct Sol
{
    public int location;
    public int[] stars;
}
