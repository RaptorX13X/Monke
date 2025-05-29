using System;
using UnityEngine;

public class HanumanUnlock : MonoBehaviour
{
    [SerializeField] private GameObject gada;
    private PlayerStateMachine stateMachine;
    public DialogueSO dialogue;
    [SerializeField] private Hint hint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStateMachine machine))
        {
            stateMachine = machine;
        }

        if (other.TryGetComponent(out InputReader reader))
        {
            reader.InteractEvent += Interact;
            hint.HintE();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out InputReader reader))
        {
            reader.InteractEvent -= Interact;
        }
    }

    private void Interact()
    {
        TriggerDialogue();
        stateMachine.UnlockedHanuman = true;
        gada.SetActive(false);
        Destroy(this);
    }
    
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
        hint.HintQ();
    }
}
