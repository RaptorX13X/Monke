using UnityEngine;

public class PlayerAttackingHState : PlayerBaseState
{
    private Attack attack;
    private float previousFrameTime;
    private bool alreadyAppliedForce;


    public PlayerAttackingHState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.AttacksH[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.Weapon.SetAttack(attack.Damage, attack.Knockback);
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();
        float normalizedTime = GetNormalizedTime(stateMachine.Animator);
        if (normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {
            if (normalizedTime >= attack.ForceTime)
            {
                TryApplyForce();
            }
            if (stateMachine.InputReader.IsAttackingL)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            if (stateMachine.Targeter.CurrentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }
        previousFrameTime = normalizedTime;
    }

    public override void Exit()
    {
        
    }
    
    private void TryApplyForce()
    {
        if (alreadyAppliedForce ) {return;}
        stateMachine.ForceReceiver.Translate(stateMachine.transform.forward * attack.Force);
        alreadyAppliedForce = true;
    }
    
    private void TryComboAttack(float normalizedTime)
    {
        if (attack.ComboStateIndex == -1)
        {
            return;
        }

        if (normalizedTime < attack.ComboAttackTime)
        {
            return;
        }
        
        stateMachine.SwitchState(new PlayerAttackingHState(stateMachine, attack.ComboStateIndex));
    }
}
