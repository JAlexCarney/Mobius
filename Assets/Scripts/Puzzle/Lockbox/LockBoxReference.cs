using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.UIElements;

public class LockBoxReference : MonoBehaviour
{
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
            Debug.Log("you did it!");
        else Debug.Log("sorry not the correct solution :(");
    }

    //open lockbox

}

[System.Serializable]
public struct Sol
{
    public int location;
    public int[] stars;
}
