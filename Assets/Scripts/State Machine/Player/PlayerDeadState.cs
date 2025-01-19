using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        // ragdoll?
        // disable weapon
        // start coroutine to respawn
    }

    public override void Tick(float deltaTime)
    {
        
    }

    public override void Exit()
    {
        
    }
    
    //coroutine to respawn
    //wait for 3 seconds and then respawn
    //and enable the weapon i guess
}
