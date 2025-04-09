using System;
using UnityEngine;

public class LeverAudio : MonoBehaviour
{
    private PlayerAudio playerAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerAudio audio))
        {
            playerAudio = audio;
        }
    }
    
    public void PlayLever()
    {
        if (playerAudio != null)
        {
            playerAudio.PlayLever();
        }
    }
}
