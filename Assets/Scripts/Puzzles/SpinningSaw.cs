using UnityEngine;
using DG.Tweening;
public class SpinningSaw : MonoBehaviour
{
    [SerializeField] private int damage = 100;
    [SerializeField]private Transform pathPoint1;
    [SerializeField]private Transform pathPoint2;
    void Start()
    {
        Vector3[] pathPoints = new[] { pathPoint1.position, pathPoint2.position };
        transform.DOLocalRotate(new Vector3(0, 0, 360), 0.5f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        transform.DOPath(pathPoints, 3f, PathType.Linear, PathMode.Full3D).SetLoops(-1).SetEase(Ease.Linear);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            health.DealDamage(damage);
        }
    }
}
