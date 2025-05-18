using FMODUnity;
using UnityEngine;

public class BladeAudio : MonoBehaviour
{
    public bool muted;
    [SerializeField] StudioEventEmitter BladeEmitter;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (muted)
        {
            BladeEmitter.EventInstance.setVolume(0f);


        }
        else
        {
            BladeEmitter.EventInstance.setVolume(1f);
        }
    }


}
