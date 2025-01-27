using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    
    protected void Move(Vector3 motion, float deltaTime)
    {
        //motion = AdjustVelocityToSlope(motion);
        stateMachine.CharacterController.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
        
        //stateMachine.BaseTransform.rotation = Quaternion.Euler(stateMachine.BaseTransform.rotation.eulerAngles.x, stateMachine.MainCameraTransform.rotation.eulerAngles.y, stateMachine.BaseTransform.rotation.eulerAngles.z);

        if (Time.time - stateMachine.lastFootstepTime > 0.5f && stateMachine.CharacterController.velocity != Vector3.zero)
        {
            stateMachine.lastFootstepTime = Time.time; // check if its not too messy
            stateMachine.PlayerAudio.PlayFootsteps();
        }
    }
    
    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void ReturnToLocomotion() 
    {
        if (stateMachine.Targeter.CurrentTarget != null)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
        else
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }
    
    private Vector3 AdjustVelocityToSlope(Vector3 velocity)
    {
        var ray = new Ray(stateMachine.BaseTransform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 5f))
        {
            var slopeRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            var adjustedVelocity = slopeRotation * velocity;
            
            if (adjustedVelocity.y < 0)
            {
                return adjustedVelocity;
            }
        }
        return velocity;
    }
    
    protected void FaceTarget()
    {
        if (stateMachine.Targeter.CurrentTarget == null ) {return;}

        Vector3 lookPos = stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position;
        lookPos.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }
}
