using UnityEngine;

public abstract class State
{
    public abstract void Enter(); //is called once when entity enters the state 
    public abstract void Tick(float deltaTime); //is called once per tick when entity is in the state
    public abstract void Exit(); //is called once when entity leaves the state
    
    protected float GetNormalizedTime(Animator animator)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
}
