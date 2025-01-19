using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{
    private float duration = 1f;
    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //crossfade do animacji dmg
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        duration -= deltaTime;
        if (duration <= 0f)
        {
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {
        
    }
}
