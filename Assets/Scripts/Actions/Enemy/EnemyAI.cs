using UnityEngine;
using ED262C;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float health = 1f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject attack;
    [SerializeField] private SpriteRenderer renderer;

    private bool[] animBools = new bool[4];
    public bool actionBool = true;
    private int flip = 1;
    private GameObject player;

    private SimpleArrayPriorityQueue<EnemyContext> priorityQueue;

    private void Awake()
    {
        priorityQueue = new SimpleArrayPriorityQueue<EnemyContext>();
        actionBool = true;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        EvaluateContexts();

        if (!priorityQueue.IsEmpty && actionBool)
        {
            EnemyContext context = priorityQueue.Dequeue();
            ExecuteContext(context);
        }
    }

    private void EvaluateContexts()
    {
        priorityQueue.Clear();

        float distance = Vector2.Distance(transform.position, player.transform.position);

        priorityQueue.Enqueue(EnemyContext.Idle, 4);

        if (distance <= chaseRange)
            priorityQueue.Enqueue(EnemyContext.Chase, 2);

        if (distance <= attackRange)
            priorityQueue.Enqueue(EnemyContext.Attack, 1);

        if (health <= 0)
            priorityQueue.Enqueue(EnemyContext.Die, 0);
    }

    private void ExecuteContext(EnemyContext context)
    {
        switch (context)
        {
            case EnemyContext.Die:
                Activator(false, false, false, true);
                break;

            case EnemyContext.Attack:
                Activator(false, false, true, false);
                actionBool = false;
                break;

            case EnemyContext.Chase:
                Chase();
                break;

            case EnemyContext.Search:
                Debug.Log("SEARCHING");
                break;

            case EnemyContext.Idle:
                Activator(true, false, false, false);
                break;
        }
        Animations();
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.transform.position,
            moveSpeed * Time.deltaTime
        );

        float newL = Vector2.MoveTowards(
            transform.position,
            player.transform.position,
            moveSpeed * Time.deltaTime
        ).x;

        if (transform.position.x < player.transform.position.x) renderer.flipX = false;
        else renderer.flipX = true;
        Activator(false, true, false, false);
    }

    public void Attack()
    {
        float angle = Mathf.Atan2(player.transform.position.y, player.transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Quaternion final = Quaternion.Slerp(transform.rotation, rotation, 100 * Time.deltaTime);
        //Vector2 Direction = (player.transform.position - transform.position).normalized;

        Instantiate(attack, transform.position, final);
    }

    private void Animations()
    {
        animator.SetBool("Idle", animBools[0]);
        animator.SetBool("Move", animBools[1]);
        animator.SetBool("Attack", animBools[2]);
        animator.SetBool("Die", animBools[3]);
    }

    private void Activator(bool yeah1, bool yeah2, bool yeah3, bool yeah4)
    {
        animBools[0] = yeah1;
        animBools[1] = yeah2;
        animBools[2] = yeah3;
        animBools[3] = yeah4;
    }

    public void Ender()
    {
        Destroy(gameObject);
    }

    public void Bye_Bye()
    {
        Debug.Log("Got hit pal");
        health = 0;
    }
    public void Moving()
    {
        actionBool = true;
    }
    public void Stopping()
    {
        actionBool = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish")) Destroy(this.gameObject);
    }
}