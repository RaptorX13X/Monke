using UnityEngine;
using DG.Tweening;
using FMODUnity;
public class LeverPuzzle : MonoBehaviour
{
    [SerializeField] private Lever[] levers;
    private Vector3 target;
    [SerializeField] private Vector3 move;
    [SerializeField] private PuzzleAudio puzzleAudio;
    [SerializeField] private StudioEventEmitter emitter;
    [SerializeField] private bool dingSound;
    [SerializeField] private float doorDuration = 20f;
    private bool playedOnce;
    [SerializeField] private bool replaceCollider;
    [SerializeField] private bool removeDialogue;
    [SerializeField] private bool triggerDialogue;
    [SerializeField] private GameObject dialogueToRemove;
    [SerializeField] private BoxCollider colliderToReplaceWith;
    [SerializeField] private MeshCollider colliderToReplace;
    public DialogueSO dialogue;
    
    
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
        if (dingSound && !playedOnce)
        {
            puzzleAudio.PlayPuzzleSolved();
            
            playedOnce = true;
        }
        MoveDoor();
        TriggerDialogue();
        emitter.Play();
    }
    
    
    private void MoveDoor()
    {
        
        transform.DOShakePosition(1f, 0.05f, 50, 90f, false, false).OnComplete(RemoveDialogue);
        transform.DOLocalMove(target, doorDuration).SetDelay(1).SetLoops(0).OnComplete(ReplaceCollider);

    }

    private void ReplaceCollider()
    {
        if (replaceCollider)
        {
            colliderToReplace.enabled = false;
            colliderToReplaceWith.enabled = true;
        }
    }

    private void RemoveDialogue()
    {
        if (removeDialogue)
        {
            dialogueToRemove.SetActive(false);
        }
    }
    
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
