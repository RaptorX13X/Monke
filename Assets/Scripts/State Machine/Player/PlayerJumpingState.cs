using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private Vector3 momentum;
    public override void Enter()
    {
        stateMachine.ForceReceiver.Jump(stateMachine.JumpFoce);

        momentum = stateMachine.CharacterController.velocity;
        momentum.y = 0f;
        
        stateMachine.PlayerAudio.PlayJump();
        //play the jump animation
    }

    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);
        
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
        //FaceTarget if we have that at all
    }

    public override void Exit()
    {
        
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
