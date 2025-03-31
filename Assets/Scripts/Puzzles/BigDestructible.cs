using UnityEngine;

public class BigDestructible : MonoBehaviour
{
    //[SerializeField] private Animator animator;
    [SerializeField] private Collider collider;
    [SerializeField] private MeshRenderer renderer;
    private PlayerAudio playerAudio;
    private PlayerStateMachine stateMachine;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out InputReader input))
        {
            input.InteractEvent += Interact;
            if (other.TryGetComponent(out PlayerAudio audio))
            {
                playerAudio = audio;
            }
            if (other.TryGetComponent(out PlayerStateMachine machine))
            {
                stateMachine = machine;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out InputReader input))
        {
            input.InteractEvent -= Interact;
        }
    }

    public void Interact()
    {
        if (!stateMachine.HanumanBool) return;
        //animator.SetTrigger("interact");
        collider.enabled = false;
        renderer.enabled = false;
        if (playerAudio != null)
        {
            playerAudio.PlaySmashBigBoy();
        }
    }
}
