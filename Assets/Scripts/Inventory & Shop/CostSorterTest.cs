using UnityEngine;

public class CostSorterTest : MonoBehaviour
{
    private struct Item
    {
        public string nombre;
        public int costo;
        public int rareza;

        public Item(string nombre, int costo, int rareza)
        {
            this.nombre = nombre;
            this.costo = costo;
            this.rareza = rareza;
        }
    }

    private Item[] items =
    {
        new Item("Poción de Vida", 20, 3),
        new Item("Poción de Escudo", 30, 1),
        new Item("Espada", 200, 4),
        new Item("Escudo", 100, 2)
    };

    private void Start()
    {
        QuickSort(items, 0, items.Length - 1);

        foreach (Item item in items)
            Debug.Log(item.nombre + " | Costo: " + item.costo);
    }

    private void QuickSort(Item[] array, int inicio, int fin)
    {
        if (inicio >= fin) return;

        int pivote = Partition(array, inicio, fin);

        QuickSort(array, inicio, pivote - 1);
        QuickSort(array, pivote + 1, fin);
    }

    private int Partition(Item[] array, int inicio, int fin)
    {
        int pivote = array[fin].costo;
        int i = inicio - 1;

        for (int j = inicio; j < fin; j++)
        {
            if (array[j].costo <= pivote)
            {
                i++;
                (array[i], array[j]) = (array[j], array[i]);
            }
        }

        (array[i + 1], array[fin]) = (array[fin], array[i + 1]);

        return i + 1;
    }
}