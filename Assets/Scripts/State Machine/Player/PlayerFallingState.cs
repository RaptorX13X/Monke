using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    private readonly int FallHash = Animator.StringToHash("Fall");
    private const float TransitionDuration = 0.1f;

    private readonly int LandHash = Animator.StringToHash("Land");
    public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private Vector3 momentum;

    public override void Enter()
    {
        momentum = stateMachine.CharacterController.velocity;
        momentum.y = 0f;

        stateMachine.LedgeDetector.OnLedgeDetect += HandleLedgeDetect;
        stateMachine.VisnaAnimator.CrossFadeInFixedTime(FallHash, TransitionDuration);
        stateMachine.HanumanAnimator.CrossFadeInFixedTime(FallHash, TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(momentum,deltaTime);
        Debug.Log(stateMachine.CharacterController.isGrounded);
        if (stateMachine.CharacterController.isGrounded)
        {
            ReturnToLocomotion();
            return;
        }

        Vector3 movement = CalculateMovement();
        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.VisnaAnimator.CrossFadeInFixedTime(LandHash, TransitionDuration);
        stateMachine.HanumanAnimator.CrossFadeInFixedTime(LandHash, TransitionDuration);
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

    private void HandleLedgeDetect(Vector3 ledgeForward)
    {
        stateMachine.SwitchState(new PlayerHangingState(stateMachine, ledgeForward));
    }
}
