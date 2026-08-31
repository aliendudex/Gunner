using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public StateMachine stateMachine;
    public Rigidbody2D rb;
    public Vector2 movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateMachine = new StateMachine(this);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.UpdateMachine();
    }
}
