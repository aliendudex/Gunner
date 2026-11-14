using System;
namespace ED262C
{
    public class DijkstraSolver<T> : IPathfindingSolver<T>
    {
        public ISimpleDictionary<GraphNode<T>, Edge<GraphNode<T>>> GetAllShortestPaths(GraphNode<T> origin, IGraph<GraphNode<T>> graph)
        {
            // Creamos la "tabla" con toda la info
            ISimpleDictionary<GraphNode<T>, Edge<GraphNode<T>>> result = new SimpleArrayDictionary<GraphNode<T>, Edge<GraphNode<T>>>();

            SimpleArrayPriorityQueue<GraphNode<T>> unvisited = new SimpleArrayPriorityQueue<GraphNode<T>>();
            SimpleArraySet<GraphNode<T>> visited = new SimpleArraySet<GraphNode<T>>();

            // Inicializamos la tabla
            GraphNode<T>[] vertices = graph.Vertices;

            for(int i = 0; i < vertices.Length; i++)
            {
                result.Add(vertices[i], new Edge<GraphNode<T>>(null, int.MaxValue));
            }

            result[origin].weight = 0;

            unvisited.Enqueue(origin, result[origin].weight);
            // Mientras haya algun nodo para visitar, seguimos iterando
            while (!unvisited.IsEmpty)
            {
                // Sacamos el primer nodo de la queue
                GraphNode<T> current = unvisited.Dequeue();
                // Si ya lo visitamos, sigue de largo
                if (visited.Contains(current))
                {
                    continue;
                }
                // Guardamos todos los vecinos del nodo a evaluar
                ISimpleList<Edge<GraphNode<T>>> neighbors = graph.GetNeighbors(current);

                // Recorremos toda la lista de vecinos
                for(int i = 0; i < neighbors.Count; i++)
                {
                    // Sumamos el costo total hasta el nodo actual (de la tabla)
                    // mas el costo de ese nodo actual hasta el vecino que estamos revisitando
                    int totalWeight = result[current].weight + neighbors[i].weight;

                    // Si el costo total es menor al que esta en la tabla para el vecino
                    // actualizamos ese costo con el que calculamos (buscamos siempre el menor costo)
                    if(totalWeight < result[neighbors[i].to].weight)
                    {
                        result[neighbors[i].to].weight = totalWeight;
                        result[neighbors[i].to].to = current;
                    }

                    // Agregamos el vecino a la queue si no fue ya visitado
                    if (!visited.Contains(neighbors[i].to))
                    {
                        unvisited.Enqueue(neighbors[i].to, totalWeight);
                    }
                }

                visited.Add(current);
            }
            // Devolvemos la tabla completa
            return result;
        }

        public ISimpleList<GraphNode<T>> GetShortestPath(GraphNode<T> origin, GraphNode<T> destination, IGraph<GraphNode<T>> graph)
        {

            // Creamos la "tabla" con toda la info
            ISimpleDictionary<GraphNode<T>, Edge<GraphNode<T>>> table = new SimpleArrayDictionary<GraphNode<T>, Edge<GraphNode<T>>>();

            SimpleArrayPriorityQueue<GraphNode<T>> unvisited = new SimpleArrayPriorityQueue<GraphNode<T>>();
            SimpleArraySet<GraphNode<T>> visited = new SimpleArraySet<GraphNode<T>>();

            // Inicializamos la tabla
            GraphNode<T>[] vertices = graph.Vertices;

            for (int i = 0; i < vertices.Length; i++)
            {
                table.Add(vertices[i], new Edge<GraphNode<T>>(null, int.MaxValue));
            }

            table[origin].weight = 0;

            unvisited.Enqueue(origin, table[origin].weight);
            // Mientras haya algun nodo para visitar, seguimos iterando
            while (!unvisited.IsEmpty)
            {
                // Sacamos el primer nodo de la queue
                GraphNode<T> current = unvisited.Dequeue();

                if(current == destination)
                {
                    break;
                }
                // Si ya lo visitamos, sigue de largo
                if (visited.Contains(current))
                {
                    continue;
                }
                // Guardamos todos los vecinos del nodo a evaluar
                ISimpleList<Edge<GraphNode<T>>> neighbors = graph.GetNeighbors(current);

                // Recorremos toda la lista de vecinos
                for (int i = 0; i < neighbors.Count; i++)
                {
                    // Sumamos el costo total hasta el nodo actual (de la tabla)
                    // mas el costo de ese nodo actual hasta el vecino que estamos revisitando
                    int totalWeight = table[current].weight + neighbors[i].weight;

                    // Si el costo total es menor al que esta en la tabla para el vecino
                    // actualizamos ese costo con el que calculamos (buscamos siempre el menor costo)
                    if (totalWeight < table[neighbors[i].to].weight)
                    {
                        table[neighbors[i].to].weight = totalWeight;
                        table[neighbors[i].to].to = current;
                    }

                    // Agregamos el vecino a la queue si no fue ya visitado
                    if (!visited.Contains(neighbors[i].to))
                    {
                        unvisited.Enqueue(neighbors[i].to, totalWeight);
                    }
                }

                visited.Add(current);
            }
            // Si el ultimo no es alcanzable, no hay camino posible y devolvemos null
            if (table[destination].to == null) return null;
            return GeneratePath(destination, table);
        }

        ISimpleList<GraphNode<T>> GeneratePath(GraphNode<T> destination, ISimpleDictionary<GraphNode<T>, Edge<GraphNode<T>>> table)
        {
            // Creamos la lista de atras para adelante
            ISimpleStack<GraphNode<T>> reversePath = new SimpleLinkedStack<GraphNode<T>>();

            // Empezamos por el ultimo nodo del camino (destino)
            GraphNode<T> current = destination;

            // Mientras haya un nodo para evaluar...
            while(current != null)
            {
                // Lo agregamos al camino
                reversePath.Push(current);
                // Pasamos al nodo anterior
                current = table[current].to;
            }

            // Creamos el path definitivo (del primero al ultimo)
            ISimpleList<GraphNode<T>> path = new SimpleArrayList<GraphNode<T>>(reversePath.Count);
            // Invertimos la lista
            while (!reversePath.IsEmpty)
            {
                path.Add(reversePath.Pop());
            }

            return path;
        }
    }
}
