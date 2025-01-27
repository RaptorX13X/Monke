using UnityEngine;
using System;
using System.Collections;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int healthRegen = 5;
    [SerializeField] private bool isPlayer;
    [SerializeField] private TextMeshProUGUI healthText;
    private int currentHealth;
    private bool isInvulnerable;
    public event Action OnTakeDamage;
    public event Action OnDie;
    public bool IsDead => currentHealth == 0;

    private void Start()
    {
        currentHealth = maxHealth;
        if (isPlayer)
        {
            healthText.text = currentHealth.ToString();
        }
    }

    public void SetInvulnerable(bool isInvulnerable)
    {
        this.isInvulnerable = isInvulnerable;
    }

    public void DealDamage(int damage)
    {
        if (currentHealth == 0) return;
        if (isInvulnerable) return;
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        OnTakeDamage?.Invoke();
        Debug.Log("Dealing damage");
        if (currentHealth == 0)
        {
            OnDie?.Invoke();
        }
        Debug.Log(currentHealth);
        if (isPlayer)
        {
            healthText.text = currentHealth.ToString();
        }
    }

    public void Respawn()
    {
        currentHealth = maxHealth;
    }

    private IEnumerator HealthRegen()
    {
        while (currentHealth != maxHealth)
        {
            currentHealth += healthRegen;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
