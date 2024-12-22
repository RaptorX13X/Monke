using UnityEngine;
using FMODUnity;

public class PlayerAudio : MonoBehaviour
{
    FMOD.Studio.EventInstance FootstepsSound;
    FMOD.Studio.EventInstance JumpSound;
    FMOD.Studio.EventInstance LandSound;


    public EventReference footstepsEvent;
    public EventReference jumpEvent;
    public EventReference landEvent;

    
    [SerializeField] private CharacterController characterController;


    public void PlayFootsteps()
    {

        if(characterController.isGrounded)
        {

            RaycastHit hit;

            if ((Physics.Raycast(transform.position, Vector3.down, out hit, characterController.height + 0.5f)))
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
                    FootstepsSound = FMODUnity.RuntimeManager.CreateInstance(footstepsEvent);
                    FootstepsSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
                    FootstepsSound.setParameterByNameWithLabel("Surface", "stone");
                    FootstepsSound.start();
                    FootstepsSound.release();
                }
                else
                {
                    FootstepsSound = FMODUnity.RuntimeManager.CreateInstance(footstepsEvent);
                    FootstepsSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
                    FootstepsSound.setParameterByNameWithLabel("Surface", "gravel");
                    FootstepsSound.start();
                    FootstepsSound.release();
                }
            }


        }


    }

    public void PlayJump()
    {
        JumpSound = FMODUnity.RuntimeManager.CreateInstance(jumpEvent);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, characterController.height + 0.5f))
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
}
