using System.Collections;
using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    private readonly int DyingHash = Animator.StringToHash("Death");
    private const float TransitionDuration = 0.1f;
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Weapon.gameObject.SetActive(false);
        stateMachine.Animator.CrossFadeInFixedTime(DyingHash, TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) return;
        stateMachine.Health.Respawn();
        stateMachine.Respawn.Respawn();
        stateMachine.Weapon.gameObject.SetActive(true);
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    public override void Exit()
    {
        
    }
}
