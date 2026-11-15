using Unity.VisualScripting;
using UnityEngine;

public class HittingPlayer : MonoBehaviour
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
        rb.linearVelocity = direction.normalized * speed;
        Destroy(gameObject, lifetime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DecidedHit(collision.collider);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DecidedHit(collision);
    }

    private void DecidedHit(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null && player.hurted && !player.perish)
        {
            player.LessHealth();
        }
    }
}
