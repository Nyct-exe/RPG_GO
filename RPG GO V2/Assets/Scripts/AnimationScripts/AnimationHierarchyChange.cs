using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Not Used because it does not work as intended
// Kept for future refrence
public class AnimationHierarchyChange : MonoBehaviour
{ 
    GameObject Shield, Sword, Magic;

   void Awake()
   {
      Shield = GameObject.Find("Shield");
      Sword = GameObject.Find("Sword");
      Magic = GameObject.Find("Magic");
   }

   public void SwitchParentToShield()
   {
       
       Sword.transform.SetParent(Shield.transform);
   }
   public void SwitchParentToMagic()
   {
       Sword.transform.SetAsFirstSibling();
   }

   public void SwitchParentToSword()
   {
       //Shield.transform.SetParent(Sword.transform);
       Shield.transform.SetAsFirstSibling();
   }
}
