using UnityEngine;
using DG.Tweening;
using FMODUnity;
public class TorchPuzzle : MonoBehaviour
{
    [SerializeField] private Brazier[] braziers;
    private Vector3 target;
    [SerializeField] private Vector3 move;
    [SerializeField] private PuzzleAudio puzzleAudio;
    [SerializeField] private StudioEventEmitter emitter;
    [SerializeField] private bool dingSound;
    [SerializeField] private float doorDuration = 20f;
    private bool playedOnce;
    [SerializeField] private PlayerStateMachine player;
    
    private void Start()
    {
        target = transform.localPosition + move;
    }
    
    public void CheckCompletion()
    {
        foreach (var brazier in braziers)
        {
            if (!brazier.burning) return;
        } 
        if (dingSound && !playedOnce)
        {
            puzzleAudio.PlayPuzzleSolved();
            player.LoseTorch();
            playedOnce = true;
        }
        MoveDoor();
        emitter.Play();
    }
    
    
    private void MoveDoor()
    {
        transform.DOShakePosition(1f, 0.05f, 50, 90f, false, false);
        transform.DOLocalMove(target, doorDuration).SetDelay(1).SetLoops(0);
    }
}
