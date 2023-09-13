using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPot : MonoBehaviour
{

    [SerializeField] private float healAmount = 0.5f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.CurrPlayer.HealPlayer(healAmount);
            Debug.Log("Player is in the healing pot");
        }
    }
}
