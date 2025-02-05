using System;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            health.DealDamage(99999);
            if (other.TryGetComponent(out PlayerStateMachine stateMachine))
            {
                stateMachine.deathByFalling = true;
            }
        }
    }
}
