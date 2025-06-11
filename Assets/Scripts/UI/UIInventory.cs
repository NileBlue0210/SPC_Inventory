using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;

    public GameObject inventoryWindow;
    public Transform slotPanel;

    public Image selectedItemIcon;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemAttack;
    public TextMeshProUGUI selectedItemDefense;
    public TextMeshProUGUI selectedItemHealth;
    public TextMeshProUGUI selectedItemCritical;

    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;
    public GameObject dropButton;

    private PlayerController controller;
    private PlayerCondition condition;

    void Start()
    {
        controller = GameManager.Instance.Player.controller;
        condition = GameManager.Instance.Player.condition;

        // inventoryWindow.SetActive(false);

        slots = new ItemSlot[slotPanel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }

        ClearSelectedItemWindow();
    }

    void Update()
    {
        // memo : Update에서 실시간으로 아이템을 체크할 수도 있지만, 소비 아이템을 한 번 사용할 때 마다 모든 데이터를 업데이트 하는 것은 비효율적이라 판단
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void BackToMainMenu()
    {
        Hide();
        UIManager.Instance.ShowMainMenu();
    }

    private void ClearSelectedItemWindow()
    {
        selectedItemIcon.sprite = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedItemAttack.text = string.Empty;
        selectedItemDefense.text = string.Empty;
        selectedItemHealth.text = string.Empty;
        selectedItemCritical.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        unEquipButton.SetActive(false);
        dropButton.SetActive(false);
    }

    private void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)  // 아이템 슬롯 수 만큼 반복
        {
            if (slots[i] != null)   // 해당 아이템 칸이 비어있다면 해당 칸에 아이템을 배치한다
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
    }

    /// <summary>
    /// 아이템 데이터를 받아 인벤토리에 추가하는 메소드
    /// </summary>
    /// <param name="data"></param>
    public void AddItem(ItemData data)
    {
        // 복수 소지 가능한 아이템일 경우
        if (data.CanStack)
        {
            ItemSlot slot = GetItemStack(data);

            if (slot != null)
            {
                slot.quantity++;

                UpdateUI();

                return;
            }
        }

        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = data;
            emptySlot.quantity = 1;
            emptySlot.isEquipped = false;

            UpdateUI();

            return;
        }

        Debug.LogWarning("인벤토리에 빈 공간이 없습니다");

        OnFailedToUpdateInventory(data);
    }

    public void UseItem()
    {

    }

    public void EquipItem()
    {

    }

    public void UnEquipItem()
    {

    }

    public void DropItem()
    {

    }

    private ItemSlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // 슬롯에 배치된 아이템이 체크할 아이템과 같고, 해당 아이템의 수량이 최대 수량보다 적을 경우
            // 최대 수량보다 많을 경우, 새 슬롯에 아이템을 배치한다
            if (slots[i].item == data && slots[i].quantity < data.maxStack)
            {
                return slots[i];
            }
        }

        return null;
    }

    private ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }

        return null;
    }

    /// <summary>
    /// 아이템 획득 실패 시의 메소드
    /// </summary>
    /// <param name="data"></param>
    private void OnFailedToUpdateInventory(ItemData data)
    {
        // to do : 재획득용 리스트를 만들어 재획득 가능하게 해보자
    }
}
