using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData item;

    public Button button;
    public Image icon;
    public TextMeshProUGUI quantityText;
    public TextMeshProUGUI equippedText;
    private Outline outline;

    public UIInventory inventory;

    public int index;
    public bool isEquipped;
    public int quantity;

    void Awake()
    {
        outline = GetComponent<Outline>();
    }

    void Update()
    {
        
    }

    private void OnEnable()
    {
        outline.enabled = isEquipped;   // 장착 아이템 강조
    }

    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = item.itemIcon;
        quantityText.text = quantity > 1 ? quantity.ToString() : string.Empty; // 수량이 1보다 크면 표시, 아니면 표시하지 않도록 설계
        equippedText.gameObject.SetActive(isEquipped);

        if (outline != null)
        {
            outline.enabled = isEquipped;   // 장착 아이템 강조
        }
    }

    public void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }

    public void OnClickButton()
    {
        inventory.SelectItem(index);
    }
}
