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
}
