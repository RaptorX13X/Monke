using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject pauseUI;
    private bool paused;
    [SerializeField] private int menuSceneNumber;

    private void Awake()
    {
        paused = false;
        Time.timeScale = 1f;
        inGameUI.SetActive(true);
        pauseUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause();
        }
    }

    private void OnPause()
    {
        if (!paused)
        {
            //input.EnablingPlayer();
            Time.timeScale = 0f;
            inGameUI.SetActive(false);
            pauseUI.SetActive(true);
            paused = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            //input.EnablingPlayer();
            Time.timeScale = 1f;
            inGameUI.SetActive(true);
            pauseUI.SetActive(false);
            paused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void Resume()
    {
        OnPause();
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(menuSceneNumber);
    }
}
