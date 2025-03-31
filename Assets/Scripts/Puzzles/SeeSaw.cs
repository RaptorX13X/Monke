using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class SeeSaw : MonoBehaviour
{
    [SerializeField] private float RotationZLeft;
    [SerializeField] private float RotationZRight;

    private void Start()
    {
        //StartCoroutine(Swinging());
        transform.DOLocalRotate(new Vector3(0, 0, RotationZLeft), 1f, RotateMode.Fast).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCirc);
    }

    private IEnumerator Swinging()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            transform.DOLocalRotate(new Vector3(0, 0, RotationZLeft), 1f, RotateMode.Fast).SetLoops(0);
            yield return new WaitForSeconds(2f);
            transform.DOLocalRotate(new Vector3(0, 0, RotationZRight), 1f, RotateMode.Fast).SetLoops(0);
            yield return new WaitForSeconds(1f);
        }
    }
}
