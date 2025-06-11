using System;
using System.Collections;
using UnityEngine;

public class PushableObject : MonoBehaviour
{
    private Vector3 respawnPoint;
    public bool isSet;
    [SerializeField] private BoxAttacher[] attachers;
    [SerializeField] private Rigidbody body;
    [SerializeField] private float speed;
    
    private InputReader inputReader;
    private PlayerStateMachine stateMachine;
    private PlayerAudio playerAudio;
    private bool isPlayingAudio;
    public bool isAttached;

    private bool moving;
    private void Start()
    {
        respawnPoint = transform.position;
    }

    public void DisableTrigger()
    {
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        isSet = true;
        DetachPlayer();
        yield return new WaitForSeconds(1f);
        foreach (var attacher in attachers)
        {
            attacher.gameObject.SetActive(false);
        }
    }

    public void Respawn()
    {
        transform.position = respawnPoint;
    }
    
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
        if (isSet)
        {
            if (stateMachine.AttachedBool)
            {
                DetachPlayer();
            }
        
            return;
        }
        
        switch (inputReader.MovementValue.x)
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
        
        switch (inputReader.MovementValue.y)
        {
            case > 0.01f:
                //transform.Translate(inputReader.MovementValue * Time.deltaTime, Space.Self);
                //body.linearVelocity = new Vector3(inputReader.MovementValue.x, body.linearVelocity.y, inputReader.MovementValue.y);
                body.MovePosition(body.position + stateMachine.transform.forward * Time.deltaTime * speed);
                PlayAudio();
                moving = true;
                break;
            case < -0.01f:
                //transform.Translate(inputReader.MovementValue * Time.deltaTime, Space.Self);
                //body.linearVelocity = new Vector3(inputReader.MovementValue.x, body.linearVelocity.y, inputReader.MovementValue.y);
                body.MovePosition(body.position +  stateMachine.transform.forward * Time.deltaTime * speed);
                PlayAudio();
                moving = true;
                break;
            case 0f:
                StopAudio();
                moving = false;
                break;
        }
    }

    private void PlayAudio()
    {
        if (isPlayingAudio) return;
        playerAudio.PlayPushingObject();
        isPlayingAudio = true;
    }

    private void StopAudio()
    {
        if (!isPlayingAudio) return;
        playerAudio.StopPushingObject();
        isPlayingAudio = false;
    }
}
