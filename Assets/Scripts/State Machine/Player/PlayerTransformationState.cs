using UnityEngine;

public class PlayerTransformationState : PlayerBaseState
{
    private float forceCooldown = 1f;
    public PlayerTransformationState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.HanumanBool = !stateMachine.HanumanBool;

        if (stateMachine.HanumanBool)
        {
            stateMachine.VisnaForm.SetActive(false);
            stateMachine.HanumanForm.SetActive(true);
            stateMachine.CharacterController.center = new Vector3(0, 1.3f, 0);
        }
        else
        {
            stateMachine.VisnaForm.SetActive(true);
            stateMachine.HanumanForm.SetActive(false);
            stateMachine.CharacterController.center = new Vector3(0, 1.55f, 0);
        }
        stateMachine.transformParticles.Play();
    }

    public override void Tick(float deltaTime)
    {
        forceCooldown -= deltaTime;
        if (forceCooldown >= 0.1f) return;
        
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    public override void Exit()
    {
        
    }
}
