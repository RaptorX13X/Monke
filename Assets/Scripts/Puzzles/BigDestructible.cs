using UnityEngine;

public class BigDestructible : MonoBehaviour
{
    //[SerializeField] private Animator animator;
    [SerializeField] private Collider collider;
    [SerializeField] private MeshRenderer renderer;
    private PlayerAudio playerAudio;
    private PlayerStateMachine stateMachine;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private Hint hint;
    

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
            hint.HintE();
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
        particles.Play();
        if (playerAudio != null)
        {
            playerAudio.PlaySmashBigBoy();
        }
        Destroy(this);
    }
}
