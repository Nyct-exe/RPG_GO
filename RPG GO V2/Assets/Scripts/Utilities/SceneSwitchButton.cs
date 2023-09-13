using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchButton : MonoBehaviour
{
    public void SwitchScenes()
    {
        Debug.Log("Switching Scenes");
        List<GameObject> list = new List<GameObject>();
        // There always only one monster in a battle scene so find object will always get what we need
        list.Add(FindObjectOfType<Monster>().gameObject);

        if (SceneManager.GetActiveScene().name == "ARBattle")
        {
            SceneTransManager.Instance.SceneChange(GameConstants.SCENE_NONAR, list);
        }
        else if(SceneManager.GetActiveScene().name == "Battle")
        {
            SceneTransManager.Instance.SceneChange(GameConstants.SCENE_BATTLE, list);   
        }
        Destroy(GameObject.FindWithTag("BattleManager").gameObject);
    }
}
