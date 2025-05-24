using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
public class Hint : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private float move;
    private float originalPosition;
    private float goal;
    private void Start()
    {
        originalPosition = rect.position.y;
        goal = originalPosition - move;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Woosh());
        }
    }

    private IEnumerator Woosh()
    {
        rect.DOMoveY(goal, 1f);
        yield return new WaitForSeconds(5f);
        rect.DOMoveY(originalPosition, 1f);
        yield return new WaitForSeconds(1f);
        Destroy(this);
    }
}
