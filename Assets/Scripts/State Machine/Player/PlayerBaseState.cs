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
        motion = AdjustVelocityToSlope(motion);
        stateMachine.CharacterController.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
        
        stateMachine.BaseTransform.rotation = Quaternion.Euler(stateMachine.BaseTransform.rotation.eulerAngles.x, stateMachine.MainCameraTransform.rotation.eulerAngles.y, stateMachine.BaseTransform.rotation.eulerAngles.z);

        if (Time.deltaTime - stateMachine.lastFootstepTime > 0.5f)
        {
            stateMachine.lastFootstepTime = Time.deltaTime; // check if its not too messy
            stateMachine.PlayerAudio.PlayFootsteps();
        }
    }

    protected void ReturnToLocomotion() 
    {
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
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
}
