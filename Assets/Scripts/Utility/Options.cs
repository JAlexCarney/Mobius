using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    bool open = false;

    private void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        if (open)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    void Open()
    {
        Util.ActivateChildren(this.gameObject);
        open = true;
    }

    void Close()
    {
        Util.DeactivateChildren(this.gameObject);
        open = false;
    }

    public void ExitToDesktop()
    {
        Application.Quit();
    }

    public void Fullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
