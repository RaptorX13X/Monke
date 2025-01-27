using System;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TestRespawnHandler respawnHandler))
        {
            respawnHandler.SetRespawn(respawnPoint);
        }
    }
}
