using System;
using UnityEngine;
using DG.Tweening;
public class PuzzleResult : MonoBehaviour
{
    [SerializeField] private PressurePlateCheat plate;
    private bool happened = false;
    [SerializeField] private Vector3 move;
    private Vector3 target;

    private void Start()
    {
        target = transform.localPosition + move;
    }

    private void Update()
    {
        if (plate.isSet && !happened)
        {
            MoveDoor();
            happened = true;
        }
    }

    private void MoveDoor()
    {
        transform.DOShakePosition(1f, 0.05f, 50, 90f, false, false);
        transform.DOLocalMove(target, 5).SetDelay(1).SetLoops(0);
        //transform.position += move;
    }
}
