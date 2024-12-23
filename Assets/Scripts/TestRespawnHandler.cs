using System;
using UnityEngine;

public class TestRespawnHandler : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private CharacterController controller;

    public void Update()
    {
        if (transform.position.y <= -10f)
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        controller.enabled = false;
        transform.position = respawnPoint.position;
        controller.enabled = true;
    }
}
