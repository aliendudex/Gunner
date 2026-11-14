using UnityEngine;
using ED262C;

public class EnemyAI : MonoBehaviour
{
    [Header("AI Settings")]
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackCooldown = 1f;

    private float nextAttackTime;
    private PlayerHealth playerHealth;

    [Header("Pathfinding")]
    [SerializeField] private float repathInterval = 0.5f;

    private GameObject player;
    private RoomNavigation navigation;

    private SimpleArrayPriorityQueue<EnemyContext> priorityQueue;
    private DijkstraSolver<Vector2Int> pathfinder;

    private ISimpleList<GraphNode<Vector2Int>> currentPath;
    private int pathIndex;
    private float nextPathTime;

    private void Awake()
    {
        priorityQueue = new SimpleArrayPriorityQueue<EnemyContext>();
        pathfinder = new DijkstraSolver<Vector2Int>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navigation = FindFirstObjectByType<RoomNavigation>();
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    private void Update()
    {
        if (player == null || navigation == null)
            return;

        EvaluateContexts();

        if (!priorityQueue.IsEmpty)
        {
            EnemyContext context = priorityQueue.Dequeue();
            ExecuteContext(context);
        }
    }

    private void EvaluateContexts()
    {
        priorityQueue.Clear();

        float distance = Vector2.Distance(
            transform.position,
            player.transform.position
        );

        priorityQueue.Enqueue(EnemyContext.Idle, 4);

        if (distance <= chaseRange)
            priorityQueue.Enqueue(EnemyContext.Chase, 2);

        if (distance <= attackRange)
            priorityQueue.Enqueue(EnemyContext.Attack, 1);
    }

    private void ExecuteContext(EnemyContext context)
    {
        switch (context)
        {
            case EnemyContext.Attack:
                Attack();
                break;

            case EnemyContext.Chase:
                Chase();
                break;

            case EnemyContext.Search:
                Debug.Log("SEARCHING");
                break;

            case EnemyContext.Idle:
                break;
        }
    }

    private void Chase()
    {
        if (Time.time >= nextPathTime)
        {
            CalculatePath();
            nextPathTime = Time.time + repathInterval;
        }

        FollowPath();
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
    private void Attack()
    {
        if (playerHealth == null)
            return;

        if (Time.time < nextAttackTime)
            return;

        playerHealth.TakeDamage(attackDamage);

        nextAttackTime = Time.time + attackCooldown;

        Debug.Log("Enemy attacked the player!");
    }
}
