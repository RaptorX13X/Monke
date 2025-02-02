using UnityEngine;
using FMODUnity;

public class AmbientTrigger : MonoBehaviour
{
    public StudioEventEmitter jungleEmitter;  
    public StudioEventEmitter caveEmitter;
    public StudioEventEmitter jungle3dEmitter;
    public StudioEventEmitter cave3dEmitter;

    private bool isInCave;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!isInCave)
            {
                jungleEmitter.Stop();
                caveEmitter.Play();
                isInCave = true;
                jungle3dEmitter.Play();
                //cave3dEmitter.Stop();
                cave3dEmitter.EventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
            else
            {
                jungleEmitter.Play();
                caveEmitter.Stop();
                isInCave = false;
                jungle3dEmitter.Stop();
                cave3dEmitter.Play();
            }
        }
    }
}
