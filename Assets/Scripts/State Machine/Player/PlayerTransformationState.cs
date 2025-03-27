using UnityEngine;

public class PlayerTransformationState : PlayerBaseState
{
    private float forceCooldown = 2f;
    public PlayerTransformationState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.HanumanBool = !stateMachine.HanumanBool;

        if (stateMachine.HanumanBool)
        {
            stateMachine.VisnaBones.SetActive(false);
            stateMachine.VisnaBones.transform.SetParent(stateMachine.TransformationHolder.transform);
            stateMachine.VisnaMesh.SetActive(false);
            stateMachine.VisnaMesh.transform.SetParent(stateMachine.TransformationHolder.transform);
            stateMachine.HanumanMesh.SetActive(true);
            stateMachine.HanumanMesh.transform.SetParent(stateMachine.PlayerHolder.transform);
            stateMachine.HanumanBones.SetActive(true);
            stateMachine.HanumanBones.transform.SetParent(stateMachine.PlayerHolder.transform);
            
        }
        else
        {
            stateMachine.VisnaBones.SetActive(true);
            stateMachine.VisnaBones.transform.SetParent(stateMachine.PlayerHolder.transform);
            stateMachine.VisnaMesh.SetActive(true);
            stateMachine.VisnaMesh.transform.SetParent(stateMachine.PlayerHolder.transform);
            stateMachine.HanumanMesh.SetActive(false);
            stateMachine.HanumanMesh.transform.SetParent(stateMachine.TransformationHolder.transform);
            stateMachine.HanumanBones.SetActive(false);
            stateMachine.HanumanBones.transform.SetParent(stateMachine.TransformationHolder.transform);
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
