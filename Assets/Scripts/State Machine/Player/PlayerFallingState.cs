using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    private readonly int FallHash = Animator.StringToHash("Fall");
    private const float TransitionDuration = 0.1f;
    public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private Vector3 momentum;

    public override void Enter()
    {
        momentum = stateMachine.CharacterController.velocity;
        momentum.y = 0f;

        stateMachine.LedgeDetector.OnLedgeDetect += HandleLedgeDetect;
        stateMachine.Animator.CrossFadeInFixedTime(FallHash, TransitionDuration);
        //play falling animation

        // stateMachine.InputReader.JumpEvent += OnJump;
    }

    public override void Tick(float deltaTime)
    {
        Move(momentum,deltaTime);
        
        Debug.Log(stateMachine.CharacterController.collisionFlags);

        if (stateMachine.CharacterController.isGrounded)
        {
            ReturnToLocomotion();
            return;
        }

        // if ((stateMachine.CharacterController.collisionFlags & CollisionFlags.Sides) != 0)
        // {
        //     stateMachine.InputReader.JumpEvent += OnJump;
        // }
        // else
        // {
        //     stateMachine.InputReader.JumpEvent -= OnJump;
        // }
        /*RaycastHit hit;

        Vector3 p1 = stateMachine.BaseTransform.position + stateMachine.CharacterController.center;
        float distance = 0;
        if (Physics.SphereCast(p1, stateMachine.CharacterController.height / 2, stateMachine.BaseTransform.forward,
                out hit, 10))
        {
            stateMachine.InputReader.JumpEvent += OnJump;
            Debug.Log(hit);
        }
        else
        {
            stateMachine.InputReader.JumpEvent -= OnJump;
        }*/
        
        
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
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.PlayerAudio.PlayLanding();
        
        stateMachine.LedgeDetector.OnLedgeDetect -= HandleLedgeDetect;
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

    private void HandleLedgeDetect(Vector3 ledgeForward)
    {
        stateMachine.SwitchState(new PlayerHangingState(stateMachine, ledgeForward));
    }
}
