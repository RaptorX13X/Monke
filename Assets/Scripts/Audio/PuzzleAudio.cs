using UnityEngine;
using FMODUnity;

public class PuzzleAudio : MonoBehaviour
{
    FMOD.Studio.EventInstance SecretDoorOpenSound;
    FMOD.Studio.EventInstance StoneWheelSound;
    FMOD.Studio.EventInstance PuzzleSolvedSound;
    FMOD.Studio.EventInstance MiddlePuzzleSound;

    [SerializeField] private EventReference secretDoorOpenEvent;
    [SerializeField] private EventReference stoneWheelEvent;
    [SerializeField] private EventReference puzzleSolvedEvent;
    [SerializeField] private EventReference middleOfPuzzleSolvedEvent;

    public void PlaySecretDoorOpen()
    {
        SecretDoorOpenSound = FMODUnity.RuntimeManager.CreateInstance(secretDoorOpenEvent);
        SecretDoorOpenSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        SecretDoorOpenSound.start();
        SecretDoorOpenSound.release();
        Debug.Log("door opened");
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

    public void PlayPuzzleSolved()
    {
        PuzzleSolvedSound = FMODUnity.RuntimeManager.CreateInstance(puzzleSolvedEvent);
        PuzzleSolvedSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        PuzzleSolvedSound.start();
        PuzzleSolvedSound.release();
        Debug.Log("puzzle solved");
    }

    public void PlayMiddleOfPuzzle()
    {
        MiddlePuzzleSound = FMODUnity.RuntimeManager.CreateInstance(middleOfPuzzleSolvedEvent);
        MiddlePuzzleSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        MiddlePuzzleSound.start();
        MiddlePuzzleSound.release();
    }
}
