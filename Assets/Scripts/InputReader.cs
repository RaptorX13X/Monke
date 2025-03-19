using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Action JumpEvent;
    public Action PauseEvent;
    public Action HanumanEvent;
    public Vector2 MovementValue {get; private set;}

    private Controls controls;

    private void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    private void OnDestroy()
    {
        controls.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        //
    }

    public void OnHanuman(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        HanumanEvent?.Invoke();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        //
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        
        JumpEvent?.Invoke();
    }
}
