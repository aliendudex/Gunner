using UnityEngine;
using ED262C;

public class InventoryShopTest : MonoBehaviour
{
    private SimpleArrayDictionary<int, string> inventory;
    private SimpleArrayDictionary<int, string> shop;

    private void Start()
    {
        inventory = new SimpleArrayDictionary<int, string>();
        shop = new SimpleArrayDictionary<int, string>();

        // Items de la Shop
        shop.Add(101, "Potion");
        shop.Add(102, "Sword");
        shop.Add(103, "Shield");

        // Items que tiene el jugador
        inventory.Add(101, "Potion");
        inventory.Add(103, "Shield");

        Debug.Log("=== SHOP ===");

        int[] shopIDs = shop.Keys();
        string[] shopItems = shop.Values();

        for (int i = 0; i < shop.Count; i++)
        {
            Debug.Log("ID: " + shopIDs[i] + " | Item: " + shopItems[i]);
        }

        Debug.Log("=== INVENTARIO ===");

        int[] inventoryIDs = inventory.Keys();
        string[] inventoryItems = inventory.Values();

        for (int i = 0; i < inventory.Count; i++)
        {
            Debug.Log("ID: " + inventoryIDs[i] + " | Item: " + inventoryItems[i]);
        }

        Debug.Log("=== BUSCAR ITEM ===");

        int itemID = 102;

        if (shop.TryGetValue(itemID, out string item))
        {
            Debug.Log("El ID " + itemID + " corresponde a: " + item);
        }
    }
}