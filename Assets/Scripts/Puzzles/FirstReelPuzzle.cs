using System;
using UnityEngine;

public class FirstReelPuzzle : MonoBehaviour
{
    private bool isAttached;
    [SerializeField] private Transform target1;
    [SerializeField] private Transform target2;
    private bool rotated1;
    private bool rotated2;
    public InputReader inputReader;
    public PlayerStateMachine stateMachine;

    public void AttachPlayer(PlayerStateMachine _stateMachine, InputReader reader, Transform attacher)
    {
        if (stateMachine == null)
        {
            stateMachine = _stateMachine;
        }
        stateMachine.attachPoint = attacher;
        inputReader = reader;
        stateMachine.AttachedBool = true;
        isAttached = true;
        stateMachine.SwitchState(new PlayerPushingState(stateMachine));
    }

    public void DetachPlayer()
    {
        stateMachine.AttachedBool = false;
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        isAttached = false;
        inputReader = null;
        stateMachine.attachPoint = null;
    }

    private void Update()
    {
        if (!isAttached) return;
        switch (inputReader.MovementValue.y)
        {
            case > 0.01f:
                transform.Rotate(0, -1, 0);
                break;
            case < -0.01f:
                transform.Rotate(0, 1, 0);
                break;
        }
        Vector3 targetDir1 = target1.position - transform.position;
        Vector3 forward1 = transform.forward;
        float angle1 = Vector3.SignedAngle(targetDir1, forward1, Vector3.up);
        
        Vector3 targetDir2 = target2.position - transform.position;
        Vector3 forward2 = transform.forward;
        float angle2 = Vector3.SignedAngle(targetDir2, forward2, Vector3.up);
        Debug.Log(transform.rotation.eulerAngles.y);
        if (Math.Abs(transform.rotation.eulerAngles.y - 90f) < 0.1f && !rotated1)
        {
            rotated1 = true;
            DetachPlayer();
        }

        if (rotated1)
        {
            if (Math.Abs(transform.rotation.eulerAngles.y - 270f) < 0.1f)
            {
                rotated2 = true;
                DetachPlayer();
            }
        }
    }
}
