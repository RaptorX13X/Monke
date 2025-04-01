using UnityEngine;

public class PlayerPushingState : PlayerBaseState
{
    public PlayerPushingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //pushing animacja crossfade
        stateMachine.CharacterController.enabled = false;
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.transform.position = new Vector3(stateMachine.attachPoint.transform.position.x, stateMachine.attachPoint.transform.position.y - stateMachine.CharacterController.height/2, stateMachine.attachPoint.transform.position.z);
        
    }

    public override void Exit()
    {
        stateMachine.CharacterController.enabled = true;
    }
}
