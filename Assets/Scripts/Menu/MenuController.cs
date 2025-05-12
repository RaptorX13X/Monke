using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private int gameSceneNumber;
    [SerializeField] private int gameSceneNumber2;
    [SerializeField] private GameObject menuObject;
    [SerializeField] private GameObject controlsObject;
    [SerializeField] private GameObject settingsObject;
    [SerializeField] private int screenWidth = 1920;
    [SerializeField] private int screenHeight = 1080;

    private void Awake()
    {
        menuObject.SetActive(true);
        controlsObject.SetActive(false);
        settingsObject.SetActive(false);
        Screen.SetResolution(screenWidth, screenHeight, FullScreenMode.ExclusiveFullScreen);
        Application.targetFrameRate = 60;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(gameSceneNumber);
    }
    public void StartGame2()
    {
        SceneManager.LoadSceneAsync(gameSceneNumber2);
    }

    public void ControlsButton()
    {
        menuObject.SetActive(false);
        settingsObject.SetActive(false);
        controlsObject.SetActive(true);
    }

    public void BackToMenuButton()
    {
        menuObject.SetActive(true);
        settingsObject.SetActive(false);
        controlsObject.SetActive(false);
    }

    public void SettingsButton()
    {
        menuObject.SetActive(false);
        settingsObject.SetActive(true);
        controlsObject.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
    
    
}
