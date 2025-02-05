using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishCollider : MonoBehaviour
{
    [SerializeField] private PauseController pauseController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pauseController.FinishedLevel();
        }
    }
}
