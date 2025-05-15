using System;
using DG.Tweening.Core.Easing;
using Unity.VisualScripting;
using UnityEngine;
using FMODUnity;

public class FirstReelPuzzle : MonoBehaviour
{
    public bool isAttached;
    private bool rotated1;
    private bool rotated2;
    private InputReader inputReader;
    private PlayerStateMachine stateMachine;
    private PlayerAudio playerAudio;
    private bool isPlayingAudio;

    [SerializeField] private StudioEventEmitter emitter;

    [SerializeField] private float rotation1 = 155f;
    [SerializeField] private float rotation2 = 55f;
    [SerializeField] private ReelDoor door;
    public bool isComplete;
    private bool moving;

    
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
        //inputReader = null;
        stateMachine.attachPoint = null;
        //playerAudio.StopStoneWheel();
    }

    private void Update()
    {
        if (!isAttached) return;
        if (isComplete)
        {
            if (stateMachine.AttachedBool)
            {
                DetachPlayer();
            }
        
            return;
        }
        
        switch (inputReader.MovementValue.y)
        {
            case > 0.01f:
                transform.Rotate(0, -1, 0);
                PlayAudio();
                moving = true;
                break;
            case < -0.01f:
                transform.Rotate(0, 1 , 0);
                PlayAudio();
                moving = true;
                break;
            case 0f:
                StopAudio();
                moving = false;
                break;
        }
        
        if (Math.Abs(transform.rotation.eulerAngles.y - rotation1) < 3f && !moving && !rotated1)
        {
            rotated1 = true;
            DetachPlayer();
            if (!rotated2)
            {
                emitter.Play();
                return;
            }
            isComplete = true;
            
        }
        else if (Math.Abs(transform.rotation.eulerAngles.y - rotation2) < 3f && !moving && !rotated2)
        {
            rotated2 = true;
            DetachPlayer();
            if (!rotated1)
            {
                emitter.Play();
                return;
            }

            isComplete = true;
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
