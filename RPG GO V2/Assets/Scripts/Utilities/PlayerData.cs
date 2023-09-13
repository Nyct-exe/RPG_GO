using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    private float hp;
    private float xp;
    private float requiredXp;
    private int bobHighscore;
    private float levelBase;
    private int level;
    private List<MonsterData> monsters = new List<MonsterData>();
    private bool messageShown;

    public PlayerData(Player player)
    {
        hp = player.Hp;
        xp = player.Xp;
        requiredXp = player.RequiredXp;
        bobHighscore = player.BobHighScore;
        levelBase = player.LevelBase;
        level = player.Level;
        messageShown = player.MessageShown;
        foreach (Monster monster in player.Monsters)
        {
            if (monster != null)
            {
                MonsterData md = new MonsterData(monster);
                monsters.Add(md);
            }
        }
    }

    public float Hp
    {
        get => hp;
        set => hp = value;
    }

    public float Xp
    {
        get => xp;
        set => xp = value;
    }

    public float RequiredXp
    {
        get => requiredXp;
        set => requiredXp = value;
    }

    public int BobHighscore
    {
        get => bobHighscore;
        set => bobHighscore = value;
    }

    public float LevelBase
    {
        get => levelBase;
        set => levelBase = value;
    }

    public int Level
    {
        get => level;
        set => level = value;
    }

    public List<MonsterData> Monsters
    {
        get => monsters;
        set => monsters = value;
    }

    public bool MessageShown
    {
        get => messageShown;
        set => messageShown = value;
    }
}
