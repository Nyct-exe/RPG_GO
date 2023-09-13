using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

public class SavingLoading
{
    // A Test behaves as an ordinary method
    [Test]
    public void SaveTest()
    {
        // If File exists remove it
        if (System.IO.File.Exists(Application.persistentDataPath + "/playerSaveTesting.dat"))
        {
            System.IO.File.Delete(Application.persistentDataPath + "/playerSaveTesting.dat");   
        }
        
        GameObject gameObject = new GameObject();
        Player player = gameObject.AddComponent<Player>();
        player.SaveLocaiton = Application.persistentDataPath + "/playerSaveTesting.dat";
        // This is needed to enable saving
        gameObject.AddComponent<GameManager>();
        // Adding experiance triggers a save.
        player.addXp(20);
        Assert.IsTrue(System.IO.File.Exists(Application.persistentDataPath+"/playerSaveTesting.dat"));

    }
    [Test]
    public void LoadTest()
    {
        // If File exists remove it
        if (System.IO.File.Exists(Application.persistentDataPath + "/playerSaveTesting.dat"))
        {
            System.IO.File.Delete(Application.persistentDataPath + "/playerSaveTesting.dat");   
        }

        GameObject gameObject = new GameObject();
        Player player = gameObject.AddComponent<Player>();
        player.SaveLocaiton = Application.persistentDataPath + "/playerSaveTesting.dat";
        // This is needed to enable saving
        gameObject.AddComponent<GameManager>();
        // Save by adding XP
        player.addXp(20);
        // Get rid of old player to avoid accidentally checking old data
        GameObject.Destroy(player);
        // Create a new player
        Player newPlayer = new GameObject().AddComponent<Player>();
        newPlayer.SaveLocaiton = Application.persistentDataPath + "/playerSaveTesting.dat";
        // Initiate Loading
        newPlayer.Start();
        Assert.AreEqual(newPlayer.Xp,20);
    }
}
