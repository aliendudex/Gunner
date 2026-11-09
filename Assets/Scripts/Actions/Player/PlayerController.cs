using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject spawnAttack;
    [SerializeField] private GameObject spawnOrigin;
    public StateMachine stateMachine;
    public Rigidbody2D rb;
    public Vector2 movement;
    public Animator animator;
    public bool hurted;
    public bool attack;
    public bool perish;
    [SerializeField] private int health = 2;
    public int Health => health;

    private SpriteRenderer render;

    [SerializeField] private GameObject attacked;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateMachine = new StateMachine(this);
        hurted = false;
        attack = false;
        perish = false;

        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hurted && !attack) stateMachine.UpdateMachine();
    }

    public void LessHealth()
    {
        health--;
        perish = true;
    }

    public void Restarted()
    {
        animator.SetBool("Hurt", false);

        perish = false;
        attack = false;
        hurted = false;
        
        animator.SetBool("Attack", false);
        animator.SetBool("Idle", true);
    }

    public void To0()
    {
        SceneManager.LoadScene("PantallaDerrota");
    }
    public void Attack()
    {
        Instantiate(attacked, spawnAttack.transform.position, spawnOrigin.transform.rotation);
    }

    public void LeftOrRight(bool flipdecide) 
    {
        render.flipX = flipdecide;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EvilHit")) LessHealth();
    }
}
