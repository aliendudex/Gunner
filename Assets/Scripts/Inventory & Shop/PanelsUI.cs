using UnityEngine;

public class PanelsUI : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject inventoryPanel;

    public void OpenShop()
    {
        bool active = !shopPanel.activeSelf;
        shopPanel.SetActive(active);
        inventoryPanel.SetActive(false);
    }

    public void OpenInventory()
    {
        bool active = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(active);
        shopPanel.SetActive(false);
    }
}
