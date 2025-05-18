using UnityEngine;

public class SpikesTrigger : MonoBehaviour
{

    [SerializeField] SpikesAudio spikesAudio;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spikesAudio.muted = !spikesAudio.muted;
        }
    }
}
