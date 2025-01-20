using UnityEngine;

public class PlayerCrouchingState : PlayerBaseState
{
    private readonly int CrouchingBlendtreeHash = Animator.StringToHash("CrouchingBlendTree");
    private readonly int CrouchingSpeedHash = Animator.StringToHash("CrouchingSpeed");
    
    private const float AnimatorDampTime = 0.1f;
    public PlayerCrouchingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        // move character controller center and size
        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.Animator.CrossFadeInFixedTime(CrouchingBlendtreeHash, 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();
        Move(movement * stateMachine.CrouchingMovementSpeed, deltaTime);
        
        if (!stateMachine.InputReader.isCrouching)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
        
        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(CrouchingSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }
        stateMachine.Animator.SetFloat(CrouchingSpeedHash, 1, AnimatorDampTime, deltaTime);
        FaceMovementDirection(movement, deltaTime);
        
    }

    public override void Exit()
    {
        // reset character controller center and size
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
    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), deltaTime * stateMachine.RotationDamping);
    }
}
