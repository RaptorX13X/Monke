using UnityEngine;
using DG.Tweening;

public class LeverPuzzle : MonoBehaviour
{
    [SerializeField] private Lever[] levers;
    private Vector3 target;
    [SerializeField] private Vector3 move;
    
    private void Start()
    {
        target = transform.localPosition + move;
    }

    public void CheckCompletion()
    {
        foreach (var lever in levers)
        {
            if (!lever.isSet) return;
        } 
        
        MoveDoor();
    }
    
    
    private void MoveDoor()
    {
        
        transform.DOShakePosition(1f, 0.05f, 50, 90f, false, false);
        transform.DOLocalMove(target, 5).SetDelay(1).SetLoops(0);

    }
}
