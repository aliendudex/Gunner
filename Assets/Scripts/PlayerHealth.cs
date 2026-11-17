using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    private PlayerController himself;

    private int currentHealth;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    private void Start()
    {
        himself = GetComponent<PlayerController>();
    }
    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0 || damage <= 0)
            return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        himself.Hurted();

        Debug.Log("Player health: " + currentHealth + "/" + maxHealth);

        if (currentHealth <= 0)
        {
            himself.LessHealth();
        }
    }

    private void Die()
    {
        Debug.Log("PLAYER DEFEATED!");
    }
}
