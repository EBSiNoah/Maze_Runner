using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject PauseButton;
    public bool IsPause;

    public void PauseGame()
    {
        if(IsPause)
        {
            Time.timeScale = 0f;
            IsPause = false;
            PauseMenu.SetActive(true);
            PauseButton.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            IsPause = true;
            PauseMenu.SetActive(false);
            PauseButton.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync("TestPathFind");
        if(!IsPause)
        {
            Time.timeScale = 1f;
            IsPause = true;
        }
        else
        {
            Time.timeScale = 0;
            IsPause = false;
        }
    }

    public void QuitGame()
    {
        SceneManager.LoadSceneAsync("Title");
        if (!IsPause)
        {
            Time.timeScale = 1f;
            IsPause = true;
        }
        else
        {
            Time.timeScale = 0;
            IsPause = false;
        }
    }

    void Start()
    {
        IsPause = true;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
}
