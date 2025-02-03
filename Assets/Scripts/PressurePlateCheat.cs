using System;
using UnityEngine;

public class PressurePlateCheat : MonoBehaviour
{
    [SerializeField] private GameObject objectToDisable;
    [SerializeField] private Transform center;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PuzzleBlock"))
        {
            other.transform.position = center.position;
            objectToDisable.SetActive(false);
        }
    }
}
