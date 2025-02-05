using UnityEngine;
using FMODUnity;

public class AmbientTrigger1 : MonoBehaviour
{
      
    
    public StudioEventEmitter jungle3dEmitter;
    public StudioEventEmitter caveEmitter;


    private bool isOutdoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!isOutdoor)
            {
                
                isOutdoor = true;
                jungle3dEmitter.Play();
                caveEmitter.EventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
            else
            {
                
                caveEmitter.Play();
                isOutdoor = false;
                jungle3dEmitter.Stop();
                
            }
        }
    }
}
