
using UnityEngine;
using ED262C;

public class TreeTestScene : MonoBehaviour
{
    void Start()
    {
        AVL<ShopItem> shopTree = new AVL<ShopItem>();

        shopTree.Insert(new ShopItem("Potion", 20));
        shopTree.Insert(new ShopItem("Sword", 80));
        shopTree.Insert(new ShopItem("Shield", 50));
        shopTree.Insert(new ShopItem("Bow", 65));
        shopTree.Insert(new ShopItem("Helmet", 35));

        Debug.Log("===== ALL SHOP ITEMS (SORTED) =====");

        ISimpleList<ShopItem> sortedItems = shopTree.InOrderList();

        for (int i = 0; i < sortedItems.Count; i++)
        {
            Debug.Log(sortedItems[i]);
        }

        int maxPrice = 50;

        Debug.Log("===== ITEMS COSTING $" + maxPrice + " OR LESS =====");

        for (int i = 0; i < sortedItems.Count; i++)
        {
            if (sortedItems[i].Price <= maxPrice)
            {
                Debug.Log(sortedItems[i]);
            }
        }

        Debug.Log("Tree Height: " + shopTree.Height);
        Debug.Log("Root Balance Factor: " + shopTree.BalanceFactor);
    }
}
