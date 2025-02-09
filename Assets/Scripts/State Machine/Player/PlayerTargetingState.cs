using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetingBlendtreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward");
    private readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.InputReader.CancelEvent += OnCancel;
        stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendtreeHash, 0.1f);
        stateMachine.Targeter.CurrentTarget.TurnMeOn();
        stateMachine.InputReader.JumpEvent += OnJump;
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttackingL)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }

        if (stateMachine.InputReader.isBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
            return;
        }
        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        Vector3 movement = CalculateMovement();
        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);
        UpdateAnimator(deltaTime);
        FaceTarget();
    }

    public override void Exit()
    {
        stateMachine.InputReader.CancelEvent -= OnCancel;
        stateMachine.InputReader.JumpEvent -= OnJump;
    }

    private void OnCancel()
    {
        stateMachine.Targeter.CurrentTarget.TurnMeOff();
        stateMachine.Targeter.Cancel();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
    
    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
    }

    private Vector3 CalculateMovement()
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;

        return movement;
    }

    private void UpdateAnimator(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue.x == 0)
        {
            stateMachine.Animator.SetFloat(TargetingForwardHash, 0, 0.1f, deltaTime);
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.x > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetingForwardHash, value, 0.1f, deltaTime);
        }
        
        if (stateMachine.InputReader.MovementValue.y == 0)
        {
            stateMachine.Animator.SetFloat(TargetingRightHash, 0, 0.1f, deltaTime);
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.y > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetingRightHash, value, 0.1f, deltaTime);
        }
    }
}
