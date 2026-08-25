using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed = 5;

    public Rigidbody2D rb;

    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }
}
