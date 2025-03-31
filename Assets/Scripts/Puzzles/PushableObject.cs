using UnityEngine;

public class PushableObject : MonoBehaviour
{
    [SerializeField] private Collider trigger;

    public void DisableTrigger()
    {
        trigger.enabled = false;
    }
}
