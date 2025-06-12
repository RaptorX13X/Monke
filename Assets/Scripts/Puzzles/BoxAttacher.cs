using UnityEngine;

public class BoxAttacher : MonoBehaviour
{
    public PlayerStateMachine stateMachine;
    public InputReader inputReader;
    [SerializeField] private PushableObject box;
    public PlayerAudio playerAudio;
    [SerializeField] private Hint hint;
    [SerializeField] private BoxCollider collider;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out InputReader input))
        {
            input.InteractEvent += Interact;
            inputReader = input;
            hint.HintE();
        }

        if (other.TryGetComponent(out PlayerStateMachine _stateMachine))
        {
            stateMachine = _stateMachine;
        }

        if (other.TryGetComponent(out PlayerAudio audio))
        {
            playerAudio = audio;
        }

        box.attacher = collider;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out InputReader input))
        {
            input.InteractEvent -= Interact;
            inputReader = null;
        }
        if (other.TryGetComponent(out PlayerStateMachine _stateMachine))
        {
            stateMachine = null;
        }
        
        if (other.TryGetComponent(out PlayerAudio audio))
        {
            playerAudio = null;
        }

        box.attacher = null;
    }

    private void Interact()
    {
        if (inputReader == null) return;
        if (box.isSet) return;
        if (box.isAttached)
        {
            box.DetachPlayer();
            return;
        }
        box.AttachPlayer(stateMachine, inputReader, transform, playerAudio);
    }
}
