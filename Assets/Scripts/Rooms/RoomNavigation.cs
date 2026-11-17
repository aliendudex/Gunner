using UnityEngine;
using ED262C;
namespace ED262C
{
    public class RoomNavigation : MonoBehaviour
    {
        [Header("Grid Settings")]
        [SerializeField] private int columns = 10;
        [SerializeField] private int rows = 6;
        [SerializeField] private float cellSize = 1f;

        [Header("Grid Position")]
        [SerializeField] private Vector2 localOrigin = new Vector2(-5f, -3f);

        [SerializeField] private Vector2Int testStart = new Vector2Int(0, 0);
        [SerializeField] private Vector2Int testDestination = new Vector2Int(21, 7);

        private ListGraph<GraphNode<Vector2Int>> graph;

        private GraphNode<Vector2Int>[,] nodes;

        private ISimpleList<GraphNode<Vector2Int>> testPath;
        private void Awake()
        {
            BuildGrid();
        }

        //private void Start()
        //{
        //    TestPathfinding();
        //}

        private void TestPathfinding()
        {
            GraphNode<Vector2Int> startNode = GetNode(testStart);
            GraphNode<Vector2Int> destinationNode = GetNode(testDestination);

            if (startNode == null || destinationNode == null)
            {
                Debug.LogError("Invalid start or destination position.");
                return;
            }

            DijkstraSolver<Vector2Int> solver = new DijkstraSolver<Vector2Int>();

            testPath = solver.GetShortestPath(startNode, destinationNode, graph);

            if (testPath == null)
            {
                Debug.Log("No path found.");
                return;
            }

            Debug.Log("Path found! Number of nodes: " + testPath.Count);

            for (int i = 0; i < testPath.Count; i++)
            {
                Debug.Log("Path node: " + testPath[i].Value);
            }
        }

        private void OnDrawGizmos()
        {
            if (testPath == null || testPath.Count == 0)
                return;

            Gizmos.color = Color.yellow;

            for (int i = 0; i < testPath.Count; i++)
            {
                Vector3 currentPosition = GetWorldPosition(testPath[i].Value);

                Gizmos.DrawSphere(currentPosition, 0.15f);

                if (i < testPath.Count - 1)
                {
                    Vector3 nextPosition = GetWorldPosition(testPath[i + 1].Value);
                    Gizmos.DrawLine(currentPosition, nextPosition);
                }
            }
        }


        private void BuildGrid()
        {
            graph = new ListGraph<GraphNode<Vector2Int>>();
            nodes = new GraphNode<Vector2Int>[columns, rows];

            // First, create all grid nodes.
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    Vector2Int cellPosition = new Vector2Int(x, y);
                    GraphNode<Vector2Int> node = new GraphNode<Vector2Int>(cellPosition);
                    nodes[x, y] = node;
                    graph.AddVertex(node);
                }
            }

            // Next, connect only walkable cells.
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    // Skip the outer border.
                    if (!IsWalkable(x, y))
                        continue;

                    GraphNode<Vector2Int> current = nodes[x, y];

                    // Connect to the right, if that cell is walkable.
                    if (x + 1 < columns && IsWalkable(x + 1, y))
                    {
                        graph.AddEdge(current, nodes[x + 1, y], 1);
                        graph.AddEdge(nodes[x + 1, y], current, 1);
                    }

                    // Connect above, if that cell is walkable.
                    if (y + 1 < rows && IsWalkable(x, y + 1))
                    {
                        graph.AddEdge(current, nodes[x, y + 1], 1);
                        graph.AddEdge(nodes[x, y + 1], current, 1);
                    }
                }
            }

            Debug.Log("Navigation grid created: " + columns + " x " + rows);
        }


        public ListGraph<GraphNode<Vector2Int>> GetGraph()
        {
            return graph;
        }

        public GraphNode<Vector2Int> GetNode(Vector2Int position)
        {
            if (position.x < 0 || position.x >= columns ||
                position.y < 0 || position.y >= rows)
            {
                return null;
            }

            return nodes[position.x, position.y];
        }

        public Vector3 GetWorldPosition(Vector2Int position)
        {
            Vector3 localPosition = new Vector3(
                localOrigin.x + (position.x + 0.5f) * cellSize,
                localOrigin.y + (position.y + 0.5f) * cellSize,
                0f
            );

            return transform.TransformPoint(localPosition);
        }

        public bool TryGetNodeFromWorldPosition(
            Vector3 worldPosition,
            out GraphNode<Vector2Int> node)
        {
            Vector3 localPosition = transform.InverseTransformPoint(worldPosition);

            int x = Mathf.FloorToInt(
                (localPosition.x - localOrigin.x) / cellSize
            );

            int y = Mathf.FloorToInt(
                (localPosition.y - localOrigin.y) / cellSize
            );

            node = GetNode(new Vector2Int(x, y));

            return node != null;
        }

        private void OnDrawGizmosSelected()
        {
            if (columns <= 0 || rows <= 0 || cellSize <= 0)
                return;

            Gizmos.color = Color.green;

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    Vector3 worldPosition = GetWorldPosition(
                        new Vector2Int(x, y)
                    );

                    Gizmos.DrawWireCube(
                        worldPosition,
                        Vector3.one * cellSize * 0.9f
                    );
                }
            }
        }
        private bool IsWalkable(int x, int y)
        {
            return x > 0 && x < columns - 1 &&
                   y > 0 && y < rows - 1;
        }
        public Vector3 GetRandomWalkablePosition()
        {
            int x = Random.Range(1, columns - 1);
            int y = Random.Range(1, rows - 1);

            return GetWorldPosition(new Vector2Int(x, y));
        }
    }

}
