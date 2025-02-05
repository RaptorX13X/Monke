using System;
using UnityEngine;

public class PuzzleResult : MonoBehaviour
{
    [SerializeField] private PressurePlateCheat plate;
    private bool happened = false;
    [SerializeField] private Vector3 move;

    private void Update()
    {
        if (plate.isSet & !happened)
        {
            MoveDoor();
            happened = true;
        }
    }

    private void MoveDoor()
    {
        transform.position += move;
    }
}
