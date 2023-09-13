using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Player : MonoBehaviour
{
    [SerializeField] private int level = 1;
    [SerializeField] private float xp = 0;
    [SerializeField] private float hp = 5;
    [SerializeField] private float requiredXp = 100;
    [SerializeField] private float levelBase = 100;
    [SerializeField] private int bobHighScore = 0;
    [SerializeField] private List<Monster> monsters = new List<Monster>();
    [SerializeField] private bool messageShown = false;
    
    private string saveLocaiton;

    public float Xp
    {
        get => xp;
        set => xp = value;
    }

    public float Hp
    {
        get => hp;
        set => hp = value;
    }

    public float RequiredXp
    {
        get => requiredXp;
        set => requiredXp = value;
    }

    public float LevelBase
    {
        get => levelBase;
        set => levelBase = value;
    }

    public List<Monster> Monsters
    {
        get => monsters;
        set => monsters = value;
    }

    public int Level
    {
        get => level;
        set => level = value;
    }

    public void addXp(float xp)
    {
        this.xp += Mathf.Max(0, xp);
        InitLevelData();
        Save();
    }
    public int BobHighScore
    {
        get => bobHighScore;
        set => bobHighScore = value;
    }

    public bool MessageShown
    {
        get => messageShown;
        set => messageShown = value;
    }

    // This is used purely for testing purposes
    public string SaveLocaiton
    {
        get => saveLocaiton;
        set => saveLocaiton = value;
    }

    public float GetMaxHealth()
    {
        return 10 * ((float) level / 2);
    }

    public void HealPlayer(float healAmount)
    {
        this.hp += healAmount;
    }

    public void addMonster(Monster monster)
    {
        bool exists = false;
        foreach (Monster m in monsters)
        {
            if (m.name == monster.name)
            {
                exists = true;
                break;
            }
        }

        if (exists == false)
        {
            monsters.Add(monster);
            monster.gameObject.SetActive(false);
            monster.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            Destroy(monster.gameObject);
            exists = false;
        }
        Save();
    }

    public void Damage(int dmg)
    {
        hp -= dmg;
        if (hp < 0)
        {
            hp = 0;
        }
        
    }

    public void InitLevelData()
    {
        int prevLevel = level;
        level = (int) Math.Floor((xp / levelBase) + 1);
        requiredXp = levelBase * level;
        
        // Increases players max hp and heals it fully after a level up
        if (level > prevLevel)
        {
            hp = 10 * ((float) level / 2);
        }
    }

    public void AddMonsterToLog(Monster monster)
    {
        monsters.Add(monster);
    }

    public void Awake()
    {
        saveLocaiton = Application.persistentDataPath + "/playerSave.dat";
    }

    public void Start()
    {
        Load();
    }

    public void UpdateBobScore(int score)
    {
       this.bobHighScore = score;
       Save();

    }

    private void Save()
    {
        // Only saves if gamemanager is present to prevent saving during testing
        if (FindObjectOfType<GameManager>())
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Create(saveLocaiton);
            PlayerData playerData = new PlayerData(this);
            binaryFormatter.Serialize(file, playerData);
            file.Close();   
        }
    }

    private void Load()
    {
        if (File.Exists(saveLocaiton))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(saveLocaiton,FileMode.Open);
            PlayerData playerData = (PlayerData) binaryFormatter.Deserialize(file);
            file.Close();
            level = playerData.Level;
            xp = playerData.Xp;
            hp = playerData.Hp;
            requiredXp = playerData.RequiredXp;
            levelBase = playerData.LevelBase;
            bobHighScore = playerData.BobHighscore;
            messageShown = playerData.MessageShown;
            
            // Importing Monsters
            foreach (MonsterData monsterData in playerData.Monsters)
            {
                GameObject gameObject = new GameObject(monsterData.Name, typeof(Monster));
                Monster monster = gameObject.GetComponent<Monster>();
                monster.SpawningRate = monsterData.SpawningRate;
                monster.Attack = monsterData.Att;
                monster.Defence = monsterData.Def;
                monster.Health = monsterData.Hp;
                monster.MaxHp = monsterData.MaxHp;
                monster.Xp = monsterData.Xp;
                monsters.Add(monster);
                gameObject.SetActive(false);
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            InitLevelData();
        }
    }
}
