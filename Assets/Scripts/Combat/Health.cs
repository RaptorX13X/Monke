using UnityEngine;
using System;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int healthRegen = 5;
    [SerializeField] private bool isPlayer;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image healthBar;
    
    private int currentHealth;
    private bool isInvulnerable;
    public event Action OnTakeDamage;
    public event Action OnDie;
    public bool IsDead => currentHealth == 0;
    public bool canRegen;
    [SerializeField] private float regenRate = 5;
    private float regen;

    [SerializeField] private PlayerAudio playerAudio;

    private void Start()
    {
        currentHealth = maxHealth;
        if (isPlayer)
        {
            healthText.text = currentHealth.ToString();
            healthBar.fillAmount = currentHealth * 0.01f;
        }

        regen = regenRate;
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
            if (playerAudio != null) playerAudio.PlayDeath();
            OnDie?.Invoke();
        }
        Debug.Log(currentHealth);
        if (isPlayer)
        {
            healthText.text = currentHealth.ToString();
            healthBar.fillAmount = currentHealth * 0.01f;
        }
    }

    public void Respawn()
    {
        currentHealth = maxHealth;
        healthText.text = currentHealth.ToString();
        healthBar.fillAmount = currentHealth * 0.01f;
    }

    private void Update()
    {
        if (canRegen)
        {
            if (currentHealth < maxHealth)
            {
                regen -= Time.deltaTime;
                if (regen <= 0)
                {
                    currentHealth += healthRegen;
                    Mathf.Clamp(currentHealth, 0, maxHealth);
                    healthText.text = currentHealth.ToString();
                    healthBar.fillAmount = currentHealth * 0.01f;
                    regen = regenRate;
                }
            }
        }
    }
}
