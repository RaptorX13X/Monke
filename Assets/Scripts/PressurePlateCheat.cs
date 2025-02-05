using System;
using UnityEngine;

public class PressurePlateCheat : MonoBehaviour
{
    [SerializeField] private Transform center;
    [SerializeField] private Collider target;
    public bool isSet = false;
    [SerializeField] private bool isSpecial;
    [SerializeField] private PressurePlateCheat theSpecialOne;

    private void OnTriggerEnter(Collider other)
    {
        if (isSpecial && !theSpecialOne.isSet) return;
        if (other.CompareTag("PuzzleBlock") && other == target)
        {
            other.transform.position = center.position;
            isSet = true;
        }
    }
}
