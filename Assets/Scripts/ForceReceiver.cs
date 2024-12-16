using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float drag;
    private float verticalVelocity;
    private Vector3 dampingVelocity;
    private Vector3 impact;
    
    public Vector3 Movement => impact + Vector3.up * verticalVelocity;

    private void Update()
    {
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);

        
        if (impact.sqrMagnitude < 0.2f * 0.2f) 
        { 
            impact = Vector3.zero;
        }
    }

    public void Translate(Vector3 translation)
    {
        impact += translation;
    }

    public void Jump(float jumpForce)
    {
        verticalVelocity += jumpForce;
    }
}
