using System.Collections;
using UnityEngine;

public class PushableObject : MonoBehaviour
{
    [SerializeField] private Collider trigger;

    public void DisableTrigger()
    {
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1f);
        trigger.enabled = false;
    }
}
