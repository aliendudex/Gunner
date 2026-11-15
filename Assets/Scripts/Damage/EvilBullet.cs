using UnityEngine;

public class EvilBullet : MonoBehaviour
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
}
