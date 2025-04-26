using UnityEditorInternal;
using UnityEngine;
using FMODUnity;

public class MusicTrigger : MonoBehaviour
{
    FMOD.Studio.EventInstance MusicSound;
    [SerializeField] private EventReference musicEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MusicManager.instance.ChangeMusic(MusicSound,musicEvent);
            Destroy(this);
        }
    }
}
