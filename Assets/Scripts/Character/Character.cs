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

    [Header("character stats")]
    protected int attack;
    protected int defense;
    protected int health;
    protected int maxHealth;
    protected float critical;

    [Header("character property")]
    protected List<Item> inventory;
    protected int gold;

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
