using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("character info")]
    protected string characterName;
    protected int characterLevel;
    protected int currentExp;
    protected int maxExp;
    protected string characterDescription;
    public string CharacterName => characterName;
    public int CharacterLevel => characterLevel;
    public int CurrentExp {
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

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void LevelUp()
    {
        characterLevel++;

        // 레벨업에 따른 경험치 요구량 증가 및 경험치 초기화
        currentExp = 0;
        maxExp *= characterLevel;
    }
}
