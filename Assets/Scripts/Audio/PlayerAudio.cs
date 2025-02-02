using UnityEngine;
using FMODUnity;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class PlayerAudio : MonoBehaviour
{
    FMOD.Studio.EventInstance FootstepsSound;
    FMOD.Studio.EventInstance JumpSound;
    FMOD.Studio.EventInstance LandSound;
    FMOD.Studio.EventInstance PlayerDamageSound;
    FMOD.Studio.EventInstance PlayerAttackSound;
    FMOD.Studio.EventInstance RegularDeathSound;
    FMOD.Studio.EventInstance FallToDeathSound;


    [SerializeField] private EventReference footstepsEvent;
    [SerializeField] private EventReference jumpEvent;
    [SerializeField] private EventReference landEvent;
    [SerializeField] private EventReference playerDamageEvent;
    [SerializeField] private EventReference playerAttackEvent;
    [SerializeField] private EventReference regularDeathEvent;
    [SerializeField] private EventReference fallToDeathEvent;


    [SerializeField] private CharacterController characterController;


    public void PlayFootsteps()
    {
        if(true) // characterController.isGrounded
        {

            RaycastHit hit;
            Debug.Log("Attempting steps");
            if ((Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))) //characterController.height + 0.5f
            {
                if (hit.collider.CompareTag("gravel"))
                {
                    Debug.Log("gravel walk");
                    FootstepsSound = FMODUnity.RuntimeManager.CreateInstance(footstepsEvent);
                    FootstepsSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
                    FootstepsSound.setParameterByNameWithLabel("Surface", "gravel");
                    FootstepsSound.start();
                    FootstepsSound.release();
                }
                else if (hit.collider.CompareTag("stone"))
                {
                    Debug.Log("stone walk");
                    FootstepsSound = FMODUnity.RuntimeManager.CreateInstance(footstepsEvent);
                    FootstepsSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
                    FootstepsSound.setParameterByNameWithLabel("Surface", "stone");
                    FootstepsSound.start();
                    FootstepsSound.release();
                }
                else
                {
                    Debug.Log("whatever walk");
                    FootstepsSound = FMODUnity.RuntimeManager.CreateInstance(footstepsEvent);
                    FootstepsSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
                    FootstepsSound.setParameterByNameWithLabel("Surface", "gravel");
                    FootstepsSound.start();
                    FootstepsSound.release();
                }
            }
            else
            {
                Debug.Log("Raycast failed");
            }

        }


    }

    public void PlayJump()
    {
        JumpSound = FMODUnity.RuntimeManager.CreateInstance(jumpEvent);
        FMODUnity.RuntimeManager.PlayOneShotAttached(jumpEvent, gameObject);
        JumpSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, characterController.height + 2f))
        {

            if (hit.collider.CompareTag("gravel"))
            {
                Debug.Log("gravel jump");
                JumpSound.setParameterByNameWithLabel("Surface", "gravel");
                JumpSound.start();
            }
            else if (hit.collider.CompareTag("stone"))
            {
                JumpSound.setParameterByNameWithLabel("Surface", "stone");
                JumpSound.start();
            }
            else
            {
                JumpSound.setParameterByNameWithLabel("Surface", "gravel");
                JumpSound.start();
            }
        }

        JumpSound.release();
    }

    public void PlayLanding()
    {
        LandSound = FMODUnity.RuntimeManager.CreateInstance(landEvent);
        FMODUnity.RuntimeManager.PlayOneShotAttached(landEvent, gameObject);
        LandSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, characterController.height + 0.5f))
        {

            if (hit.collider.CompareTag("gravel"))
            {
                Debug.Log("gravel land");
                LandSound.setParameterByNameWithLabel("Surface", "gravel");
                LandSound.start();
            }
            else if (hit.collider.CompareTag("stone"))
            {
                LandSound.setParameterByNameWithLabel("Surface", "stone");
                LandSound.start();
            }
            else
            {
                LandSound.setParameterByNameWithLabel("Surface", "gravel");
                LandSound.start();
            }
        }

        LandSound.release();
    }

    public void PlayDamage() 
    {
        PlayerDamageSound = FMODUnity.RuntimeManager.CreateInstance(playerDamageEvent);
        PlayerDamageSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        PlayerDamageSound.start();
        PlayerDamageSound.release();
    }

    public void PlayAttack()
    {
        PlayerAttackSound = FMODUnity.RuntimeManager.CreateInstance(playerAttackEvent);
        PlayerAttackSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        PlayerAttackSound.start();
        PlayerAttackSound.release();
    }

    public void PlayDeath()
    {
        RegularDeathSound = FMODUnity.RuntimeManager.CreateInstance(regularDeathEvent);
        RegularDeathSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        RegularDeathSound.start();
        RegularDeathSound.release();
    }

    public void PlayDeathByFalling()
    {
        FallToDeathSound = FMODUnity.RuntimeManager.CreateInstance(fallToDeathEvent);
        FallToDeathSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        FallToDeathSound.start();
        FallToDeathSound.release();
    }
}
