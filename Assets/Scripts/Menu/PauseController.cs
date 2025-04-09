using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject levelFinishedUI;
    private bool paused;
    private bool canPause => !levelFinishedUI.activeInHierarchy;
    [SerializeField] private int menuSceneNumber;
    [SerializeField] private int gameSceneNumber;
    [SerializeField] private int level2SceneNumber;

    private void Awake()
    {
        paused = false;
        Time.timeScale = 1f;
        inGameUI.SetActive(true);
        pauseUI.SetActive(false);
        levelFinishedUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause();
        }
    }

    public void FinishedLevel()
    {
        inGameUI.SetActive(false);
        pauseUI.SetActive(false);
        levelFinishedUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void OnPause()
    {
        if (canPause)
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
    }

    public void Resume()
    {
        OnPause();
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(menuSceneNumber);
    }

    public void Restart()
    {
        SceneManager.LoadScene(gameSceneNumber);
    }

    public void Level2()
    {
        SceneManager.LoadScene(level2SceneNumber);
    }
}
