using UnityEngine;
using UnityEngine.InputSystem;
public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 MovementValue {get; private set;}
    
    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        //we will figure this out later
    }
}
