using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    [SerializeField] private float spawningRate = 0.2f;

    [SerializeField] private int att = 0;

    [SerializeField] private int def = 0;

    [SerializeField] private float hp = 20;

    [SerializeField] private float maxHp = 20;

    [SerializeField] private int xp;

    public bool clickable = false;

    public float SpawningRate
    {
        get => spawningRate;
        set => spawningRate = value;
    }

    public int Attack
    {
        get => att;
        set => att = value;
    }

    public int Defence
    {
        get => def;
        set => def = value;
    }

    public float Health
    {
        get => hp;
        set => hp = value;
    }

    public float MaxHp
    {
        get => maxHp;
        set => maxHp = value;
    }

    public int Xp
    {
        get => xp;
        set => xp = value;
    }

    public bool Clickable
    {
        get => clickable;
        set => clickable = value;
    }

    private void OnMouseDown()
    {
        if (clickable == true)
        {
            MonsterSceneManager[] managers = FindObjectsOfType<MonsterSceneManager>();
            foreach (MonsterSceneManager monsterSceneManager in managers)
            {
                if (monsterSceneManager.gameObject.activeSelf)
                {
                    monsterSceneManager.monsterPressed(this.gameObject);
                }
            }   
        }
        else
        {
            UIManager uiManager = FindObjectOfType<UIManager>();
            Assert.IsNotNull(uiManager);
            StartCoroutine(uiManager.DisplayInfoText("You're Too Far"));
        }
    }

    public void Damage(int dmg)
    {
        hp -= dmg;
        if (hp < 0)
        {
            hp = 0;
        }
    }

    public float HealthPercent()
    {
        return hp / maxHp;
    }
}
