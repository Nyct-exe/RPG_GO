using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

public class ExperianceTest
{
    [Test]
    public void ExperianceGained()
    {
        GameObject gameObject = new GameObject();
        Player player = gameObject.AddComponent<Player>();
        player.addXp(10);
        Assert.AreEqual(10,player.Xp);
    }
    
    [Test]
    public void SuccesfulLevelUp()
    {
        GameObject gameObject = new GameObject();
        Player player = gameObject.AddComponent<Player>();
        player.addXp(100);
        Assert.AreEqual(2,player.Level);
    }
    
}
