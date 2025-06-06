using UnityEngine;

public class PlayerTestPush : MonoBehaviour
{
    public float pushPower = 2.0f;
    [SerializeField] private PlayerStateMachine stateMachine;
    [SerializeField] private PlayerAudio playerAudio;
    private bool alreadyPlaying;
    
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.TryGetComponent(out BigPush bigPush))
        {
            if (!stateMachine.HanumanBool) return;
        }
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0f, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.linearVelocity = pushDir * pushPower;

        //if (!alreadyPlaying)
        //{
        //    playerAudio.PlayPushingObject();
        //    alreadyPlaying = true;
        //}
    }
}
