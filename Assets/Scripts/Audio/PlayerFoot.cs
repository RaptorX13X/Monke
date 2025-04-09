using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerFoot : MonoBehaviour
{
    [SerializeField] private PlayerAudio playerAudio;
    public bool canPlay = true;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("attempting audio from stópka");
        if (!canPlay) return;
        playerAudio.PlayFootsteps();

        if (other.CompareTag("gravel") || other.CompareTag("stone"))
        {
            Debug.Log("stupka dziala");
            
        }
    }
}
