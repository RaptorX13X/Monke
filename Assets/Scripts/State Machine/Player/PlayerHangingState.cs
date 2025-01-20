using UnityEngine;

public class PlayerHangingState : PlayerBaseState
{
    private Vector3 ledgeForward;
    private readonly int HangingHash = Animator.StringToHash("Hanging");
    private const float CrossFadeDuration = 0.1f;
    public PlayerHangingState(PlayerStateMachine stateMachine, Vector3 ledgeForward) : base(stateMachine)
    {
        this.ledgeForward = ledgeForward;
    }

    public override void Enter()
    {
        stateMachine.transform.rotation = Quaternion.LookRotation(ledgeForward, Vector3.up);
        stateMachine.Animator.CrossFadeInFixedTime(HangingHash, CrossFadeDuration);
        //stateMachine.InputReader.JumpEvent += OnJump
    }

    public override void Tick(float deltaTime)
    {
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
        //stateMachine.InputReader.JumpEvent -= OnJump;
    }
    
    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
    }
}
