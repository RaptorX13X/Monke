using System;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool isSet;
    [SerializeField] private Animator animator;
    [SerializeField] private LeverPuzzle puzzle;
    private PlayerAudio playerAudio;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out InputReader input))
        {
            input.InteractEvent += Interact;
            if (other.TryGetComponent(out PlayerAudio audio))
            {
                playerAudio = audio;
            }
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
        if (playerAudio != null)
        {
            playerAudio.PlayLever();
        }
    }
}
