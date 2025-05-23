using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private int damage = 100;
    [SerializeField] private Vector3 move;
    [SerializeField] private bool routine;
    private Vector3 target;
    private Vector3 origin;

    public Action  hide;
    public Action  rise;


    private void Start()
    {
        target = transform.localPosition + move;
        origin = transform.localPosition;
        if (!routine) return;
        //StartCoroutine(Routine());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            health.DealDamage(damage);
        }
    }

    //private IEnumerator Routine()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(3f);
    //        hide.Invoke();
    //        transform.DOLocalMove(target, 3).SetDelay(3).SetLoops(0);
            
    //        yield return new WaitForSeconds(3f);
    //        rise.Invoke();
    //        transform.DOLocalMove(origin, 3).SetDelay(3).SetLoops(0);
            
    //       // yield return new WaitForSeconds(3f);
    //    }
    //}
}
