using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float FreeLookSprintingMovementSpeed { get; private set; }
    [field: SerializeField] public float CrouchingMovementSpeed { get; private set; }
    [field: SerializeField] public Transform BaseTransform { get; private set; }
    [field: SerializeField] public GameObject CrouchTest { get; private set; }
    [field: SerializeField] public GameObject WalkTest { get; private set; }
    [field: SerializeField] public float JumpFoce { get; private set; }

    public Transform MainCameraTransform { get; private set; }
    private void Start()
    {
        MainCameraTransform = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
    }
}
