using UnityEngine;

public class Torch : MonoBehaviour
{
    private PlayerStateMachine stateMachine;
    private InputReader inputReader;
    private PlayerAudio playerAudio;
    
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
        gameObject.SetActive(false);
        
    }
}
