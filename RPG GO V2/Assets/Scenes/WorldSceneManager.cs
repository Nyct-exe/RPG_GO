using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class WorldSceneManager : MonsterSceneManager
{
    [SerializeField] public Text infoText;
    public override void playerPressed(GameObject player)
    {
        print("MonsterSceneManager.PlayerTapped activated");
       
    }

    public override void monsterPressed(GameObject monster)
    {
        print("MonsterSceneManager.MonsterTappedActivated");
        if (GameManager.Instance.CurrPlayer.Hp <= 1)
        {
            StartCoroutine(Wait());
        }
        else
        {
            List<GameObject> list = new List<GameObject>();
            list.Add(monster);
            SceneTransManager.Instance.SceneChange(GameConstants.SCENE_BATTLE, list);
        }
    }

    IEnumerator Wait()
    {
        infoText.text = "You're Too Tired";
        infoText.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        infoText.gameObject.SetActive(false);
    }
}
