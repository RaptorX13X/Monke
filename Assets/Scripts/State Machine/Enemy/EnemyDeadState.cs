using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    private readonly int DyingHash = Animator.StringToHash("Dying");
    private const float TransitionDuration = 0.1f;
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(DyingHash, TransitionDuration);
        stateMachine.Weapon.gameObject.SetActive(false);
        GameObject.Destroy(stateMachine.Target);
    }

    public override void Tick(float deltaTime)
    {
       
    }

    public override void Exit()
    {
       
    }
}
