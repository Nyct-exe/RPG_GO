using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    private AnimationController animationController;

    private GamePlayController gameplayController;
    

    private void Awake()
    {
        animationController = GetComponent<AnimationController>();
        gameplayController = GetComponent<GamePlayController>();
    }

    public void GetChoice()
    {
        string choiceName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        GameChoices selectedChoice = GameChoices.NONE;

        switch (choiceName)
        {
            case "Shield":
                selectedChoice = GameChoices.SHIELD;
                break;
            case "Magic":
                selectedChoice = GameChoices.MAGIC;
                break;
            case "Sword":
                selectedChoice = GameChoices.SWORD;
                break;
        }
        gameplayController.SetChoices(selectedChoice);
        animationController.PlayerMadeChoice();
    }
    
}
