using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Transform MainCameraTransform { get; private set; }
    private void Start()
    {
        MainCameraTransform = Camera.main.transform;
    }
}
