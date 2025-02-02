using System;
using UnityEngine;

public class TestRespawnHandler : MonoBehaviour
{
    public Transform respawnPoint;
    [SerializeField] private CharacterController controller;

    // public void Update()
    // {
    //     if (transform.position.y <= -10f)
    //     {
    //         Respawn();
    //     }
    // }

    public void Respawn()
    {
        controller.enabled = false;
        transform.position = respawnPoint.position;
        controller.enabled = true;
    }

    public void SetRespawn(Transform newRespawn)
    {
        respawnPoint = newRespawn;
    }
}
