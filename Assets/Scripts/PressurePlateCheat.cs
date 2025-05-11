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
    public bool playerOn;
    [SerializeField] private Transform door;
    [SerializeField] private Vector3 playerMove;
    private Vector3 originalPos;
    private void Start()
    {
        originalPos = transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isSpecial && !theSpecialOne.isSet) return;
        if (other.CompareTag("PuzzleBlock") && other == target)
        {
            Vector3 movement = new Vector3(center.position.x, center.position.y + 1.5f, center.position.z);
            other.transform.DOShakePosition(1f, 0.05f, 50, 90f, false, false);
            other.transform.DOMove(movement, 1).SetDelay(0).SetLoops(0);
            isSet = true;
            other.attachedRigidbody.isKinematic = true;
            if (other.TryGetComponent(out PushableObject pushable))
            {
                pushable.DisableTrigger();
            }
        }
        else if (other.CompareTag("Player"))
        {
            ShakeDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopShake();
        }
    }
    
    private void ShakeDoor()
    {
        door.DOLocalMove(door.localPosition - playerMove, 1).SetDelay(0).SetLoops(0).OnComplete(StartShake);
    }
    
    private void StartShake()
    {
        door.DOShakePosition(1f, 0.2f, 50, 90f, false, true, ShakeRandomnessMode.Harmonic).SetLoops(-1);
    }
    private void StopShake()
    {
        door.DOLocalMove(originalPos, 1).SetDelay(0).SetLoops(0).OnComplete(StopAnim);
    }
    
    private void StopAnim()
    {
        door.DOPause();
    }
}
