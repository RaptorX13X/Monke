using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private int damage = 100;
    [SerializeField] private Vector3 move;
    private Vector3 target;
    private Vector3 origin;
    
    private void Start()
    {
        target = transform.localPosition + move;
        origin = transform.localPosition;

        StartCoroutine(Routine());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            health.DealDamage(damage);
        }
    }

    private IEnumerator Routine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            transform.DOLocalMove(target, 3).SetDelay(3).SetLoops(0);
            yield return new WaitForSeconds(3f);
            transform.DOLocalMove(origin, 3).SetDelay(3).SetLoops(0);
            yield return new WaitForSeconds(3f);
        }
    }
}
