using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Assert = UnityEngine.Assertions.Assert;

public class RockPaperScissorsTest
{
   
    // A Test behaves as an ordinary method
    [Test]
    public void PlayerWins()
    {
        GameObject gameObject = new GameObject();
        GamePlayController gamePlayController = gameObject.AddComponent<GamePlayController>();
        gamePlayController.testing = true;
        GameManager gameManager = gameObject.AddComponent<GameManager>();
        gameManager.CurrPlayer = new GameObject().AddComponent<Player>();
        gamePlayController.monster = new GameObject().AddComponent<Monster>();
        gamePlayController.infoText = new GameObject().AddComponent<Text>();
        Assert.AreEqual(gamePlayController.DetermineWinner(GameChoices.SWORD, GameChoices.MAGIC),1);
    }
    [Test]
    public void PlayerLoses()
    {
        GameObject gameObject = new GameObject();
        GamePlayController gamePlayController = gameObject.AddComponent<GamePlayController>();
        gamePlayController.testing = true;
        GameManager gameManager = gameObject.AddComponent<GameManager>();
        gameManager.CurrPlayer = new GameObject().AddComponent<Player>();
        gamePlayController.monster = new GameObject().AddComponent<Monster>();
        gamePlayController.infoText = new GameObject().AddComponent<Text>();
        Assert.AreEqual(gamePlayController.DetermineWinner(GameChoices.SWORD, GameChoices.SHIELD),-1);
    }
    [Test]
    public void Draw()
    {
        GameObject gameObject = new GameObject();
        GamePlayController gamePlayController = gameObject.AddComponent<GamePlayController>();
        gamePlayController.testing = true;
        GameManager gameManager = gameObject.AddComponent<GameManager>();
        gameManager.CurrPlayer = new GameObject().AddComponent<Player>();
        gamePlayController.monster = new GameObject().AddComponent<Monster>();
        gamePlayController.infoText = new GameObject().AddComponent<Text>();
        Assert.AreEqual(gamePlayController.DetermineWinner(GameChoices.SWORD, GameChoices.SWORD),0);
    }

   
}
