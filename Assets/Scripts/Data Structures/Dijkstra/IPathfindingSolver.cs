namespace ED262C
{
    // Podemos modificar esto para que no sea generico y que trabaje con GraphNode directamente
    public interface IPathfindingSolver<T>
    {
        public ISimpleDictionary<GraphNode<T>, Edge<GraphNode<T>>> GetAllShortestPaths(GraphNode<T> origin, IGraph<GraphNode<T>> graph);
        public ISimpleList<GraphNode<T>> GetShortestPath(GraphNode<T> origin, GraphNode<T> destination, IGraph<GraphNode<T>> graph);
    }
}
