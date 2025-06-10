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
    public int CurrentExp => currentExp;
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
}
