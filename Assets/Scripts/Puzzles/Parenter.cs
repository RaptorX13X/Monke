using System;
using Unity.VisualScripting;
using UnityEngine;

public class Parenter : MonoBehaviour
{
    public Vector3 velocity;
    public float velocityMagn;
    private Vector3 currentFrame;
    private Vector3 lastFrame;

    private void Update()
    {
        velocity = (transform.position - lastFrame) / Time.deltaTime;
        velocityMagn = (transform.position - lastFrame).magnitude / Time.deltaTime;
        lastFrame = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStateMachine stateMachine))
        {
            stateMachine.parenter = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerStateMachine stateMachine))
        {
            stateMachine.parenter = null;
        }
    }
}
