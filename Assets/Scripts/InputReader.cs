using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Action JumpEvent;
    public Action TargetEvent;
    public Action CancelEvent;
    public Action PauseEvent;
    public bool isBlocking;
    public Vector2 MovementValue {get; private set;}
    
    public bool IsAttackingL { get; private set; }

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

    public void OnAttackL(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsAttackingL = true;
        }
        else if (context.canceled)
        {
            IsAttackingL = false;
        }
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if(!context.performed) {return;}

        TargetEvent?.Invoke();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if(!context.performed) {return;}

        CancelEvent?.Invoke();
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isBlocking = true;
        }
        else if (context.canceled)
        {
            isBlocking = false;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        //
    }

    public void OnHanuman(InputAction.CallbackContext context)
    {
        //
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
