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
    public bool CanStack; // 아이템이 중복 소지 가능한지 여부
    public int maxStack;    // 최대 소지수
}
