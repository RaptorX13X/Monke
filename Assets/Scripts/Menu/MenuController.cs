using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private int gameSceneNumber;
    [SerializeField] private GameObject menuObject;
    [SerializeField] private GameObject controlsObject;
    [SerializeField] private int screenWidth = 640;
    [SerializeField] private int screenHeight = 480;

    private void Awake()
    {
        menuObject.SetActive(true);
        controlsObject.SetActive(false);
        Screen.SetResolution(screenWidth, screenHeight, FullScreenMode.ExclusiveFullScreen);
        Application.targetFrameRate = 60;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(gameSceneNumber);
    }

    public void ControlsButton()
    {
        menuObject.SetActive(false);
        controlsObject.SetActive(true);
    }

    public void BackToMenuButton()
    {
        menuObject.SetActive(true);
        controlsObject.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
    
    
}
