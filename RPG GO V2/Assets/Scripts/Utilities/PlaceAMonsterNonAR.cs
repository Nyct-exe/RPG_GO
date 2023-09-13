using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlaceAMonsterNonAR : MonoBehaviour
{
    public GameObject monster;
    // Start is called before the first frame update
    void Start()
    {
        monster = GameObject.FindObjectOfType<Monster>().gameObject;
        Assert.IsNotNull(monster);
        monster.transform.position = new Vector3(0f, 0.3f, -5f);
        monster.transform.rotation = Quaternion.identity;
        monster.transform.LookAt(Camera.main.transform);
        monster.SetActive(true);
        monster.transform.Find("HealthCanvas").gameObject.SetActive(true);
    }
}
