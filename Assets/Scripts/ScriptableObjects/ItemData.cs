using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public int itemAttack;
    public int itemDefense;
    public int itemHealth;
    public float itemCritical;
}
