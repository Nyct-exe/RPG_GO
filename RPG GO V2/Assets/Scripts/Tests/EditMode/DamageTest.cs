using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

public class DamageTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void PlayerTakesDamage()
    {
        GameObject gameObject = new GameObject();
        Player player = gameObject.AddComponent<Player>();
        player.Damage(1);
        Assert.AreEqual(4,player.Hp);
    }
    // A Test behaves as an ordinary method
    [Test]
    public void MonsterTakesDamage()
    {
        GameObject gameObject = new GameObject();
        Monster monster = gameObject.AddComponent<Monster>();
        monster.Damage(1);
        Assert.AreEqual(19,monster.Health);
        
    }
    
}
