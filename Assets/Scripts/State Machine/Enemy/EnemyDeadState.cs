using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    private readonly int DyingHash = Animator.StringToHash("Dying");
    private const float TransitionDuration = 0.1f;
    private float waitTime = 4;
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(DyingHash, TransitionDuration);
        stateMachine.Weapon.gameObject.SetActive(false);
        GameObject.Destroy(stateMachine.Target);
        stateMachine.Target.TurnMeOff();
    }

    public override void Tick(float deltaTime)
    {
        waitTime -= deltaTime;
        if (waitTime >= 0.1f) return;
        stateMachine.DestroyObject();
    }

    public override void Exit()
    {
       
    }
}
