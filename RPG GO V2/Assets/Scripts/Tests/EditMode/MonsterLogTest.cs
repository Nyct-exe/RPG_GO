using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class MonsterLogTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void AddingAMonsterTest()
    {
        GameObject gameObjectPlayer = new GameObject();
        GameObject gameObjectMonster = new GameObject();
        Player player = gameObjectPlayer.AddComponent<Player>();
        Monster monster = gameObjectMonster.AddComponent<Monster>();
        // Dummy data to negate the search of the health bar
        new GameObject().transform.SetParent(monster.transform);
        new GameObject().transform.SetParent(monster.transform);
        new GameObject().transform.SetParent(monster.transform);
        player.addMonster(monster);
        Assert.IsNotEmpty(player.Monsters);
    }
    [Test]
    public void CheckingForCorrectAddedMonster()
    {
        GameObject gameObjectPlayer = new GameObject();
        GameObject gameObjectMonster = new GameObject();
        Player player = gameObjectPlayer.AddComponent<Player>();
        Monster monster = gameObjectMonster.AddComponent<Monster>();
        monster.name = "TestMonster";
        // Dummy data to negate the search of the health bar
        new GameObject().transform.SetParent(monster.transform);
        new GameObject().transform.SetParent(monster.transform);
        new GameObject().transform.SetParent(monster.transform);
        player.addMonster(monster);
        Assert.AreEqual(monster,player.Monsters[0]);
    }

    [Test]
    public void IncreasingBobsHighscore()
    {
        GameObject gameObject = new GameObject();
        Player player = gameObject.AddComponent<Player>();
        player.UpdateBobScore(1);
        Assert.AreEqual(1,player.BobHighScore);

    }
}
