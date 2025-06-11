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
    int selectedItemIndex = 0;  // 선택된 아이템 인덱스
    int currentEquipItemIndex;  // 현재 장착된 아이템 인덱스
    int usedItemSlotCount = 0;  // 사용된 아이템 슬롯 수
    [SerializeField] int maxInventorySlot;

    void Start()
    {
        controller = GameManager.Instance.Player.controller;
        condition = GameManager.Instance.Player.condition;

        CloseSelectedItemWindow();  // 선택 아이템 설명 창을 시작 시 비활성화
        Hide(); // 인벤토리 창을 시작 시 비활성화

        AddInventorySlot(maxInventorySlot);

        slots = new ItemSlot[slotPanel.childCount];

        UpdateSlotData();

        ClearSelectedItemWindow();

        remainSlotCountText.text = $"{usedItemSlotCount} / {maxInventorySlot.ToString()}";  // 남은 슬롯 수 초기화
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
        usedItemSlotCount = 0;  // 사용된 아이템 슬롯 수 초기화

        for (int i = 0; i < slots.Length; i++)  // 아이템 슬롯 수 만큼 반복
        {
            if (slots[i].item != null)   // 해당 아이템 칸이 비어있다면 해당 칸에 아이템을 배치한다
            {
                slots[i].Set();

                // 2번씩 호출되고 있다.
                usedItemSlotCount++;  // 사용된 아이템 슬롯 수 증가
            }
            else
            {
                slots[i].Clear();
            }
        }

        remainSlotCountText.text = $"{usedItemSlotCount} / {maxInventorySlot.ToString()}";  // 남은 슬롯 수 업데이트
    }

    /// <summary>
    /// 아이템 데이터를 받아 인벤토리에 추가하는 메소드
    /// </summary>
    /// <param name="data"></param>
    public void AddItem(ItemData data)
    {
        if (usedItemSlotCount >= maxInventorySlot)   // 인벤토리 슬롯이 가득 찼을 경우, 아이템 획득 실패 처리
        {
            Debug.LogWarning("인벤토리 슬롯이 가득 찼습니다. 아이템을 추가할 수 없습니다.");

            OnFailedToUpdateInventory(data);

            return;
        }

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
        if (selectedItem.itemType == ItemType.Consumable)
        {

        }
    }

    public void EquipItem()
    {
        slots[selectedItemIndex].isEquipped = true;
        currentEquipItemIndex = selectedItemIndex;  // to do : 복수 장착 시스템을 고려한 수정 필요 ( 배열으로 만들어둘 필요가 있음 )

        // 플레이어의 스탯을 아이템에 맞게 업데이트
        controller.Attack += selectedItem.itemAttack;
        controller.Defense += selectedItem.itemDefense;
        controller.MaxHealth += selectedItem.itemHealth;
        controller.Critical += selectedItem.itemCritical;

        UpdateUI();

        SelectItem(selectedItemIndex);  // 아이템 장착 후, 장착 아이템 설명 창 리로드
    }

    public void UnEquipItem(int index)
    {
        slots[index].isEquipped = false;

        // 플레이어의 스탯을 아이템에 맞게 업데이트
        controller.Attack -= slots[index].item.itemAttack;
        controller.Defense -= slots[index].item.itemDefense;
        controller.MaxHealth -= slots[index].item.itemHealth;
        controller.Critical -= slots[index].item.itemCritical;

        UpdateUI();

        if (selectedItemIndex == index)
        {
            SelectItem(selectedItemIndex);  // 아이템 장착 후, 장착 아이템 설명 창 리로드
        }
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

    public void SelectItem(int index)
    {
        if (slots[index].item == null) return;
        /*
        if (selectedItemWindow.activeSelf && selectedItemIndex == index)  // 선택한 아이템을 다시 누르면, 선택 아이템 창을 비활성화하고 초기화
        {
            ClearSelectedItemWindow();
            CloseSelectedItemWindow();

            return;
        }
        */

        // 선택 아이템 설명 창 활성화
        OpenSelectedItemWindow();

        selectedItem = slots[index].item;
        selectedItemIndex = index;

        selectedItemIcon.sprite = selectedItem.itemIcon;

        // to do: 소비 아이템일 경우의 처리 추가 ( ItemStat표시용 UI를 비활성화하고, 아이템 효과에 따른 스테이터스를 표시하도록 개수가 필요 )
        selectedItemName.text = selectedItem.itemName;
        selectedItemDescription.text = selectedItem.itemDescription;
        selectedItemAttack.text = selectedItem.itemAttack.ToString();
        selectedItemDefense.text = selectedItem.itemDefense.ToString();
        selectedItemHealth.text = selectedItem.itemHealth.ToString();
        selectedItemCritical.text = selectedItem.itemCritical.ToString("F2") + "%";

        // 아이템 타입에 따라 버튼 활성화
        useButton.SetActive(selectedItem.itemType == ItemType.Consumable);
        equipButton.SetActive(selectedItem.itemType == ItemType.Equipable && !slots[index].isEquipped);
        unEquipButton.SetActive(selectedItem.itemType == ItemType.Equipable && slots[index].isEquipped);
        dropButton.SetActive(true);
    }

    /// <summary>
    /// 소비 아이템 사용 메소드
    /// </summary>
    public void OnUseButton()
    {

    }

    /// <summary>
    /// 선택된 아이템 슬롯의 아이템 삭제
    /// </summary>
    public void OnDeleteButton()
    {
        if (selectedItem == null) return;

        slots[selectedItemIndex].quantity--;

        if (slots[selectedItemIndex].isEquipped)
        {
            UnEquipItem(selectedItemIndex);  // 아이템 삭제 시, 장착된 아이템은 해제
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
            UnEquipItem(currentEquipItemIndex);  // 현재 장착된 아이템을 해제
        }

        EquipItem();
    }

    public void OnUnEquipButton()
    {
        UnEquipItem(selectedItemIndex);
    }

    /// <summary>
    /// 인벤토리에 새로운 슬롯을 추가하는 메소드
    /// </summary>
    /// <param name="count"></param>
    public void AddInventorySlot(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newSlot = Instantiate(slot); // 새로운 슬롯 추가

            newSlot.transform.SetParent(slotPanel, false);  // slotPanel의 자식으로 설정, 위치 초기화
            slots = slotPanel.GetComponentsInChildren<ItemSlot>();  // 새로 생성된 슬롯을 포함하여 slots 배열 업데이트
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
