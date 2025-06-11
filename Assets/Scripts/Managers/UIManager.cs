using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    [SerializeField] private UIMainMenu mainMenu;
    [SerializeField] private UIStatus status;
    [SerializeField] private UIInventory inventory;

    public UIMainMenu MainMenu { get { return mainMenu; } }
    public UIStatus Status { get { return status; } }
    public UIInventory Inventory { get { return inventory; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMainMenu()
    {
        mainMenu.Show();
    }

    public void ShowStatusMenu()
    {
        mainMenu.HideMenuButton();
        status.Show();
    }

    public void ShowInventoryMenu()
    {
        mainMenu.HideMenuButton();
        inventory.Show();
    }
}
