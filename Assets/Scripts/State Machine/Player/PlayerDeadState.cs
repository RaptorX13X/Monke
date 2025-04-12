using System.Collections;
using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    private readonly int DyingHash = Animator.StringToHash("Death");
    private const float TransitionDuration = 0.1f;

    private float forceCooldown = 2f;
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        if (stateMachine.HanumanBool) stateMachine.HanumanAnimator.CrossFadeInFixedTime(DyingHash, TransitionDuration);
        else stateMachine.VisnaAnimator.CrossFadeInFixedTime(DyingHash, TransitionDuration);
        
        if (stateMachine.deathByFalling) stateMachine.PlayerAudio.PlayDeathByFalling();
        else if (!stateMachine.deathByFalling) stateMachine.PlayerAudio.PlayDeath();
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
        stateMachine.Health.Respawn();
        stateMachine.Respawn.Respawn();
        //stateMachine.Weapon.gameObject.SetActive(true);
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    public override void Exit()
    {
        stateMachine.deathByFalling = false;
    }
}
