using UnityEngine;

public enum ItemType
{
    Weapon,
    Armor,
    Consumable
}

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public Sprite itemIcon;
    public string itemDescription;
    public int itemAttack;
    public int itemDefense;
    public int itemHealth;
    public float itemCritical;
    public int maxStack;    // 최대 소지수
}
