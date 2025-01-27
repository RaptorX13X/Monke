using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State currentState; //lets the entity have only one state at a time

    //handles the tick event of the current state if any is assigned
    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }

    //handles switching of states done by any entity in game, calls exit on the current state, changes into the new one and calls enter on it
    public void SwitchState(State newState)
    {
        currentState?.Exit(); 
        currentState = newState;
        currentState?.Enter();
        Debug.Log(newState);
    }
}
