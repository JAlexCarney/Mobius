using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    // time of transition in frames (50fps)
    private int sceneTransitionDelay = 50;
    private int count = 0;
    private bool transitioning = false;
    private int playerBeingTransitioned = -1;
    private int loopBeingEntered = -1;
    private bool toCredits = false;
    // transition object
    public GameObject transitionObj;
    public int player = -1;

    public void Start()
    {
        transitionObj.GetComponent<Animator>().enabled = false;
    }

    public void FixedUpdate()
    {
        if (transitioning)
        {
            if (count == sceneTransitionDelay)
            {
                transitioning = false;
                if (toCredits)
                {
                    string sceneName = "Credits";
                    SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
                }
                else 
                {
                    LoadRoom();
                }
            }
            else
            {
                count++;
            }
        }
    }

    public void Update()
    {
        
    }

    public void PlayerOneStart()
    {
        playerBeingTransitioned = 1;
        loopBeingEntered = 1;
        player = 1;
        LoadRoom();
    }

    public void PlayerTwoStart()
    {

        playerBeingTransitioned = 2;
        loopBeingEntered = 1;
        player = 2;
        LoadRoom();
    }

    public void LoadRoom()
    {
        string sceneName = "P" + playerBeingTransitioned + "Iteration" + loopBeingEntered;
        Debug.Log(player);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void StartMenu()
    {
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }

    public void P1EnterLoop2()
    {
        playerBeingTransitioned = 1;
        loopBeingEntered = 2;
        TransitionWithAnimation();
    }

    public void P1EnterLoop3()
    {
        Debug.Log("ENTERING LOOP 3");
        playerBeingTransitioned = 1;
        loopBeingEntered = 3;
        TransitionWithAnimation();
    }

    public void P2EnterLoop2()
    {
        playerBeingTransitioned = 2;
        loopBeingEntered = 2;
        TransitionWithAnimation();
    }

    public void P2EnterLoop3()
    {
        playerBeingTransitioned = 2;
        loopBeingEntered = 3;
        TransitionWithAnimation();
    }

    public void LoadCredits()
    {
        toCredits = true;
        TransitionWithAnimation();
    }

    private void TransitionWithAnimation()
    {
        count = 0;
        transitioning = true;
        transitionObj.GetComponent<Animator>().enabled = true;
    }


}
