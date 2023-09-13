using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnableClickOnMonsters : MonoBehaviour
{
    /*
     * These Triggers ensure that players have to actually approach the monsters
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster") || other.CompareTag("Bob"))
        {
            Monster monster = other.gameObject.GetComponent<Monster>();
            monster.clickable = true;
            Debug.Log("The player is within a radius of the: " + monster.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster") || other.CompareTag("Bob"))
        {
            Monster monster = other.gameObject.GetComponent<Monster>();
            monster.clickable = true;
            Debug.Log("The player has exited the range of the: " + monster.name);
        }
    }
}
