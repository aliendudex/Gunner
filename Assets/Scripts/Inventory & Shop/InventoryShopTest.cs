using UnityEngine;
using TMPro;
using ED262C;
using System.Collections.Concurrent;
using UnityEngine.UI;

public class InventoryShopTest : MonoBehaviour
{
    [SerializeField] private ItemData[] shopItems;
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private Transform shopContent;
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private Transform inventoryContent;
    [SerializeField] private ItemEffectManager effectManager;
    private SimpleArrayDictionary<int, ItemData> inventory;
    private SimpleArrayDictionary<int, ItemData> shop;

    private void Start()
    {
        inventory = new SimpleArrayDictionary<int, ItemData>();
        shop = new SimpleArrayDictionary<int, ItemData>();

        foreach (ItemData item in shopItems)
        {
            shop.Add(item.id, item);
        }

        RefreshShop();
        RefreshInventory();
    }

    private void QuickSort(int[] array, int inicio, int fin)
    {
        if (inicio >= fin) 
            return;
        int pivote = Partition(array, inicio, fin);
        QuickSort(array, inicio, pivote - 1);
        QuickSort(array, pivote + 1, fin);
    }
    
    private int Partition(int[] array, int inicio, int fin)
    {
        int pivote = shop[array[fin]].cost;
        int i = inicio - 1;

        for (int j = inicio; j < fin; j++)
        {
            if (shop[array[j]].cost <= pivote)
            {
                i++;
                (array[i], array[j]) = (array[j], array[i]);
            }
        }
        (array[i + 1], array[fin]) = (array[fin], array[i + 1]);
        return i + 1;
    }

    public void ObtainItem(int id)
    {
        if (shop.TryGetValue(id, out ItemData item))
        {
            shop.Remove(id);
            inventory.Add(id, item);

            Debug.Log("Obtuviste: " + item.itemName);
            effectManager.ApplyEffect(item);
            RefreshShop();
            RefreshInventory();
        }
    }
    private void RefreshShop()
    {
        for (int i = 0; i < shopContent.childCount; i++)
        {
            Destroy(shopContent.GetChild(i).gameObject);
        }

        int[] shopIDs = shop.Keys();
        QuickSort(shopIDs, 0, shopIDs.Length - 1);

        foreach (int id in shopIDs)
        {
            ItemData item = shop[id];

            Debug.Log("ID: " + id +
                      " | Item: " + item.itemName +
                      " | Costo: " + item.cost);

            GameObject newItem = Instantiate(shopItemPrefab, shopContent);
            TMP_Text[] texts = newItem.GetComponentsInChildren<TMP_Text>();
            texts[0].text = item.itemName;
            texts[1].text = item.cost + " $";
            Button button = newItem.GetComponent<Button>();
            button.onClick.AddListener(() => ObtainItem(id));
        }
    }
    private void RefreshInventory()
    {
        for (int i = 0; i < inventoryContent.childCount; i++)
        {
            Destroy(inventoryContent.GetChild(i).gameObject);
        }

        int[] inventoryIDs = inventory.Keys();
        QuickSort(inventoryIDs, 0, inventoryIDs.Length - 1);

        foreach (int id in inventoryIDs)
        {
            ItemData item = inventory[id];

            GameObject newItem = Instantiate(inventoryItemPrefab, inventoryContent);
            TMP_Text[] texts = newItem.GetComponentsInChildren<TMP_Text>();
            texts[0].text = item.itemName;
            texts[1].text = item.cost + " $";
            Button button = newItem.GetComponent<Button>();
            button.interactable = false;
        }
    }
}