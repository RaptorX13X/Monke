using System;
using UnityEngine;
using DG.Tweening;
public class PressurePlateCheat : MonoBehaviour
{
    [SerializeField] private Transform center;
    [SerializeField] private Collider target;
    public bool isSet = false;
    [SerializeField] private bool isSpecial;
    [SerializeField] private PressurePlateCheat theSpecialOne;

    private void OnTriggerEnter(Collider other)
    {
        if (isSpecial && !theSpecialOne.isSet) return;
        if (other.CompareTag("PuzzleBlock") && other == target)
        {
            Vector3 movement = new Vector3(center.position.x, center.position.y + 1.5f, center.position.z);
            other.transform.DOShakePosition(1f, 0.05f, 50, 90f, false, false);
            other.transform.DOMove(movement, 1).SetDelay(0).SetLoops(0);
            isSet = true;
        }
    }
}
