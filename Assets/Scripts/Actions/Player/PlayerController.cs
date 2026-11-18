using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private GameObject spawnAttack;
    //[SerializeField] private GameObject spawnOrigin;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject defeatPanel;
    public StateMachine stateMachine;
    public Rigidbody2D rb;
    public Vector2 movement;
    public Animator animator;
    public bool hurted;
    public bool attack;
    public bool perish;
    [SerializeField] private int health = 2;
    public int Health => health;
    public int MaxHealth => health;

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
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (perish)
        {
            defeatPanel.SetActive(true);
            return;
        }
        if (!hurted && !attack) stateMachine.UpdateMachine();
    }

    public void LessHealth()
    {
        health--;
        if (health <= 0)
        {
            health = 0;
            perish = true;
        }
        else
        {
            perish = false;
        }
    }
    public void IncreaseHealth(int amount)
    {
        health += amount;
    }

    public void Restarted()
    {
        animator.SetBool("Hurt", false);

        perish = false;
        attack = false;
        hurted = false;
        defeatPanel.SetActive(false);

        animator.SetBool("Attack", false);
        animator.SetBool("Idle", true);
    }
    public void Victory()
    {
        victoryPanel.SetActive(true);
    }

    public void LeftOrRight(bool flipdecide) 
    {
        render.flipX = flipdecide;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EvilHit")) LessHealth();
    }
    public void Defeat()
    {
        perish = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
