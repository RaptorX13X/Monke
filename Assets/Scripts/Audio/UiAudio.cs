using UnityEngine;
using FMODUnity;

public class UiAudio : MonoBehaviour
{

    FMOD.Studio.EventInstance UiSound;

    [SerializeField] private EventReference UiEvent;

    public void PlayUiSound(string action)
    {
        if (action == "hovered")
        {
            UiSound = FMODUnity.RuntimeManager.CreateInstance(UiEvent);
            UiSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            UiSound.setParameterByNameWithLabel("UI", "hovered");
            UiSound.start();
            UiSound.release();
        }
        else if (action == "clicked")
        {
            UiSound = FMODUnity.RuntimeManager.CreateInstance(UiEvent);
            UiSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            UiSound.setParameterByNameWithLabel("UI", "clicked");
            UiSound.start();
            UiSound.release();
        }
    }
}
