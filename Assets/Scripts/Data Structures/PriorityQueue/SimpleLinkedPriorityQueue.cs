using ED262C;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class SimpleLinkedPriorityQueue<T> : ISimplePriorityQueue<T>
{
    int count = 0;
    LinkedPriorityNode<T> first;
    LinkedPriorityNode<T> last;
    public int Count => count;

    public bool IsEmpty => count == 0;

    public void Clear()
    {
        count = 0;
        first = null;
        last = null;
    }

    public T Dequeue()
    {
        if (first == null)
            last = null;
        else
            first.prev = null;

        if (IsEmpty) throw new System.Exception("Cannot Dequeue from this Priority Linked Queue");

        T result = first.value;
        first = first.next;

        if (first != null) first.prev = null;
        count--;
        return result;
    }

    public void Enqueue(T item, int priority)
    {
        LinkedPriorityNode<T> newNode = new LinkedPriorityNode<T>(item, priority);

        // Caso 1: La Queue esta vacia
        if (first == null)
        {
            first = newNode;
            last = newNode;
            count++;
            return;
        }

        // Caso 2: Tiene mayor prioridad que el first (va al principio)
        if (priority > first.priority)
        {
            newNode.next = first;
            first.prev = newNode;
            first = newNode;
            count++;
            return;
        }

        // Caso 3: Recorremos desde atras hacia adelante (de last a first)
        LinkedPriorityNode<T> current = last;

        // Retrocedemos mientras no lleguemos al principio Y el nodo actual tenga MENOR prioridad que el nuevo
        while (current.prev != null && current.priority < priority)
        {
            current = current.prev;
        }

        // Caso 3A: Si el nuevo nodo tiene MENOR prioridad que el last (va al final de todo)
        if (current == last && priority < last.priority)
        {
            newNode.prev = last;
            last.next = newNode;
            last = newNode; // El nuevo nodo pasa a ser el LAST
        }
        // Caso 3B: Va en el medio de la lista
        else
        {
            // Conectamos newNode con los nodos vecinos
            newNode.prev = current.prev;
            newNode.next = current;

            // Si tenia un nodo a la izquierda, conectamos su 'next' al nuevo nodo
            if (newNode.prev != null)
            {
                newNode.prev.next = newNode;
            }

            // Conectamos el 'prev' de current al nuevo nodo
            current.prev = newNode;
        }

        count++;
    }

    public int GetHighestPriority()
    {
        if (IsEmpty) throw new System.Exception("Linked Priority Queue is empty");

        return first.priority;
    }

    public T Peek()
    {
        if (IsEmpty) throw new Exception("Cannot Peek from empty LinkedPriorityQueue");

        return first.value;
    }

    public T[] ToArray()
    {
        T[] result = new T[count];
        LinkedPriorityNode<T> current = first;

        for (int i = 0; i < count; i++)
        {
            //recorro el array y le asigno a la posicion actual el valur del nodo
            result[i] = current.value;
            //despues paso al siguiente nodo
            current = current.next;
        }
        return result;
    }
}