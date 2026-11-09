using Unity.VisualScripting;
using UnityEngine;

public class HittingPlayer : MonoBehaviour
{
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
