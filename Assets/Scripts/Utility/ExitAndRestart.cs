using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitAndRestart : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
        }
    }
}
