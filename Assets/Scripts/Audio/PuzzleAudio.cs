using UnityEngine;
using FMODUnity;

public class PuzzleAudio : MonoBehaviour
{
    FMOD.Studio.EventInstance SecretDoorOpenSound;

    [SerializeField] private EventReference secretDoorOpenEvent;

    public void PlaySecretDoorOpen()
    {
        SecretDoorOpenSound = FMODUnity.RuntimeManager.CreateInstance(secretDoorOpenEvent);
        SecretDoorOpenSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        SecretDoorOpenSound.start();
        SecretDoorOpenSound.release();
    }
}
