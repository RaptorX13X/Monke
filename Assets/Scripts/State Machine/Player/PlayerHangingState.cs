using UnityEngine;

public class PlayerHangingState : PlayerBaseState
{
    private Vector3 ledgeForward;
    private readonly int HangingHash = Animator.StringToHash("Hanging");
    private const float CrossFadeDuration = 0.1f;

    private float forceCooldown = 0.5f;
    public PlayerHangingState(PlayerStateMachine stateMachine, Vector3 ledgeForward) : base(stateMachine)
    {
        this.ledgeForward = ledgeForward;
    }

    public override void Enter()
    {
        stateMachine.transform.rotation = Quaternion.LookRotation(ledgeForward, Vector3.up);
        if (!stateMachine.HanumanBool) stateMachine.VisnaAnimator.CrossFadeInFixedTime(HangingHash, CrossFadeDuration);
        else stateMachine.HanumanAnimator.CrossFadeInFixedTime(HangingHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        forceCooldown -= deltaTime;
        if (forceCooldown >= 0.1f) return;
        if (stateMachine.InputReader.MovementValue.y > 0f)
        {
            stateMachine.SwitchState(new PlayerPullUpState(stateMachine));
        }
        if (stateMachine.InputReader.MovementValue.y < 0f)
        {
            stateMachine.CharacterController.Move(Vector3.zero);
            stateMachine.ForceReceiver.Reset();
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        }
    }

    public override void Exit()
    {
        
    }
}
