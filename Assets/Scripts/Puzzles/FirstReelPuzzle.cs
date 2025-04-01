using System;
using UnityEngine;

public class FirstReelPuzzle : MonoBehaviour
{
    private bool isAttached;
    private bool rotated1;
    private bool rotated2;
    private InputReader inputReader;
    private PlayerStateMachine stateMachine;
    [SerializeField] private PuzzleAudio puzzleAudio;

    public bool isComplete;

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
        puzzleAudio.StopStoneWheel();
    }

    private void Update()
    {
        if (!isAttached) return;
        switch (inputReader.MovementValue.y)
        {
            case > 0.01f:
                transform.Rotate(0, -1, 0);
                puzzleAudio.PlayStoneWheel();
                break;
            case < -0.01f:
                transform.Rotate(0, 1, 0);
                puzzleAudio.PlayStoneWheel();
                break;
            case 0f:
                puzzleAudio.StopStoneWheel();
                break;
        }
        
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
                isComplete = true;
            }
        }
    }
}
