using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) {}
    public override void Enter()
    {
        //handle animation crossfade
        stateMachine.InputReader.JumpEvent += OnJump;
    }

    public override void Tick(float deltaTime)
    {
        //check for attack input
        Vector3 movement = CalculateMovement();
        
        if (stateMachine.InputReader.isSprinting)
        {
            Move(movement * stateMachine.FreeLookSprintingMovementSpeed, deltaTime);
        }
        else 
        {
            Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);
        }

        //if (stateMachine.InputReader.MovementValue == 0)
        //{
            // animation float 0
            // return;
        //}
        // stateMachine.Animator.SetFloat();   float to 1 

        if (stateMachine.InputReader.isCrouching)
        {
            stateMachine.SwitchState(new PlayerCrouchingState(stateMachine));
        }
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
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
    
    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
    }
}
