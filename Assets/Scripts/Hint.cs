using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
public class Hint : MonoBehaviour
{
    [SerializeField] private RectTransform rectQ;
    [SerializeField] private RectTransform rectE;
    [SerializeField] private float moveQ;
    [SerializeField] private float moveE;
    private float originalPositionQ;
    private float originalPositionE;
    private float goalQ;
    private float goalE;

    [SerializeField] private RectTransform HiddenHint;
    [SerializeField] private RectTransform OutHintE;
    [SerializeField] private RectTransform OutHintQ;
    private void Start()
    {
        originalPositionQ = rectQ.position.y;
        goalQ = rectQ.position.y - (rectQ.position.y - OutHintQ.position.y);
        originalPositionE = rectE.position.y;
        goalE = rectE.position.y - (rectE.position.y - OutHintE.position.y);
    }

    public void HintQ()
    {
        StartCoroutine(WooshQ());
    }

    public void HintE()
    {
        StartCoroutine(WooshE());
    }

    private IEnumerator WooshQ()
    {
        rectQ.DOMoveY(goalQ, 1f);
        yield return new WaitForSeconds(5f);
        rectQ.DOMoveY(originalPositionQ, 1f);
        yield return new WaitForSeconds(1f);
    }
    
    private IEnumerator WooshE()
    {
        rectE.DOMoveY(goalE, 1f);
        yield return new WaitForSeconds(5f);
        rectE.DOMoveY(originalPositionE, 1f);
        yield return new WaitForSeconds(1f);
    }
}
