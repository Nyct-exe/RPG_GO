using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MonsterSpawner : Singleton<MonsterSpawner>
{
    [SerializeField] private Monster[] availableMonsters;

    [SerializeField] private float spawnTime = 100f;

    [SerializeField] private int monsterLimit = 4;

    [SerializeField] private float minRange = 4f;

    [SerializeField] private float maxRange = 50f;

    private List<Monster> spawnedMonsters = new List<Monster>();
    private Monster currentMonster;
    private Player player;

    public Monster CurrentMonster
    {
        get => currentMonster;
        set => currentMonster = value;
    }

    public List<Monster> SpawnedMonsters
    {
        get => spawnedMonsters;
        set => spawnedMonsters = value;
    }


    // Checks if Monsters and player are null on awake
    private void Awake()
    {
        Assert.IsNotNull(availableMonsters);
     //   Assert.IsNotNull(player);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.CurrPlayer;
        Assert.IsNotNull(player);
        
        for(int i = 0; i < monsterLimit; i++){
            InstantiateMonster();
        }

        StartCoroutine(SpawnMonsters());

    }
    //Instantiates monsters after a set time
    private IEnumerator SpawnMonsters()
    {
        while (true)
        {
            InstantiateMonster();
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void InstantiateMonster()
    {
        int i = Random.Range(0, availableMonsters.Length);
        float x = player.transform.position.x + SpawnRange();
        float z = player.transform.position.z + SpawnRange();
        float y = player.transform.position.y;
        spawnedMonsters.Add(Instantiate(availableMonsters[i], new Vector3(x, y, z), Quaternion.identity));
    }

    public void SelectedMonster(Monster monster)
    {
        currentMonster = monster;
    }

    // Creates a random distance from a player to spawn a monster
    private float SpawnRange()
    {
        float x = Random.Range(minRange, maxRange);
        if (Random.value >= 0.5)
        {
            return x;
        }
        return x * -1;
    }
}
