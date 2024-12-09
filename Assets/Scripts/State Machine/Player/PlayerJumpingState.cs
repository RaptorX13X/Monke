using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private Vector3 momentum;
    public override void Enter()
    {
        stateMachine.ForceReceiver.Jump(stateMachine.JumpFoce);

        momentum = stateMachine.CharacterController.velocity;
        momentum.y = 0f;
        //play the jump animation
    }

    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);

        if (stateMachine.CharacterController.velocity.y <= 0)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }
        //FaceTarget if we have that at all
    }

    public override void Exit()
    {
        
    }
}
