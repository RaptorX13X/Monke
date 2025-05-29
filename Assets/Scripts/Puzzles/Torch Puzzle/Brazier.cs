using UnityEngine;
using FMODUnity;
using UnityEngine.UIElements;

public class Brazier : MonoBehaviour
{
    private PlayerStateMachine stateMachine;
    private InputReader inputReader;
    [SerializeField] private TorchPuzzle puzzle;
    public bool burning;
    public bool canInteract = true;
    [SerializeField] private GameObject fakeFire;

    [SerializeField] private StudioEventEmitter emitter;
    [SerializeField] private Hint hint;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out InputReader input))
            {
                input.InteractEvent += Interact;
                inputReader = input;
                if (canInteract)
                {
                    hint.HintE();
                }
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
            if (!stateMachine.hasTorch) return;
            if (canInteract)
            {
                burning = true;
                puzzle.CheckCompletion();
                canInteract = false;
            emitter.Play();
            fakeFire.SetActive(true);
            }
        }
}
