using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerStateMachine : StateMachine
{
    [field: Header("Components")]
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public PlayerAudio PlayerAudio { get; private set; }
    [field: SerializeField] public LedgeDetector LedgeDetector { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public WeaponDamage Weapon { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }
    

    [field: Header("Movement Stats")]
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float FreeLookSprintingMovementSpeed { get; private set; }
    [field: SerializeField] public float CrouchingMovementSpeed { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField] public Vector3 ClimbOffset { get; private set; }

    [field: Header("Testing")]
    [field: SerializeField] public Transform BaseTransform { get; private set; }
    [field: SerializeField] public GameObject CrouchTest { get; private set; }
    [field: SerializeField] public GameObject WalkTest { get; private set; }

    public float lastFootstepTime = 0f;

    public Transform MainCameraTransform { get; private set; }
    private void Start()
    {
        MainCameraTransform = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
    }

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDie;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDie;
    }

    private void HandleTakeDamage()
    {
        //SwitchState(new PlayerImpactState(this));
    }

    private void HandleDie()
    {
        //SwitchState(new PlayerDeadState(this));
    }
}
