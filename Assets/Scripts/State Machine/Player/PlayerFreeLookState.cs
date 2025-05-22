using Unity.Mathematics;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookBlendtreeHash = Animator.StringToHash("FreeLookBlendTree");
    private readonly int TorchBlendtreeHash = Animator.StringToHash("TorchBlendTree");
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int TorchSpeedHash = Animator.StringToHash("TorchSpeed");
    
    private const float AnimatorDampTime = 0.1f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) {}
    public override void Enter()
    {
        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.InputReader.HanumanEvent += OnHanuman;
        if (!stateMachine.hasTorch)
        {
            if (!stateMachine.HanumanBool) stateMachine.VisnaAnimator.CrossFadeInFixedTime(FreeLookBlendtreeHash, 0.1f);
            else stateMachine.HanumanAnimator.CrossFadeInFixedTime(FreeLookBlendtreeHash, 0.1f);
        }
        else
        {
            if (!stateMachine.HanumanBool) stateMachine.VisnaAnimator.CrossFadeInFixedTime(TorchBlendtreeHash, 0.1f);
            else stateMachine.HanumanAnimator.CrossFadeInFixedTime(TorchBlendtreeHash, 0.1f);
        }
        stateMachine.PlayerLeftFoot.canPlay = true;
        stateMachine.PlayerRightFoot.canPlay = true;
        stateMachine.HanumanLeftFoot.canPlay = true;
        stateMachine.HanumanRightFoot.canPlay = true;
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();
        if (stateMachine.parenter != null)
        { 
            if (stateMachine.parenter.TryGetComponent(out Parenter attach))
            {
                Move(attach.velocity, deltaTime);
            }
        }
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
            if (!stateMachine.HanumanBool)
            {
                stateMachine.VisnaAnimator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
                stateMachine.VisnaAnimator.SetFloat(TorchSpeedHash, 0, AnimatorDampTime, deltaTime);
            }
            else
            {
                stateMachine.HanumanAnimator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
                stateMachine.HanumanAnimator.SetFloat(TorchSpeedHash, 0, AnimatorDampTime, deltaTime);
            }
            return;
        }

        if (!stateMachine.HanumanBool)
        {
            stateMachine.VisnaAnimator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);
            stateMachine.VisnaAnimator.SetFloat(TorchSpeedHash, 1, AnimatorDampTime, deltaTime);
        }
        else
        {
            stateMachine.HanumanAnimator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);
            stateMachine.HanumanAnimator.SetFloat(TorchSpeedHash, 1, AnimatorDampTime, deltaTime);
        }
        FaceMovementDirection(movement, deltaTime);
        if (stateMachine.CharacterController.velocity.y <= -5f)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.HanumanEvent -= OnHanuman;
        stateMachine.PlayerLeftFoot.canPlay = false;
        stateMachine.PlayerRightFoot.canPlay = false;
        stateMachine.HanumanLeftFoot.canPlay = false;
        stateMachine.HanumanRightFoot.canPlay = false;
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
        if (stateMachine.UnlockedHanuman)
        {
            stateMachine.SwitchState(new PlayerTransformationState(stateMachine));
        }
    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), deltaTime * stateMachine.RotationDamping);
    }
}