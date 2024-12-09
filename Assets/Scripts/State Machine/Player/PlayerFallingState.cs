using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private Vector3 momentum;

    public override void Enter()
    {
        momentum = stateMachine.CharacterController.velocity;
        momentum.y = 0f;
        
        //play falling animation
    }

    public override void Tick(float deltaTime)
    {
        Move(momentum,deltaTime);

        if (stateMachine.CharacterController.isGrounded)
        {
            ReturnToLocomotion();
        }
        
        //facetarget idk
    }

    public override void Exit()
    {
        
    }
}
