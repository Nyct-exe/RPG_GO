using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSceneManager : MonsterSceneManager
{
    public override void playerPressed(GameObject player)
    {
        print("MonsterSceneManager.PlayerTapped activated");
       
    }

    public override void monsterPressed(GameObject monster)
    {
        print("MonsterSceneManager.MonsterTappedActivated");
        SceneManager.LoadScene(GameConstants.SCENE_BATTLE, LoadSceneMode.Additive);
    }
}
