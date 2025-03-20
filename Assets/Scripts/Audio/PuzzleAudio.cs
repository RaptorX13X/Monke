using UnityEngine;
using FMODUnity;

public class PuzzleAudio : MonoBehaviour
{
    FMOD.Studio.EventInstance SecretDoorOpenSound;
    FMOD.Studio.EventInstance StoneWheelSound;

    [SerializeField] private EventReference secretDoorOpenEvent;
    [SerializeField] private EventReference stoneWheelEvent;

    public void PlaySecretDoorOpen()
    {
        SecretDoorOpenSound = FMODUnity.RuntimeManager.CreateInstance(secretDoorOpenEvent);
        SecretDoorOpenSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        SecretDoorOpenSound.start();
        SecretDoorOpenSound.release();
    }
    public void PlayStoneWheel()
    {
        StoneWheelSound = FMODUnity.RuntimeManager.CreateInstance(stoneWheelEvent);
        StoneWheelSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        StoneWheelSound.start();
        StoneWheelSound.release();
    }

    public void StopStoneWheel()
    {
        StoneWheelSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
