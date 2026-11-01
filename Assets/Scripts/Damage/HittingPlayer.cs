using Unity.VisualScripting;
using UnityEngine;

public class HittingPlayer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null && player.hurted && !player.perish) 
        {
            player.LessHealth();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null && player.hurted && !player.perish)
        {
            player.LessHealth();
        }
    }
}
