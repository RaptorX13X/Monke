using System;
using UnityEngine;

public class HanumanUnlock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStateMachine stateMachine))
        {
            stateMachine.UnlockedHanuman = true;
        }
    }
}
