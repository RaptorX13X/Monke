using UnityEngine;

public abstract class State
{
    public abstract void Enter(); //is called once when entity enters the state 
    public abstract void Tick(float deltaTime); //is called once per tick when entity is in the state
    public abstract void Exit(); //is called once when entity leaves the state
}
