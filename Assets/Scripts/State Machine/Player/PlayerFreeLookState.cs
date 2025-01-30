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
        stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendtreeHash, 0.1f);
        stateMachine.PlayerLeftFoot.canPlay = true;
        stateMachine.PlayerRightFoot.canPlay = true;
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttackingL)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
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

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }
        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);

        if (stateMachine.InputReader.isCrouching)
        {
            stateMachine.SwitchState(new PlayerCrouchingState(stateMachine));
        }
        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.TargetEvent -= OnTarget;
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
    
    private void OnTarget()
    {
        if(!stateMachine.Targeter.SelectTarget()) {return;}
        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }
    
    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), deltaTime * stateMachine.RotationDamping);
    }
}