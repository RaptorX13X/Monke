using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionManager : MonoBehaviour
{
    private int resNumber;

    private int resWidth = 1920;
    private int resHeight = 1080;
    
    [SerializeField] private TextMeshProUGUI resolutionText;
    public Toggle vSyncToggle;
    public Toggle fullscreenToggle;

    private bool isFullscreen;

    private void Awake()
    {
        resNumber = 3;
        resolutionText.text = resWidth + " x " + resHeight;
        isFullscreen = Screen.fullScreen;
    }
    void Start()
    {
        // Ustaw odpowiedni toggle zgodnie z aktualnym ustawieniem VSync
        UpdateToggle();
    }

    private void ChangeDisplayText()
    {
        switch (resNumber)
        {
            case 0:
                resWidth = 1024;
                resHeight = 768;
                resolutionText.text = resWidth + " x " + resHeight;
                break;
            case 1:
                resWidth = 1200;
                resHeight = 1024;
                resolutionText.text = resWidth + " x " + resHeight;
                break;
            case 2:
                resWidth = 1366;
                resHeight = 768;
                resolutionText.text = resWidth + " x " + resHeight;
                break;
            case 3:
                resWidth = 1920;
                resHeight = 1080;
                resolutionText.text = resWidth + " x " + resHeight;
                break;
            case 4:
                resWidth = 2560;
                resHeight = 1440;
                resolutionText.text = resWidth + " x " + resHeight;
                break;
            case 5: 
                resWidth = 3440;
                resHeight = 1440;
                resolutionText.text = resWidth + " x " + resHeight;
                break;
        }
    }

    public void ToggleLeft()
    {
        if (resNumber > 0)
        {
            resNumber -= 1;
        }
        else
        {
            resNumber = 5;
        }
        ChangeDisplayText();
    }

    public void ToggleRight()
    {
        if (resNumber < 5)
        {
            resNumber += 1;
        }
        else
        {
            resNumber = 0;
        }
        ChangeDisplayText();
    }

    public void ApplyButton()
    {
        Screen.SetResolution(resWidth, resHeight, isFullscreen);
   
    }

    public void ToggleVsync(bool value)
    {
        QualitySettings.vSyncCount = value ? 1 : 0;
    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        isFullscreen = !isFullscreen;
    }
    
    private void UpdateToggle()
    {
        vSyncToggle.isOn = QualitySettings.vSyncCount != 0;
        fullscreenToggle.isOn = Screen.fullScreen;
    }
}
