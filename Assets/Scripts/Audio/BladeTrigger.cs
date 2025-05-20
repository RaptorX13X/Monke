using UnityEngine;

public class BladeTrigger : MonoBehaviour
{
    [SerializeField] BladeAudio bladeAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bladeAudio.muted = !bladeAudio.muted;
        }
    }
}
