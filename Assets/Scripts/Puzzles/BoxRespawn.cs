using System;
using UnityEngine;

public class BoxRespawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PushableObject box))
        {
            box.Respawn();
        }
    }
}
