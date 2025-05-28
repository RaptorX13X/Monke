using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject levelFinishedUI;
    [SerializeField] private GameObject optionsUI;

    private bool paused;
    private bool canPause => !levelFinishedUI.activeInHierarchy;
    [SerializeField] private int menuSceneNumber;
    [SerializeField] private int gameSceneNumber;
    [SerializeField] private int level2SceneNumber;
    
    
    [SerializeField] private Animator animator;

    private void Awake()
    {
        paused = false;
        Time.timeScale = 1f;
        inGameUI.SetActive(true);
        pauseUI.SetActive(false);
        levelFinishedUI.SetActive(false);
        animator.gameObject.SetActive(true);
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
                MusicManager.instance.PauseMusic();
                //input.EnablingPlayer();
                Time.timeScale = 0f;
                inGameUI.SetActive(false);
                pauseUI.SetActive(true);
                optionsUI.SetActive(false);
                paused = true;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            else
            {
                MusicManager.instance.UnpausedMusic();
                //input.EnablingPlayer();
                Time.timeScale = 1f;
                inGameUI.SetActive(true);
                pauseUI.SetActive(false);
                optionsUI.SetActive(false);
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
        animator.SetTrigger("FadeOut");
        StartCoroutine(StartGameFade());
    }
    
    private IEnumerator StartGameFade()
    {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(1.1f);
        MusicManager.instance.StopMusic();
        SceneManager.LoadSceneAsync(menuSceneNumber);
    }

    public void Restart()
    {
        MusicManager.instance.StopMusic();
        SceneManager.LoadScene(gameSceneNumber);
    }

    public void Level2()
    {
        animator.SetTrigger("FadeOut");
        StartCoroutine(StartGameFade2());
    }
    
    private IEnumerator StartGameFade2()
    {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(1.1f);
        MusicManager.instance.StopMusic();
        SceneManager.LoadScene(level2SceneNumber);
    }

    public void Settings()
    {
        inGameUI.SetActive(false);
        pauseUI.SetActive(false);
        optionsUI.SetActive(true);
    }

    public void ReturnButton()
    {
        inGameUI.SetActive(false);
        pauseUI.SetActive(true);
        optionsUI.SetActive(false);
    }
}
