using UnityEngine;

public class Attacher : MonoBehaviour
{
    public PlayerStateMachine stateMachine;
    public InputReader inputReader;
    [SerializeField] private FirstReelPuzzle puzzle;
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
    }

    private void Interact()
    {
        if (puzzle.isComplete) return;
        puzzle.AttachPlayer(stateMachine, inputReader, transform);
    }
}
