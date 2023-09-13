using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : Singleton<GameManager>
{
    private Player currPlayer;
    public Player CurrPlayer
    {
        get
        {
            if(currPlayer == null)
            {
                currPlayer = gameObject.AddComponent<Player>();
            }

            return currPlayer;
        }
        set => currPlayer = value;

    }

    private void Update()
    {
        HealthRegeneration();
    }

    // Code responsible for passive player health regeneration
    public void HealthRegeneration()
    {
        if(FindObjectOfType<UIManager>())
            if (GameManager.Instance.currPlayer.Hp < GameManager.Instance.currPlayer.GetMaxHealth())
        {
            GameManager.Instance.currPlayer.Hp += (float)(0.1 * Time.deltaTime);
        }

        if (GameManager.Instance.currPlayer.Hp > GameManager.Instance.currPlayer.GetMaxHealth())
        {
            GameManager.Instance.currPlayer.Hp = GameManager.Instance.currPlayer.GetMaxHealth();
        }
        if (GameManager.Instance.currPlayer.Hp < 0)
        {
            GameManager.Instance.currPlayer.Hp = 0;
        }
    }
}
