using Unity.Mathematics;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookBlendtreeHash = Animator.StringToHash("FreeLookBlendTree");
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    
    private const float AnimatorDampTime = 0.1f;
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) {}
    public override void Enter()
    {
        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.InputReader.HanumanEvent += OnHanuman;
        stateMachine.VisnaAnimator.CrossFadeInFixedTime(FreeLookBlendtreeHash, 0.1f);
        stateMachine.HanumanAnimator.CrossFadeInFixedTime(FreeLookBlendtreeHash, 0.1f);
        stateMachine.PlayerLeftFoot.canPlay = true;
        stateMachine.PlayerRightFoot.canPlay = true;
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();
        if (stateMachine.HanumanBool)
        {
            Move(movement * stateMachine.HanumanMovementSpeed, deltaTime);
        }
        else
        {
            Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);
        }
        
        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.VisnaAnimator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            stateMachine.HanumanAnimator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }
        stateMachine.VisnaAnimator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);
        stateMachine.HanumanAnimator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);
        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.HanumanEvent -= OnHanuman;
        stateMachine.PlayerLeftFoot.canPlay = false;
        stateMachine.PlayerRightFoot.canPlay = false;
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

    private void OnHanuman()
    {
        stateMachine.SwitchState(new PlayerTransformationState(stateMachine));
    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), deltaTime * stateMachine.RotationDamping);
    }
}