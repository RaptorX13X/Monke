using System;
using UnityEngine;

public class HanumanUnlock : MonoBehaviour
{
    [SerializeField] private GameObject gada;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStateMachine stateMachine))
        {
            stateMachine.UnlockedHanuman = true;
            gada.SetActive(false);
            Destroy(this);
        }
    }
}
