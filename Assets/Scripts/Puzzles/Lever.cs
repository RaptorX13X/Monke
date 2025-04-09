using System;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool isSet;
    [SerializeField] private Animator animator;
    [SerializeField] private LeverPuzzle puzzle;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out InputReader input))
        {
            input.InteractEvent += Interact;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out InputReader input))
        {
            input.InteractEvent -= Interact;
        }
    }

    public void Interact()
    {
        Debug.Log("interacted");
        animator.SetTrigger("interact");
        isSet = !isSet;
        puzzle.CheckCompletion();
    }
}
