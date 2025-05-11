using UnityEngine;

public class Attacher2 : MonoBehaviour
{
    public PlayerStateMachine stateMachine;
    public InputReader inputReader;
    [SerializeField] private SecondReelPuzzle puzzle;
    public PlayerAudio playerAudio;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out InputReader input))
        {
            input.InteractEvent += Interact;
            inputReader = input;
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
        if (inputReader == null) return;
        if (puzzle.isComplete) return;
        if (puzzle.isAttached)
        {
            puzzle.DetachPlayer();
            return;
        }
        if (puzzle.requireHanuman && !stateMachine.HanumanBool)
        {
            puzzle.NeedHanuman();
            return;
        }
        puzzle.AttachPlayer(stateMachine, inputReader, transform, playerAudio);
    }
}
