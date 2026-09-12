using Unity.IO.LowLevel.Unsafe;

namespace ED262C
{
    public class SimpleArrayPriorityQueue<T> : ISimplePriorityQueue<T>
    {
        int defaultCapacity = 4;
        int count = 0;
        T[] internalArray;
        int[] priorities; // Nuevo array para prioridades
        // Inicializamos los dos arrays ahora
        public SimpleArrayPriorityQueue()
        {
            internalArray = new T[defaultCapacity];
            priorities = new int[defaultCapacity];
        }

        public int Count => count;

        public bool IsEmpty => count == 0;

        // Ahora vacia los dos arrays
        public void Clear()
        {
            internalArray = new T[internalArray.Length];
            priorities = new int[priorities.Length];
            count = 0;
        }

        public T Dequeue()
        {
            if (IsEmpty) throw new System.InvalidOperationException("Cannot Dequeue from empty Queue.");
            // Guardamos el elemento en una variable temporal
            // Hacemos esto para no perderlo
            T result = internalArray[0];
            // Lo sacamos y luego devolvemos el dato con la variable temporal
            ShiftLeft(0, 1);
            count--;
            return result;
        }

        public void Enqueue(T item, int priority)
        {
            // Primero chequeamos que haya espacio
            ValidateSize(count + 1);

            
            int insertIndex = count;
            // Recorremos de atras para adelante, hasta 1 (no 0)
            // Solo seguimos iterando si la prioridad es menor (mas prioritario) que el indice anterior
            for(int i = count; i > 0 && priority < priorities[i - 1]; i--)
            {
                // Corremos lo que esta en el indice anterior al indice actual
                // Movemos para la derecha, dejando un hueco en el indice anterior
                internalArray[i] = internalArray[i - 1];
                priorities[i] = priorities[i - 1];
                // Vamos a insertar en ese hueco
                insertIndex = i - 1;
            }
            internalArray[insertIndex] = item;
            priorities[insertIndex] = priority;

                // La cantidad de elementos ocupados es igual al primer indice  libre 
                count++;
        }
        // Igual que Peek de Stack pero con el PRIMER elemento
        public T Peek()
        {
            if (IsEmpty) throw new System.InvalidOperationException("Cannot Peek from empty PriorityQueue.");
            return internalArray[0];
        }

        // Nueva funcion: devuelve la primera prioridad
        public int GetHighestPriority()
        {
            if (IsEmpty) throw new System.InvalidOperationException("Empty PriorityQueue.");
            return priorities[0];
        }

        // Igual que List y Stack
        public T[] ToArray()
        {
            // Creo un array con la cantidad de elementos que estan ocupados en la lista
            T[] result = new T[count];

            // Copiamos uno por uno todos los elementos al nuevo array
            for (int i = 0; i < count; i++)
                result[i] = internalArray[i];

            // Devolvemos el array completo
            return result;
        }

        void ValidateSize(int nextIndex)
        {
            if (nextIndex >= internalArray.Length) Resize(nextIndex);
        }

        // Le pasamos cuantos elementos va a tener despues de agregar
        void Resize(int targetAmount)
        {
            // Guaramos el largo del array al principio
            int currentLength = internalArray.Length;

            // Vamos a duplicar ese tamaño mientras sea mas chico que la cantidad que queremos
            while (targetAmount > currentLength)
                currentLength *= 2;

            // Creamos un array del doble de largo que el actual
            T[] nextArray = new T[currentLength];
            int[] nextPriorities = new int[currentLength];

            // Copiamos todo lo que hay en los arrays actuales a los nuevos
            for (int i = 0; i < count; i++)
            {
                nextArray[i] = internalArray[i];
                nextPriorities[i] = priorities[i];
            }

            // Reemplazamos los arrays actuales por los nuevos
            internalArray = nextArray;
            priorities = nextPriorities;
        }

        // Corremos todo lo que viene despues de index para la izquierda
        // Asi no quedan espacios en el medio
        void ShiftLeft(int index, int offset)
        {
            // Lo que esta en el casillero actual se pisa con el siguiente
            for (int i = index; i < count - offset; i++)
            {
                internalArray[i] = internalArray[i + offset];
                priorities[i] = priorities[i + offset];
            }

            // Limpiamos todo lo que vaya despues del ultimo elemento
            // para no guardar datos que ya no necesitamos
            for (int i = count - offset; i < count; i++)
            {
                internalArray[i] = default;
                priorities[i] = default;
            }
        }
    }
}
