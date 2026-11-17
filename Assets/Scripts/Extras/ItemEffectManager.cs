using UnityEngine;

public class ItemEffectManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    public int damage = 1;
    public float speed = 5;

    public void ApplyEffect(ItemData item)
    {
        switch (item.effect)
        {
            case ItemEffect.IncreaseHealth:
                playerController.IncreaseHealth((int)item.effectValue);
                break;

            case ItemEffect.IncreaseDamage:
                damage += (int)item.effectValue;
                break;

            case ItemEffect.IncreaseSpeed:
                speed += item.effectValue;
                break;
        }

        Debug.Log("Efecto aplicado: " + item.itemName);
    }
}