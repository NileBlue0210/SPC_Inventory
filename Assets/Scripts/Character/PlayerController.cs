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
    public int Attack { get => attack; set => attack = value; }
    public int Defense { get => defense; set => defense = value; }

    public int Health => health;
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }

    public float Critical { get => critical; set => critical = value; }

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
        characterDescription = "이제 막 모험을 시작한 풋내기 모험가입니다.";

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
    /// 테스트용 골드 획득 메소드
    /// </summary>
    private void GetGold()
    {
        gold += 1000;

        Debug.Log("골드 획득: " + gold);
    }

    [Button]
    /// <summary>
    /// 아이템 획득 메소드
    /// </summary>
    private void GetItem()
    {
        // 테스트용 아이템 생성 로직
        GameObject woodSword = Resources.Load<GameObject>("ItemDictionary/WoodSword");
        ItemData woodSwordData;

        if (woodSword == null)
        {
            Debug.LogError("아이템을 로드할 수 없습니다. 경로를 확인하세요.");

            return;
        }
        else
        {
            woodSwordData = woodSword.GetComponent<Item>().itemData;

            if (woodSwordData != null)
            {
                inventory.Add(woodSwordData);

                Debug.Log("아이템 획득: " + woodSwordData.itemName);

                // UIManager를 통해 인벤토리 UI 갱신
                UIManager.Instance.Inventory.AddItem(woodSwordData);
            }
        }
    }

    [Button]
    /// <summary>
    /// 테스트용 스테이터스 증가 메소드
    /// </summary>
    private void IncreaseStatus()
    {
        attack += 2;
        defense += 1;
        maxHealth += 10;
        health = maxHealth;
        critical += 0.05f;
        CurrentExp += 10;

        Debug.Log("전 스테이터스 증가: 공격력 " + attack + ", 방어력 " + defense + ", 체력 " + health + ", 크리티컬 확률 " + critical);
    }

    [Button]
    private void LevelUp()
    {
        characterLevel++;

        // 레벨업에 따른 경험치 요구량 증가 및 경험치 초기화
        currentExp = 0;
        maxExp *= characterLevel;

        // 레벨업에 따른 스테이터스 증가
        attack += 2;
        defense += 1;
        maxHealth += 10;
        health = maxHealth;
        critical += 0.05f;

        Debug.Log("레벨업 : " + characterLevel);
    }
}
