using UnityEngine;

public class PlayerPullUpState : PlayerBaseState
{
    public PlayerPullUpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {

    }

    public override void Tick(float deltaTime)
    {
        //when animation finishes
        //if (stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) return;
        stateMachine.CharacterController.enabled = false;
        stateMachine.transform.Translate(stateMachine.ClimbOffset, Space.Self);
        stateMachine.CharacterController.enabled = true;
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    public override void Exit()
    {
        stateMachine.CharacterController.Move(Vector3.zero);
        stateMachine.ForceReceiver.Reset();
    }
}
