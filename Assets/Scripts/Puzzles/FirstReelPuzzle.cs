using System;
using DG.Tweening.Core.Easing;
using Unity.VisualScripting;
using UnityEngine;

public class FirstReelPuzzle : MonoBehaviour
{
    private bool isAttached;
    private bool rotated1;
    private bool rotated2;
    private InputReader inputReader;
    private PlayerStateMachine stateMachine;
    private PlayerAudio playerAudio;
    private bool isPlayingAudio;

    public bool isComplete;

    public void AttachPlayer(PlayerStateMachine _stateMachine, InputReader reader, Transform attacher, PlayerAudio audio)
    {
        if (stateMachine == null)
        {
            stateMachine = _stateMachine;
        }
        stateMachine.attachPoint = attacher;
        inputReader = reader;
        playerAudio = audio;
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
        playerAudio.StopStoneWheel();
    }

    private void Update()
    {
        if (!isAttached) return;
        switch (inputReader.MovementValue.y)
        {
            case > 0.01f:
                transform.Rotate(0, -1, 0);
                PlayAudio();
                break;
            case < -0.01f:
                transform.Rotate(0, 1, 0);
                PlayAudio();
                break;
            case 0f:
                StopAudio();
                break;
        }
        
        if (Math.Abs(transform.rotation.eulerAngles.y - 155f) < 0.1f && !rotated1)
        {
            rotated1 = true;
            DetachPlayer();
        }

        if (rotated1)
        {
            if (Math.Abs(transform.rotation.eulerAngles.y - 55f) < 0.1f)
            {
                rotated2 = true;
                DetachPlayer();
                isComplete = true;
            }
        }
    }

    private void PlayAudio()
    {
        if (isPlayingAudio) return;
        playerAudio.PlayStoneWheel();
        isPlayingAudio = true;
    }

    private void StopAudio()
    {
        if (!isPlayingAudio) return;
        playerAudio.StopStoneWheel();
        isPlayingAudio = false;
    }
}
