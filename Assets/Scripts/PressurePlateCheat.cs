using System;
using UnityEngine;

public class PressurePlateCheat : MonoBehaviour
{
    [SerializeField] private Transform center;
    public bool isSet = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PuzzleBlock"))
        {
            other.transform.position = center.position;
            isSet = true;
        }
    }
}
