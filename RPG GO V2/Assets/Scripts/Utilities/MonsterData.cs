using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterData
{
    private string name;
    private float spawningRate;
    private int att;
    private int def;
    private float hp;
    private float maxHp;
    private int xp;

    public MonsterData(Monster monster)
    {
        name = monster.name;
        spawningRate = monster.SpawningRate;
        att = monster.Attack;
        def = monster.Defence;
        hp = monster.Health;
        maxHp = monster.MaxHp;
        xp = monster.Xp;
    }

    public string Name
    {
        get => name;
        set => name = value;
    }

    public float SpawningRate
    {
        get => spawningRate;
        set => spawningRate = value;
    }

    public int Att
    {
        get => att;
        set => att = value;
    }

    public int Def
    {
        get => def;
        set => def = value;
    }

    public float Hp
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
}
