using UnityEngine;
using FMODUnity;
using DG.Tweening;

public class TwoDoorDoor : MonoBehaviour
{
    [SerializeField] private HeadDoor head;
    [SerializeField] private PuzzleResult box;
    
    public bool happened = false;
    [SerializeField] private Vector3 move;
    private Vector3 target;
    [SerializeField] private PuzzleAudio puzzleAudio;
    [SerializeField] private StudioEventEmitter emitter;
    
    private void Start()
    {
        target = transform.localPosition + move;
    }
    private void Update()
    {
        if  ((head.happened && box.happened) && !happened)
        {
            emitter.Play();
            MoveDoor();
            puzzleAudio.PlayPuzzleSolved();
            happened = true;
        }
    }
    
    private void MoveDoor()
    {
        transform.DOShakePosition(1f, 0.05f, 50, 90f, false, false);
        transform.DOLocalMove(target, 5).SetDelay(1).SetLoops(0);
    }
}
