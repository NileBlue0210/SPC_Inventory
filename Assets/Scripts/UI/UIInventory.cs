using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;

    public GameObject inventoryWindow;
    public GameObject selectedItemWindow;
    public GameObject slot;
    public Transform slotPanel;
    public TextMeshProUGUI remainSlotCountText;

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

    ItemData selectedItem;
    int selectedItemIndex = 0;  // ���õ� ������ �ε���
    int currentEquipItemIndex;  // ���� ������ ������ �ε���
    int usedItemSlotCount = 0;  // ���� ������ ���� ��
    [SerializeField] int maxInventorySlot;

    void Start()
    {
        controller = GameManager.Instance.Player.controller;
        condition = GameManager.Instance.Player.condition;

        CloseSelectedItemWindow();  // ���� ������ ���� â�� ���� �� ��Ȱ��ȭ
        Hide(); // �κ��丮 â�� ���� �� ��Ȱ��ȭ

        AddInventorySlot(maxInventorySlot);

        slots = new ItemSlot[slotPanel.childCount];

        UpdateSlotData();

        ClearSelectedItemWindow();

        remainSlotCountText.text = $"{usedItemSlotCount} / {maxInventorySlot.ToString()}";  // ���� ���� �� �ʱ�ȭ
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
        if (selectedItemWindow.activeSelf)
        {
            CloseSelectedItemWindow();
        }

        ClearSelectedItemWindow();

        gameObject.SetActive(false);
    }

    public void OpenSelectedItemWindow()
    {
        selectedItemWindow.SetActive(true);
    }

    public void CloseSelectedItemWindow()
    {
        selectedItemWindow.SetActive(false);
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
        usedItemSlotCount = 0;  // ���� ������ ���� �� �ʱ�ȭ

        for (int i = 0; i < slots.Length; i++)  // ������ ���� �� ��ŭ �ݺ�
        {
            if (slots[i].item != null)   // �ش� ������ ĭ�� ����ִٸ� �ش� ĭ�� �������� ��ġ�Ѵ�
            {
                slots[i].Set();

                // 2���� ȣ��ǰ� �ִ�.
                usedItemSlotCount++;  // ���� ������ ���� �� ����
            }
            else
            {
                slots[i].Clear();
            }
        }

        remainSlotCountText.text = $"{usedItemSlotCount} / {maxInventorySlot.ToString()}";  // ���� ���� �� ������Ʈ
    }

    /// <summary>
    /// ������ �����͸� �޾� �κ��丮�� �߰��ϴ� �޼ҵ�
    /// </summary>
    /// <param name="data"></param>
    public void AddItem(ItemData data)
    {
        if (usedItemSlotCount >= maxInventorySlot)   // �κ��丮 ������ ���� á�� ���, ������ ȹ�� ���� ó��
        {
            Debug.LogWarning("�κ��丮 ������ ���� á���ϴ�. �������� �߰��� �� �����ϴ�.");

            OnFailedToUpdateInventory(data);

            return;
        }

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
        if (selectedItem.itemType == ItemType.Consumable)
        {

        }
    }

    public void EquipItem()
    {
        slots[selectedItemIndex].isEquipped = true;
        currentEquipItemIndex = selectedItemIndex;  // to do : ���� ���� �ý����� ����� ���� �ʿ� ( �迭���� ������ �ʿ䰡 ���� )

        // �÷��̾��� ������ �����ۿ� �°� ������Ʈ
        controller.Attack += selectedItem.itemAttack;
        controller.Defense += selectedItem.itemDefense;
        controller.MaxHealth += selectedItem.itemHealth;
        controller.Critical += selectedItem.itemCritical;

        UpdateUI();

        SelectItem(selectedItemIndex);  // ������ ���� ��, ���� ������ ���� â ���ε�
    }

    public void UnEquipItem(int index)
    {
        slots[index].isEquipped = false;

        // �÷��̾��� ������ �����ۿ� �°� ������Ʈ
        controller.Attack -= slots[index].item.itemAttack;
        controller.Defense -= slots[index].item.itemDefense;
        controller.MaxHealth -= slots[index].item.itemHealth;
        controller.Critical -= slots[index].item.itemCritical;

        UpdateUI();

        if (selectedItemIndex == index)
        {
            SelectItem(selectedItemIndex);  // ������ ���� ��, ���� ������ ���� â ���ε�
        }
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

    public void SelectItem(int index)
    {
        if (slots[index].item == null) return;
        /*
        if (selectedItemWindow.activeSelf && selectedItemIndex == index)  // ������ �������� �ٽ� ������, ���� ������ â�� ��Ȱ��ȭ�ϰ� �ʱ�ȭ
        {
            ClearSelectedItemWindow();
            CloseSelectedItemWindow();

            return;
        }
        */

        // ���� ������ ���� â Ȱ��ȭ
        OpenSelectedItemWindow();

        selectedItem = slots[index].item;
        selectedItemIndex = index;

        selectedItemIcon.sprite = selectedItem.itemIcon;

        // to do: �Һ� �������� ����� ó�� �߰� ( ItemStatǥ�ÿ� UI�� ��Ȱ��ȭ�ϰ�, ������ ȿ���� ���� �������ͽ��� ǥ���ϵ��� ������ �ʿ� )
        selectedItemName.text = selectedItem.itemName;
        selectedItemDescription.text = selectedItem.itemDescription;
        selectedItemAttack.text = selectedItem.itemAttack.ToString();
        selectedItemDefense.text = selectedItem.itemDefense.ToString();
        selectedItemHealth.text = selectedItem.itemHealth.ToString();
        selectedItemCritical.text = selectedItem.itemCritical.ToString("F2") + "%";

        // ������ Ÿ�Կ� ���� ��ư Ȱ��ȭ
        useButton.SetActive(selectedItem.itemType == ItemType.Consumable);
        equipButton.SetActive(selectedItem.itemType == ItemType.Equipable && !slots[index].isEquipped);
        unEquipButton.SetActive(selectedItem.itemType == ItemType.Equipable && slots[index].isEquipped);
        dropButton.SetActive(true);
    }

    /// <summary>
    /// �Һ� ������ ��� �޼ҵ�
    /// </summary>
    public void OnUseButton()
    {

    }

    /// <summary>
    /// ���õ� ������ ������ ������ ����
    /// </summary>
    public void OnDeleteButton()
    {
        if (selectedItem == null) return;

        slots[selectedItemIndex].quantity--;

        if (slots[selectedItemIndex].isEquipped)
        {
            UnEquipItem(selectedItemIndex);  // ������ ���� ��, ������ �������� ����
        }

        if (slots[selectedItemIndex].quantity <= 0)
        {
            selectedItem = null;
            slots[selectedItemIndex].item = null;
            selectedItemIndex = -1;

            ClearSelectedItemWindow();
            CloseSelectedItemWindow();
        }

        UpdateUI();
    }

    public void OnEquipButton()
    {
        if (slots[currentEquipItemIndex].isEquipped)
        {
            UnEquipItem(currentEquipItemIndex);  // ���� ������ �������� ����
        }

        EquipItem();
    }

    public void OnUnEquipButton()
    {
        UnEquipItem(selectedItemIndex);
    }

    /// <summary>
    /// �κ��丮�� ���ο� ������ �߰��ϴ� �޼ҵ�
    /// </summary>
    /// <param name="count"></param>
    public void AddInventorySlot(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newSlot = Instantiate(slot); // ���ο� ���� �߰�

            newSlot.transform.SetParent(slotPanel, false);  // slotPanel�� �ڽ����� ����, ��ġ �ʱ�ȭ
            slots = slotPanel.GetComponentsInChildren<ItemSlot>();  // ���� ������ ������ �����Ͽ� slots �迭 ������Ʈ
        }

        UpdateSlotData();
    }

    private void UpdateSlotData()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }
    }
}
