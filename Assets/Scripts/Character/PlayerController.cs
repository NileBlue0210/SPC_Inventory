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
    protected List<Item> inventory;
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

        inventory = new List<Item>();
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
    /// 테스트용 아이템 획득 메소드
    /// </summary>
    private void GetNoviceSword()
    {
        ItemData noviceSwordData = ScriptableObject.CreateInstance<ItemData>();

        noviceSwordData.itemName = "Novice Sword";
        noviceSwordData.itemDescription = "초보자를 위한 기본 검입니다.";
        noviceSwordData.itemAttack = 10;
        noviceSwordData.itemDefense = 0;
        noviceSwordData.itemHealth = 0;
        noviceSwordData.itemCritical = 0.05f;

        Item noviceSword = new GameObject("Novice Sword").AddComponent<Item>();
        noviceSword.itemData = noviceSwordData;

        inventory.Add(noviceSword);

        Debug.Log("아이템 획득: " + noviceSword.itemData.itemName);
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
