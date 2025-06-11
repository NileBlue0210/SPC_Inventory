using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("character info")]
    protected string characterName;
    protected int characterLevel;
    protected int currentExp;
    protected int maxExp;
    protected string characterDescription;
    public string CharacterName => characterName;
    public int CharacterLevel => characterLevel;
    public int CurrentExp
    {
        get => currentExp;
        set
        {
            currentExp = value;

            if (currentExp >= maxExp)
            {
                LevelUp();
            }
        }
    }
    public int MaxExp => maxExp;
    public string CharacterDescription => characterDescription;

    [Header("character stats")]
    protected int attack;
    protected int defense;
    protected int health;
    protected int maxHealth;
    protected float critical;
    public int Attack => attack;
    public int Defense => defense;
    public int Health => health;
    public int MaxHealth => maxHealth;
    public float Critical => critical;

    [Header("character property")]
    protected List<ItemData> inventory;
    protected int gold;
    public int Gold => gold;

    private void Awake()
    {
        SetCharacter();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void SetCharacter()
    {
        characterName = "Novice";
        characterLevel = 1;
        currentExp = 0;
        maxExp = 100;
        characterDescription = "���� �� ������ ������ ǲ���� ���谡�Դϴ�.";

        attack = 5;
        defense = 3;
        maxHealth = 100;
        health = maxHealth;
        critical = 15f;
        gold = 0;

        inventory = new List<ItemData>();
    }

    [Button]
    /// <summary>
    /// �׽�Ʈ�� ��� ȹ�� �޼ҵ�
    /// </summary>
    private void GetGold()
    {
        gold += 1000;

        Debug.Log("��� ȹ��: " + gold);
    }

    [Button]
    /// <summary>
    /// ������ ȹ�� �޼ҵ�
    /// </summary>
    private void GetItem()
    {
        // �׽�Ʈ�� ������ ���� ����
        ItemData noviceSwordData = ScriptableObject.CreateInstance<ItemData>();

        noviceSwordData.itemName = "Novice Sword";
        noviceSwordData.itemDescription = "�ʺ��ڸ� ���� �⺻ ���Դϴ�.";
        noviceSwordData.itemAttack = 10;
        noviceSwordData.itemDefense = 0;
        noviceSwordData.itemHealth = 0;
        noviceSwordData.itemCritical = 0.05f;
        noviceSwordData.itemType = ItemType.Weapon;
        noviceSwordData.itemIcon = null;
        noviceSwordData.CanStack = false;
        noviceSwordData.maxStack = 1;

        inventory.Add(noviceSwordData);

        Debug.Log("������ ȹ��: " + noviceSwordData.itemName);

        // UIManager�� ���� �κ��丮 UI ����
        UIManager.Instance.Inventory.AddItem(noviceSwordData);
    }

    [Button]
    /// <summary>
    /// �׽�Ʈ�� �������ͽ� ���� �޼ҵ�
    /// </summary>
    private void IncreaseStatus()
    {
        attack += 2;
        defense += 1;
        maxHealth += 10;
        health = maxHealth;
        critical += 0.05f;
        CurrentExp += 10;

        Debug.Log("�� �������ͽ� ����: ���ݷ� " + attack + ", ���� " + defense + ", ü�� " + health + ", ũ��Ƽ�� Ȯ�� " + critical);
    }

    [Button]
    private void LevelUp()
    {
        characterLevel++;

        // �������� ���� ����ġ �䱸�� ���� �� ����ġ �ʱ�ȭ
        currentExp = 0;
        maxExp *= characterLevel;

        // �������� ���� �������ͽ� ����
        attack += 2;
        defense += 1;
        maxHealth += 10;
        health = maxHealth;
        critical += 0.05f;

        Debug.Log("������ : " + characterLevel);
    }
}
