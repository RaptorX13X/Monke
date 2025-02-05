using UnityEngine;
using DG.Tweening;
public class PuzzleResult3 : MonoBehaviour
{
    [SerializeField] private PressurePlateCheat plate;
    [SerializeField] private PressurePlateCheat plate2;
    private bool happened = false;
    [SerializeField] private Vector3 move;

    private Vector3 target;

    private void Start()
    {
        target = transform.localPosition + move;
    }
    private void Update()
    {
        if ((plate.isSet && plate2.isSet) && !happened)
        {
            MoveDoor();
            happened = true;
        }
    }

    private void MoveDoor()
    {
        transform.DOShakePosition(1f, 0.05f, 50, 90f, false, false);
        transform.DOLocalMove(target, 5).SetDelay(1).SetLoops(0);
        //transform.position += move;
    }
}
