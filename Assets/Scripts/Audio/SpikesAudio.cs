using UnityEngine;
using FMODUnity;

public class SpikesAudio : MonoBehaviour
{
  
    [SerializeField] private Spikes spikes;
    [SerializeField] private StudioEventEmitter EmitterRise;
    [SerializeField] private StudioEventEmitter EmitterHide;

    public bool muted;


    private void Start()
    {
        spikes.rise += PlaySpikesRise;
        spikes.hide += PlaySpikesHide;
    }
    public void PlaySpikesRise()
    {
        EmitterRise.Play();
    }

    public void PlaySpikesHide()
    {
        EmitterHide.Play();
    }

    private void OnDestroy()
    {
        spikes.rise -= PlaySpikesRise;
        spikes.hide -= PlaySpikesHide;
    }
}
