using UnityEngine;

public class PlayerPullUpState : PlayerBaseState
{
    private readonly int PullUpHash = Animator.StringToHash("Climbing");
    private const float CrossFadeDuration = 0.1f;

    private float forceCooldown = 1f;
    public PlayerPullUpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        if (!stateMachine.HanumanBool) stateMachine.VisnaAnimator.CrossFadeInFixedTime(PullUpHash, CrossFadeDuration);
        else stateMachine.HanumanAnimator.CrossFadeInFixedTime(PullUpHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        forceCooldown -= deltaTime;
        if (forceCooldown >= 0.1f) return;
        if (!stateMachine.HanumanBool)
        {
            if (stateMachine.VisnaAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) return;
        }
        else
        {
            if (stateMachine.HanumanAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) return;
        }
        stateMachine.CharacterController.enabled = false;
        stateMachine.transform.Translate(stateMachine.ClimbOffset, Space.Self);
        stateMachine.CharacterController.enabled = true;
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    public override void Exit()
    {
        stateMachine.CharacterController.Move(Vector3.zero);
        stateMachine.ForceReceiver.Reset();
    }
}
