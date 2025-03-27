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
    FMOD.Studio.EventInstance PushingObjectSound;
    FMOD.Studio.EventInstance LeverSound;

    FMOD.Studio.EventInstance HanumanJumpSound;
    FMOD.Studio.EventInstance HanumanDamageSound;
    FMOD.Studio.EventInstance HanumanRegularDeathSound;
    FMOD.Studio.EventInstance HanumanFallToDeathSound;


    [SerializeField] private EventReference footstepsEvent;
    [SerializeField] private EventReference jumpEvent;
    [SerializeField] private EventReference landEvent;
    [SerializeField] private EventReference playerDamageEvent;
    [SerializeField] private EventReference playerAttackEvent;
    [SerializeField] private EventReference regularDeathEvent;
    [SerializeField] private EventReference fallToDeathEvent;
    [SerializeField] private EventReference pushingObjectEvent;
    [SerializeField] private EventReference leverEvent;

    [SerializeField] private EventReference hanumanJumpEvent;
    [SerializeField] private EventReference hanumanDamageEvent;
    [SerializeField] private EventReference hanumanRegularDeathEvent;
    [SerializeField] private EventReference hanumanFallToDeathEvent;

    [SerializeField] private CharacterController characterController;
    [SerializeField] private PlayerStateMachine stateMachine;

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
        RaycastHit hit;

        if (!stateMachine.HanumanBool)
        {
            JumpSound = FMODUnity.RuntimeManager.CreateInstance(jumpEvent);
            FMODUnity.RuntimeManager.PlayOneShotAttached(jumpEvent, gameObject);
            JumpSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));





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
        }
        else
        {
            HanumanJumpSound = FMODUnity.RuntimeManager.CreateInstance(hanumanJumpEvent);
            FMODUnity.RuntimeManager.PlayOneShotAttached(hanumanJumpEvent, gameObject);
            HanumanJumpSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));



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
        if (!stateMachine.HanumanBool)
        {
            PlayerDamageSound = FMODUnity.RuntimeManager.CreateInstance(playerDamageEvent);
            PlayerDamageSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            PlayerDamageSound.start();
            PlayerDamageSound.release();
        }
        else
        {
            HanumanDamageSound = FMODUnity.RuntimeManager.CreateInstance(hanumanDamageEvent);
            HanumanDamageSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            HanumanDamageSound.start();
            HanumanDamageSound.release();
        }
        
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
        if (!stateMachine.HanumanBool)
        {
            RegularDeathSound = FMODUnity.RuntimeManager.CreateInstance(regularDeathEvent);
            RegularDeathSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            RegularDeathSound.start();
            RegularDeathSound.release();
        }
        else
        {
            HanumanRegularDeathSound = FMODUnity.RuntimeManager.CreateInstance(hanumanRegularDeathEvent);
            HanumanRegularDeathSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            HanumanRegularDeathSound.start();
            HanumanRegularDeathSound.release();
        }
        
    }

    public void PlayDeathByFalling()
    {
        if (!stateMachine.HanumanBool)
        {
            FallToDeathSound = FMODUnity.RuntimeManager.CreateInstance(fallToDeathEvent);
            FallToDeathSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            FallToDeathSound.start();
            FallToDeathSound.release();
        }
        else
        {
            HanumanFallToDeathSound = FMODUnity.RuntimeManager.CreateInstance(hanumanFallToDeathEvent);
            HanumanFallToDeathSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            HanumanFallToDeathSound.start();
            HanumanFallToDeathSound.release();
        }
        
    }

    public void PlayPushingObject()
    {
        PushingObjectSound = FMODUnity.RuntimeManager.CreateInstance(pushingObjectEvent);
        PushingObjectSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        PushingObjectSound.start();
        PushingObjectSound.release();
    }
    
    public void PlayLever()
    {
        LeverSound = FMODUnity.RuntimeManager.CreateInstance(leverEvent);
        LeverSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        LeverSound.start();
        LeverSound.release();
    }

    public void StopPushingObject()
    {
        PushingObjectSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
