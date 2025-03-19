using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    
    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.CharacterController.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }
    
    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void ReturnToLocomotion() 
    {
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    protected void FaceTarget()
    {
        if (stateMachine.Targeter.CurrentTarget == null ) {return;}

        Vector3 lookPos = stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position;
        lookPos.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    protected void HanumanChange()
    {
        stateMachine.HanumanBool = !stateMachine.HanumanBool;
        // change state to changing into hanuman or out of hanuman
        if (stateMachine.HanumanBool)
        {
            stateMachine.PlayerObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        else
        {
            stateMachine.PlayerObject.transform.localScale = Vector3.one;
        }
    }
}
