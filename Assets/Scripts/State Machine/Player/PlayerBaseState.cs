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
        stateMachine.CharacterController.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
        if (stateMachine.InputReader.isSprinting)
        {
            
        }
        stateMachine.BaseTransform.rotation = Quaternion.Euler(stateMachine.BaseTransform.rotation.eulerAngles.x, stateMachine.MainCameraTransform.rotation.eulerAngles.y, stateMachine.BaseTransform.rotation.eulerAngles.z);
    }

    protected void ReturnToLocomotion()
    {
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
}
