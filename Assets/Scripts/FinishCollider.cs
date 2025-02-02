using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishCollider : MonoBehaviour
{
    [SerializeField] private int targetSceneBuildNumber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(targetSceneBuildNumber);
        }
    }
}
