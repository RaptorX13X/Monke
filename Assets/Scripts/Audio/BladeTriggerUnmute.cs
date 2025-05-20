using UnityEngine;

public class BladeTriggerUnmute : MonoBehaviour
{
    [SerializeField] BladeAudio bladeAudio;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bladeAudio.muted = false;
        }
    }
}
