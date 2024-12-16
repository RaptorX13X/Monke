using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Action JumpEvent;
    public bool isSprinting;
    public bool isCrouching;
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

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isSprinting = true;
        }
        else if (context.canceled)
        {
            isSprinting = false;
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isCrouching = true;
        }
        else if (context.canceled)
        {
            isCrouching = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        
        JumpEvent?.Invoke();
    }
}
