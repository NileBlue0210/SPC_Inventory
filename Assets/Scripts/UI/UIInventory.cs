using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    /*
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
    */

    void Start()
    {
        
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
}
