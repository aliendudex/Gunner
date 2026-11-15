namespace ED262C
{
    // Como esta planteado, este nodo es un wrapper para que T sea nulleable
    // Pueden expandirlo segune el Enfoque Orientado a Objetos, dandole mas funcionalidad
    // Ej: que tenga si lista de vecinos...

    // Tambien podemos hacer que no sea generico
    public class GraphNode<T>
    {
        public T Value;
        // Para top-down 2D: Vector2Int y Matriz GraphNode<Vector2Int>[,]
        // for(int x = 0...)
        // for(int y = 0...)
        public GraphNode(T value) => this.Value = value;
    }
}
