using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private Vector3 momentum;

    public override void Enter()
    {
        momentum = stateMachine.CharacterController.velocity;
        momentum.y = 0f;
        
        //play falling animation
    }

    public override void Tick(float deltaTime)
    {
        Move(momentum,deltaTime);

        if (stateMachine.CharacterController.isGrounded)
        {
            ReturnToLocomotion();
            return;
        }
        
        Vector3 movement = CalculateMovement();
        
        if (stateMachine.InputReader.isSprinting)
        {
            Move(movement * stateMachine.FreeLookSprintingMovementSpeed, deltaTime);
        }
        else 
        {
            Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);
        }

        if (stateMachine.CharacterController.velocity.y <= 0)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }
        
        //facetarget idk
    }

    public override void Exit()
    {
        stateMachine.PlayerAudio.PlayLanding();
    }
    
    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();
        return forward * stateMachine.InputReader.MovementValue.y  + right * stateMachine.InputReader.MovementValue.x;
    }
}
