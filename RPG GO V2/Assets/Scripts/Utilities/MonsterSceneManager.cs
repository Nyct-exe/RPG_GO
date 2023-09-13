using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterSceneManager : MonoBehaviour
{
   public abstract void playerPressed(GameObject player);
   public abstract void monsterPressed(GameObject monster);
}
