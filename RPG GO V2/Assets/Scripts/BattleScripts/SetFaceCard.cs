using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFaceCard : MonoBehaviour
{
    private GamePlayController gameplayController;
    // The Card In The Middle of the screen
    [SerializeField] public GameChoices FaceCard = GameChoices.SWORD;

    public void setSwordFaceCard()
    {
        FaceCard = GameChoices.SWORD;
    }
    public void setShieldFaceCard()
    {
        FaceCard = GameChoices.SHIELD;
    }
    public void setMagicFaceCard()
    {
        FaceCard = GameChoices.MAGIC;
    }

    public GameChoices getFaceCard()
    {
        return FaceCard;
    }
}
