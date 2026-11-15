
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifetime = 3f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public void SetDirection(Vector2 direction)
    {
        Debug.Log("execute moving");
        rb.linearVelocity = direction.normalized * speed;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyAI enemy = other.GetComponent<EnemyAI>();

        if (enemy != null)
        {
            enemy.Ender(damage);
            Destroy(gameObject);
        }
    }
}
