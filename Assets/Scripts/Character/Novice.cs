using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Novice : Character
{
    protected override void Awake()
    {
        base.Awake();

        GameManager.Instance.Player = this;

        SetCharacter();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
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

        inventory = new List<Item>();
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
    /// �׽�Ʈ�� ������ ȹ�� �޼ҵ�
    /// </summary>
    private void GetNoviceSword()
    {
        ItemData noviceSwordData = ScriptableObject.CreateInstance<ItemData>();

        noviceSwordData.itemName = "Novice Sword";
        noviceSwordData.itemDescription = "�ʺ��ڸ� ���� �⺻ ���Դϴ�.";
        noviceSwordData.itemAttack = 10;
        noviceSwordData.itemDefense = 0;
        noviceSwordData.itemHealth = 0;
        noviceSwordData.itemCritical = 0.05f;

        Item noviceSword = new GameObject("Novice Sword").AddComponent<Item>();
        noviceSword.itemData = noviceSwordData;

        inventory.Add(noviceSword);

        Debug.Log("������ ȹ��: " + noviceSword.itemData.itemName);
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
    protected override void LevelUp()
    {
        base.LevelUp();

        // ������ �� �������ͽ� ����
        attack += 2;
        defense += 1;
        maxHealth += 10;
        health = maxHealth;
        critical += 0.05f;

        Debug.Log("������ : " + characterLevel);
    }
}
