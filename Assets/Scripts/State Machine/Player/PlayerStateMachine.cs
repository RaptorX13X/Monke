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
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }
    [field: SerializeField] public Attack[] AttacksH { get; private set; }
    [field: SerializeField] public TestRespawnHandler Respawn { get; private set; }
    [field: SerializeField] public PlayerFoot PlayerLeftFoot { get; private set; }
    [field: SerializeField] public PlayerFoot PlayerRightFoot { get; private set; }
    

    [field: Header("Movement Stats")]
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float FreeLookSprintingMovementSpeed { get; private set; }
    [field: SerializeField] public float CrouchingMovementSpeed { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField] public Vector3 ClimbOffset { get; private set; }
    

    [field: Header("Testing")]
    [field: SerializeField] public Transform BaseTransform { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }

    public bool OutOfCombat()
    {
        if (Targeter.targets.Count == 0)
        {
            return true;
        }
        return false;
    }

    private float timer;

    public Transform MainCameraTransform { get; private set; }
    private void Start()
    {
        MainCameraTransform = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
    }

    private void FixedUpdate() // zmienic na event callniety raz jak sie jest out of combat?
    {
        if (OutOfCombat())
        {
            Debug.Log("Out Of Combat");
            timer += Time.fixedDeltaTime;
            Debug.Log(timer);
            if (timer >= 5f)
            {
                Debug.Log("Starting health regeneration");
                timer = 0;
                Health.canRegen = true;
            }
        }
        else
        {
            Health.canRegen = false;
        }
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
        SwitchState(new PlayerImpactState(this));
    }

    private void HandleDie()
    {
        SwitchState(new PlayerDeadState(this));
    }
}
