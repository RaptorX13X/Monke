using UnityEngine;
using FMODUnity;
using DG.Tweening;
public class ReelDoor2 : MonoBehaviour
{
    [SerializeField] private SecondReelPuzzle reelPuzzle;
    private bool happened = false;
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
        if  (reelPuzzle.rotated1 && !happened && reelPuzzle != null)
        {
            emitter.Play();
            MoveDoor();
            puzzleAudio.PlayMiddleOfPuzzle();
            happened = true;
        }
    }
    
    private void MoveDoor()
    {
        transform.DOShakePosition(1f, 0.05f, 50, 90f, false, false);
        transform.DOLocalMove(target, 5).SetDelay(1).SetLoops(0);
    }
}
