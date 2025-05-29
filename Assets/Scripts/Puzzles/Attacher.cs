using UnityEngine;

public class Attacher : MonoBehaviour
{
    public PlayerStateMachine stateMachine;
    public InputReader inputReader;
    [SerializeField] private FirstReelPuzzle puzzle;
    public PlayerAudio playerAudio;
    [SerializeField] private Hint hint;
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
    }

    private void Interact()
    {
        if (puzzle.isComplete) return;
        if (puzzle.isAttached)
        {
            puzzle.DetachPlayer();
            return;
        }
        puzzle.AttachPlayer(stateMachine, inputReader, transform, playerAudio);
    }
}
