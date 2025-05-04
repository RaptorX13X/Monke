using UnityEngine;
using DG.Tweening;
using FMODUnity;
public class DoorDialogueTrigger : MonoBehaviour
{
    public DialogueSO dialogue;
    public Transform door;
    public Vector3 move;
    private Vector3 target;
    [SerializeField] private StudioEventEmitter emitter;
    
    private void Start()
    {
        target = door.localPosition + move;
    }
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
        Destroy(this);
    }

    public bool isComplete()
    {
        return dialogue.isComplete;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MoveDoor();
            TriggerDialogue();
        }
    }
    
    private void MoveDoor()
    {
        emitter.Play();
        door.DOLocalMove(target, 0.5f).SetLoops(0);
    }
}
