using UnityEngine;
using FMODUnity;

public class MusicStop : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MusicManager.instance.StopMusic();
            Destroy(this);
        }
    }
}
