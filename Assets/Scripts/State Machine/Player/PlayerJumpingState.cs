using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    private readonly int JumpHash = Animator.StringToHash("Jump");
    private const float TransitionDuration = 0.1f;
    public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private Vector3 momentum;
    public override void Enter()
    {
        if (stateMachine.HanumanBool)
        {
            stateMachine.ForceReceiver.Jump(stateMachine.HanumanJumpForce);
        }
        else
        {
            stateMachine.ForceReceiver.Jump(stateMachine.JumpForce);
        }
        

        momentum = stateMachine.CharacterController.velocity;
        momentum.y = 0f;
        if (!stateMachine.HanumanBool) stateMachine.VisnaAnimator.CrossFadeInFixedTime(JumpHash, TransitionDuration);
        else stateMachine.HanumanAnimator.CrossFadeInFixedTime(JumpHash, TransitionDuration);
        stateMachine.PlayerAudio.PlayJump();
        stateMachine.LedgeDetector.OnLedgeDetect += HandleLedgeDetect;
    }

    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);
        
        Vector3 movement = CalculateMovement();
        
        if (stateMachine.HanumanBool)
        {
            Move(movement * stateMachine.HanumanMovementSpeed, deltaTime);
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
        FaceTarget();
    }

    public override void Exit()
    {
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
