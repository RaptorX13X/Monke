using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class MusicManager : MonoBehaviour
{
    private  EventInstance eventI;
    private  EventReference eventR;
    
    public static MusicManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeMusic(EventInstance eventInstance, EventReference eventRefernce)
    {

        
        eventI = eventInstance;
        eventR = eventRefernce;

        eventI = FMODUnity.RuntimeManager.CreateInstance(eventR);
        eventI.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        eventI.start();
    }

    public void StopMusic()
    {
        eventI.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        eventI.release();
    }

    public void PauseMusic()
    {
        eventI.setPaused(true);
    }

    public void UnpausedMusic()
    {
        eventI.setPaused(false);
    }
}
