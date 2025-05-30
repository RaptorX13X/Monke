using UnityEngine;

public class Torch : MonoBehaviour
{
    private PlayerStateMachine stateMachine;
    private InputReader inputReader;
    private PlayerAudio playerAudio;
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
        if (other.TryGetComponent(out PlayerAudio _playerAudio))
        {
            playerAudio = _playerAudio;
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
        if (other.TryGetComponent(out PlayerAudio _playerAudio))
        {
            playerAudio = null;
        }
    }

    private void Interact()
    {
        playerAudio.PlayTorch();
        stateMachine.PickUpTorch();
        inputReader.InteractEvent -= Interact;
        inputReader = null;
        stateMachine = null;
        playerAudio = null;
        gameObject.SetActive(false);
    }
}
