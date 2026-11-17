using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 30;

    private int currentHealth;
    private BossAI bossAI;
    private bool isDead;

    private void Awake()
    {
        bossAI = GetComponent<BossAI>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        Debug.Log("Boss took damage. Remaining health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Boss defeated!");

        if (bossAI != null)
            bossAI.Death();
    }
}
