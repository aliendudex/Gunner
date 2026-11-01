using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public StateMachine stateMachine;
    public Rigidbody2D rb;
    public Vector2 movement;
    public Animator animator;
    public bool hurted;
    public bool attack;
    public bool perish;
    private int health = 2;
    public int Health => health;

    [SerializeField] private GameObject attacked;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateMachine = new StateMachine(this);
        hurted = false;
        attack = false;
        perish = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!hurted && !attack) stateMachine.UpdateMachine();
    }

    public void LessHealth() 
    {
        health--;
        perish = true;
    }

    public void Restarted() 
    {
        perish = false;
        attack = false;
        hurted = false;
        animator.SetBool("Hurt", false);
        animator.SetBool("Attack", false);
    }

    public void To0() 
    {
        SceneManager.LoadScene("SceneTest");
    }
    public void Attack() 
    {
        Instantiate(attacked,transform.position,transform.rotation);
    }
}
