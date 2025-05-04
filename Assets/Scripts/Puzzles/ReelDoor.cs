using System;
using UnityEngine;
using FMODUnity;
using DG.Tweening;


public class ReelDoor : MonoBehaviour
{
    [SerializeField] private FirstReelPuzzle reelPuzzle;
    private bool happened = false;
    [SerializeField] private Vector3 move;
    private Vector3 target;
    [SerializeField] private PuzzleAudio puzzleAudio;
    [SerializeField] private StudioEventEmitter emitter;
    public DialogueSO dialogue;


    private void Start()
    {
        target = transform.localPosition + move;
    }

    private void Update()
    {
        if  (reelPuzzle.isComplete && !happened && reelPuzzle != null)
        {
            emitter.Play();
            MoveDoor();
            puzzleAudio.PlayPuzzleSolved();
            TriggerDialogue();
            happened = true;
        }
    }
    
    private void MoveDoor()
    {
        transform.DOShakePosition(1f, 0.05f, 50, 90f, false, false);
        transform.DOLocalMove(target, 5).SetDelay(1).SetLoops(0);
    }
    
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
        Destroy(this);
    }
}
