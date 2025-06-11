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
        // memo : Update���� �ǽð����� �������� üũ�� ���� ������, �Һ� �������� �� �� ����� �� ���� ��� �����͸� ������Ʈ �ϴ� ���� ��ȿ�����̶� �Ǵ�
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
        for (int i = 0; i < slots.Length; i++)  // ������ ���� �� ��ŭ �ݺ�
        {
            if (slots[i] != null)   // �ش� ������ ĭ�� ����ִٸ� �ش� ĭ�� �������� ��ġ�Ѵ�
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
    /// ������ �����͸� �޾� �κ��丮�� �߰��ϴ� �޼ҵ�
    /// </summary>
    /// <param name="data"></param>
    public void AddItem(ItemData data)
    {
        // ���� ���� ������ �������� ���
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

        Debug.LogWarning("�κ��丮�� �� ������ �����ϴ�");

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
            // ���Կ� ��ġ�� �������� üũ�� �����۰� ����, �ش� �������� ������ �ִ� �������� ���� ���
            // �ִ� �������� ���� ���, �� ���Կ� �������� ��ġ�Ѵ�
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
    /// ������ ȹ�� ���� ���� �޼ҵ�
    /// </summary>
    /// <param name="data"></param>
    private void OnFailedToUpdateInventory(ItemData data)
    {
        // to do : ��ȹ��� ����Ʈ�� ����� ��ȹ�� �����ϰ� �غ���
    }
}
