using UnityEngine;

public enum ItemEffect
{
    None,
    IncreaseHealth,
    IncreaseDamage,
    IncreaseSpeed
}

[System.Serializable]
public class ItemData
{
    public int id;
    public string itemName;
    public GameObject prefab;
    public int cost;
    public ItemEffect effect;
    public float effectValue;
}