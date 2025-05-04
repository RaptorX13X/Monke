using UnityEngine;
using DG.Tweening;
public class GadaSrada : MonoBehaviour
{
    [SerializeField] private Vector3 rotateValue;
    [SerializeField] private float speed;
    [SerializeField] private float speedUpDown;
    
    [SerializeField]private Transform pathPoint1;
    [SerializeField]private Transform pathPoint2;
    void Start()
    {
        Vector3[] pathPoints = new[] { pathPoint1.position, pathPoint2.position };
        
        transform.DOLocalRotate(rotateValue, speed, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        transform.DOPath(pathPoints, speedUpDown, PathType.Linear, PathMode.Full3D).SetLoops(-1).SetEase(Ease.InOutSine);
    }
}
