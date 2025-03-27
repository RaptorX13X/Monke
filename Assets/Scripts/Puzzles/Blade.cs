using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField] private int damage = 20;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            health.DealDamage(damage);
        }
    }
}
