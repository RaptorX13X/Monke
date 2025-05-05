using System;
using System.Collections;
using UnityEngine;

public class PushableObject : MonoBehaviour
{
    [SerializeField] private Collider trigger;
    private Vector3 respawnPoint;

    private void Start()
    {
        respawnPoint = transform.position;
    }

    public void DisableTrigger()
    {
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1f);
        trigger.enabled = false;
    }

    public void Respawn()
    {
        transform.position = respawnPoint;
    }
}
