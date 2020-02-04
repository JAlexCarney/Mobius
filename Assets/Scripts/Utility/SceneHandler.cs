using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void PlayerOneStart()
    {
        LoadRoom(1, 1);
    }

    public void PlayerTwoStart()
    {
        LoadRoom(2, 1);
    }

    public void LoadRoom(int player, int room)
    {
        string sceneName = "P" + player + "Iteration" + room;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void StartMenu()
    {
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }

}
