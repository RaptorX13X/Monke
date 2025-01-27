using System;
using UnityEngine;

public class PressurePlateCheat : MonoBehaviour
{
    [SerializeField] private GameObject objectToDisable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objectToDisable.SetActive(false);
        }
    }
}
