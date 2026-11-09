using UnityEngine;

public class HittingEnemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DecideHit(collision.collider);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DecideHit(collision);
    }

    private void DecideHit(Collider2D collision) 
    {
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
        if (enemy != null)
        {
            enemy.Bye_Bye();
        }
    }
}
