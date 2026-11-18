using ED262C;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossAI : MonoBehaviour
{
    [Header("AI Settings")]
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float attackRange = 4f;
    [SerializeField] private float chaseRange = 5000f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject attack;
    [SerializeField] private GameObject madness;
    [SerializeField] private SpriteRenderer renderer;

    private bool[] animBools = new bool[5];
    public bool actionBool = true;
    private bool hurted = false;
    private bool isDead;
    private int flip = 1;
    private GameObject player;
    private PlayerHealth playerHealth;

    private SimpleArrayPriorityQueue<EnemyContext> priorityQueue;

    private float nextAttackTime;

    [Header("Pathfinding")]
    [SerializeField] private float repathInterval = 0.5f;

    [SerializeField] private RoomNavigation navigation;

    private DijkstraSolver<Vector2Int> pathfinder;

    private ISimpleList<GraphNode<Vector2Int>> currentPath;
    private int pathIndex;
    private float nextPathTime;
    private PlayerController playerController;
    private void Awake()
    {
        priorityQueue = new SimpleArrayPriorityQueue<EnemyContext>();
        pathfinder = new DijkstraSolver<Vector2Int>();
    }

    private void Start()
    {
        actionBool = true;
        player = GameObject.FindGameObjectWithTag("Player");
        navigation = FindFirstObjectByType<RoomNavigation>();
        renderer = GetComponent<SpriteRenderer>();
        if (player != null)
            playerController = player.GetComponent<PlayerController>();
            playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (player == null || navigation == null)
            return;
        if (isDead) return;

        EvaluateContexts();

        if (!priorityQueue.IsEmpty && actionBool && !hurted)
        {
            EnemyContext context = priorityQueue.Dequeue();
            ExecuteContext(context);
        }
    }

    private void EvaluateContexts()
    {
        priorityQueue.Clear();

        float distance = Vector2.Distance(transform.position, player.transform.position);

        priorityQueue.Enqueue(EnemyContext.Idle, 5);

        if (distance <= chaseRange)
            priorityQueue.Enqueue(EnemyContext.Chase, 3);

        if (distance <= attackRange)
            priorityQueue.Enqueue(EnemyContext.Attack, 2);

        //if (hurted == true)
        //    priorityQueue.Enqueue(EnemyContext.Hurt, 1);
    }

    private void ExecuteContext(EnemyContext context)
    {
        switch (context)
        {
            case EnemyContext.Die:
                Activator(false, false, false, true, false);
                break;

            case EnemyContext.Hurt:
                Activator(false, false, false, false, true);
                break;

            case EnemyContext.Attack:
                Activator(false, false, true, false, false);
                Attack();
                break;

            case EnemyContext.Chase:
                Chase();
                break;

            case EnemyContext.Search:
                Debug.Log("SEARCHING");
                break;

            case EnemyContext.Idle:
                Activator(true, false, false, false, false);
                break;
        }
        Animations();
    }

    //private void Chase()
    //{
    //    //transform.position = Vector2.MoveTowards(
    //    //    transform.position,
    //    //    player.transform.position,
    //    //    moveSpeed * Time.deltaTime
    //    //);
    //    //
    //    //float newL = Vector2.MoveTowards(
    //    //    transform.position,
    //    //    player.transform.position,
    //    //    moveSpeed * Time.deltaTime
    //    //).x;

    //    if (Time.time >= nextPathTime)
    //    {
    //        CalculatePath();
    //        nextPathTime = Time.time + repathInterval;
    //    }

    //    FollowPath();

    //    if (transform.position.x < player.transform.position.x) renderer.flipX = false;
    //    else renderer.flipX = true;
    //    Activator(false, true, false, false, false);

    //    if (attackCooldown > nextAttackTime)
    //    {
    //        nextAttackTime += Time.deltaTime * attackCooldown;
    //        return;
    //    }
    //    else
    //    {
    //        float angle = Mathf.Atan2(player.transform.position.y, player.transform.position.x) * Mathf.Rad2Deg - 90f;
    //        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    //        Quaternion final = Quaternion.Slerp(transform.rotation, rotation, 100 * Time.deltaTime);
    //        Vector2 Direction = (player.transform.position - transform.position).normalized;

    //        GameObject bullet = Instantiate(attack, transform.position, Quaternion.identity);

    //        bullet.GetComponent<HittingPlayer>().SetDirection(Direction);

    //        nextAttackTime = 0;
    //    }
    //}
    private void Chase()
    {
        if (Time.time >= nextPathTime)
        {
            CalculatePath();
            nextPathTime = Time.time + repathInterval;
        }

        FollowPath();

        if (renderer != null)
        {
            if (transform.position.x < player.transform.position.x)
                renderer.flipX = false;
            else
                renderer.flipX = true;
        }

        Activator(false, true, false, false, false);
    }

    //public void Attack()
    //{
    //    Instantiate(madness, transform.position, transform.rotation);
    //}

    public void Attack()
    {
        if (playerHealth == null) return;

        if (Time.time < nextAttackTime) return;

        playerHealth.TakeDamage(attackDamage);

        nextAttackTime = Time.time + attackCooldown;

        Debug.Log("Boss attacked the player!");
    }
    private void Animations()
    {
        if (animator == null) return;

        animator.SetBool("Idle", animBools[0]);
        animator.SetBool("Move", animBools[1]);
        animator.SetBool("Attack", animBools[2]);
        animator.SetBool("Die", animBools[3]);
        animator.SetBool("Hurt", animBools[4]);
    }

    private void Activator(bool yeah1, bool yeah2, bool yeah3, bool yeah4, bool yeah5)
    {
        animBools[0] = yeah1;
        animBools[1] = yeah2;
        animBools[2] = yeah3;
        animBools[3] = yeah4;
        animBools[4] = yeah5;
    }

    private void CalculatePath()
    {
        GraphNode<Vector2Int> startNode;
        GraphNode<Vector2Int> targetNode;

        if (!navigation.TryGetNodeFromWorldPosition(
            transform.position, out startNode))
            return;

        if (!navigation.TryGetNodeFromWorldPosition(
            player.transform.position, out targetNode))
            return;

        currentPath = pathfinder.GetShortestPath(
            startNode,
            targetNode,
            navigation.GetGraph()
        );

        pathIndex = 1;
    }

    private void FollowPath()
    {
        if (currentPath == null || currentPath.Count <= 1)
            return;

        if (pathIndex >= currentPath.Count)
            return;

        Vector3 targetPosition = navigation.GetWorldPosition(
            currentPath[pathIndex].Value
        );

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, targetPosition) < 0.05f)
            pathIndex++;
    }
    //public void Ender(float damage)
    //{
    //    health -= damage;
    //    if (health > 0)
    //    {
    //        hurted = true;
    //        Activator(false, false, false, false, true);
    //    }
    //    else hurted = false;

    //    Animations();
    //}
    public void BeginDeath()
    {
        if (isDead) return;

        isDead = true;
        actionBool = false;
        hurted = false;

        Activator(false, false, false, true, false);
        Animations();
    }

    public void Death()
    {
        playerController.Victory();
    }
    public void Moving()
    {
        actionBool = true;
        hurted = false;
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
