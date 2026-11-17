using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    private EnemyAI himself;

    private int currentHealth;

    public int PublicHealth => currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    private void Start()
    {
        himself = GetComponent<EnemyAI>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        himself.Hurted();

        Debug.Log("Enemy took damage. Remaining health: " + currentHealth);

        //if (currentHealth <= 0)
        //{
        //    Die();
        //}
    }

    private void Die()
    {
        Debug.Log("Enemy defeated!");
        Destroy(gameObject);
    }
}
