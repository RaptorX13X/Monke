using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using DG.Tweening;
using FMODUnity;

public class HeadPuzzle : MonoBehaviour
{
    [SerializeField] private Transform headParent;
    [SerializeField] private float desiredRotation;
    public bool isSet;
    public bool canInteract = true;
    private InputReader reader;
    private Vector3 rotation = new Vector3(0, 90, 0);
    public Vector3 targetRotation;
    [SerializeField] private StudioEventEmitter puzzleEmitter;
    [SerializeField] private Hint hint;

    private void Update()
    {
        targetRotation = transform.rotation.eulerAngles - rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out InputReader input))
        {
            input.InteractEvent += Interact;
            hint.HintE();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out InputReader input))
        {
            input.InteractEvent -= Interact;
        }
    }

    private void Interact()
    {
        if (!canInteract) return;
        if (isSet) return;
        puzzleEmitter.Play();
        StartCoroutine(Rotation());
    }

    private IEnumerator Rotation()
    {
        canInteract = false;
        headParent.DOLocalRotate(targetRotation, 1.5f, RotateMode.FastBeyond360);
        yield return new WaitForSeconds(1.5f);
        if (Math.Abs(transform.rotation.eulerAngles.y - desiredRotation) == 0f)
        {
            isSet = true;
        }
        canInteract = true;
    }
}
