using UnityEngine;

public class PlayerPushingState : PlayerBaseState
{
    
    private readonly int PushingHash = Animator.StringToHash("Push");
    private const float CrossFadeDuration = 0.1f;
    public PlayerPushingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.CharacterController.enabled = false;
        if (!stateMachine.HanumanBool) stateMachine.VisnaAnimator.CrossFadeInFixedTime(PushingHash, CrossFadeDuration);
        else stateMachine.HanumanAnimator.CrossFadeInFixedTime(PushingHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.transform.position = new Vector3(stateMachine.attachPoint.transform.position.x, stateMachine.attachPoint.transform.position.y - stateMachine.CharacterController.height/2, stateMachine.attachPoint.transform.position.z);
        stateMachine.transform.rotation = Quaternion.LookRotation(stateMachine.attachPoint.transform.forward, Vector3.up);
    }

    public override void Exit()
    {
        stateMachine.CharacterController.enabled = true;
    }
}
