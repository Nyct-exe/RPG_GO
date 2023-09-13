using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BattleUI : MonoBehaviour
{
    [SerializeField] private GameObject shakeObject;

    private void ShakeSuggest()
    {
        if (GameManager.Instance.CurrPlayer.Monsters.Count < 1)
        {
            StartCoroutine(waiting());
        }
        else
        {
            shakeObject.SetActive(false);
        }
    }

    IEnumerator waiting()
    {
        shakeObject.SetActive(true);
        yield return new WaitForSeconds(5);
        shakeObject.SetActive(false);
    }

    private void Start()
    {
        Assert.IsNotNull(shakeObject);
        ShakeSuggest();
    }
}
