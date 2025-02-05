using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float drag;
    [SerializeField] private float gravityMultiplier;
    private float verticalVelocity;
    private Vector3 dampingVelocity;
    private Vector3 impact;
    
    public Vector3 Movement => impact + Vector3.up * verticalVelocity;

    private Vector3 hitNormal;
    private bool isGrounded;
    private float slideFriction = 0.3f;

    private void Update()
    {
        // isGrounded = (Vector3.Angle (Vector3.up, hitNormal) <= controller.slopeLimit);
        //
        // if (!isGrounded)
        // {
        //     impact.x += (1f - hitNormal.y) * hitNormal.x * (1f - slideFriction);
        //     impact.z += (1f - hitNormal.y) * hitNormal.z * (1f - slideFriction);
        // }
        
        //Debug.Log(verticalVelocity);
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            verticalVelocity = -2f; // Physics.gravity.y * Time.deltaTime
        }
        else
        {
            verticalVelocity += -15 * Time.deltaTime; // * gravityMultiplier
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);

        
        if (impact.sqrMagnitude < 0.2f * 0.2f) 
        { 
            impact = Vector3.zero;
        }
        
        if (agent != null)
        {
            if (impact.sqrMagnitude < 0.2f * 0.2f)
            {
                impact = Vector3.zero;
                agent.enabled = true;
            }
        }
    }

    public void Translate(Vector3 translation)
    {
        impact += translation;
    }

    public void Jump(float jumpForce)
    {
        verticalVelocity += Mathf.Sqrt(jumpForce * -2f * -15); // bez -2f, physics i sqrt
    }

    public void Reset()
    {
        impact = Vector3.zero;
        verticalVelocity = 0f;
    }

    void OnControllerColliderHit (ControllerColliderHit hit) {
        hitNormal = hit.normal;
    }
}
