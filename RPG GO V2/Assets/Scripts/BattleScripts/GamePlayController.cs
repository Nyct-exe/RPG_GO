using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;
using Random = System.Random;

public enum GameChoices
{
    SWORD,
    SHIELD,
    MAGIC,
    NONE
}

public class GamePlayController : MonoBehaviour
{
    [SerializeField] private Sprite sword_Sprite, shield_Sprite, magic_Sprite;

    [SerializeField] private Image playerChoice_Img, opponentChoice_Img;

    [SerializeField] public Text infoText;

    [SerializeField] private Image playerHealth;

    [SerializeField] private Text scoreText;

    private Image monsterHealh;

    private GameChoices player_choice = GameChoices.NONE, opponent_choice = GameChoices.NONE;

    private AnimationController animationController;

    public Monster monster;

    private int currentStreak = 0;

    public Monster defaultMonster;

    private bool draw = false;

    public bool testing = false;

    private void Awake()
    {
        // Aditional and operator was needed since on creation this is activated and testing has not been set to true yet
        if (testing == false && playerHealth != null)
        {
            // If there is no monster instantiates a default - BOB
            if (!FindObjectOfType<Monster>())
            {
                Instantiate(defaultMonster, new Vector3(2.0f, 0, 0), Quaternion.identity);
                monster = defaultMonster;
            }
            else
            {
                monster = FindObjectOfType<Monster>();
            }
            animationController = GetComponent<AnimationController>();
            Assert.IsNotNull(monster);
            Assert.IsNotNull(animationController);
            float maxHealth = GameManager.Instance.CurrPlayer.GetMaxHealth();
            playerHealth.fillAmount = GameManager.Instance.CurrPlayer.Hp/maxHealth;
            monsterHealh = monster.transform.Find("HealthCanvas").GetChild(0).GetChild(0).GetComponent<Image>(); 
            // Bob Check
            if (monster.CompareTag("Bob"))
            {
                scoreText.gameObject.SetActive(true);
            }
        }
    }

    public void SetChoices(GameChoices choice)
    {
        switch (choice)
        {
            case GameChoices.SHIELD:

                playerChoice_Img.sprite = shield_Sprite;
                player_choice = GameChoices.SHIELD;
                break;
            
            case GameChoices.MAGIC:
                playerChoice_Img.sprite = magic_Sprite;
                player_choice = GameChoices.MAGIC;
                break;
            
            case GameChoices.SWORD:
                playerChoice_Img.sprite = sword_Sprite;
                player_choice = GameChoices.SWORD;
                break;
        }

        SetOpponentChoice();

        // In Order to reduce chance of a draw and make fights less of a drag on every to be draw
        // it has a 50% of redrawing the card
        if (opponent_choice == player_choice)
        {
            Random random = new Random();
            if (random.NextDouble() < 0.5)
            {
                SetOpponentChoice();
            }
        }

        DetermineWinner(player_choice, opponent_choice);
        
        
    }

    public void SetOpponentChoice()
    {
        Array choices = Enum.GetValues(typeof(GameChoices));
        Random random = new Random();
        GameChoices randomChoice = (GameChoices) choices.GetValue(random.Next(choices.Length-1));
        
        switch (randomChoice)
        {
            case GameChoices.SHIELD:

                opponentChoice_Img.sprite = shield_Sprite;
                opponent_choice = GameChoices.SHIELD;
                break;
            
            case GameChoices.MAGIC:
                opponentChoice_Img.sprite = magic_Sprite;
                opponent_choice = GameChoices.MAGIC;
                break;
            
            case GameChoices.SWORD:
                opponentChoice_Img.sprite = sword_Sprite;
                opponent_choice = GameChoices.SWORD;
                break;
        }
        
        
    }
    // Returns an integer purely for testing purposes
    public int DetermineWinner(GameChoices player_choice, GameChoices opponent_choice)
    {
        if (player_choice == opponent_choice)
        {
            // Player TIE       
            infoText.text = "It's a Draw!";
            draw = true;
            // This Ensures that during testing it does not do unnecessary coroutines
            if (testing == false)
            {
                StartCoroutine(DisplayWinnerAndRestart());   
            }
            return 0;
        }
        else if (player_choice == GameChoices.SHIELD && opponent_choice == GameChoices.SWORD)
        {
            // Player Wins
            if ((monster.Defence - (2 * GameManager.Instance.CurrPlayer.Level)) <= 0)
            {
                infoText.text = "Successful hit!";
                monster.Damage(Math.Abs(monster.Defence - (2 * GameManager.Instance.CurrPlayer.Level)));
            }
            else
            {
                infoText.text = "Monsters Defence Is Too Strong!"; 
                monster.Damage(1);
            }
            draw = false;
            // This Ensures that during testing it does not do unnecessary coroutines
            if (testing == false)
            {
                StartCoroutine(DisplayWinnerAndRestart());   
            }
            return 1;
        }
        
        else if (player_choice == GameChoices.MAGIC && opponent_choice == GameChoices.SWORD)
        {
            // Player Loses
            if (((GameManager.Instance.CurrPlayer.Level - monster.Attack) == GameManager.Instance.CurrPlayer.Level)
                || ((GameManager.Instance.CurrPlayer.Level - monster.Attack) >= 0))
            {
                infoText.text = "Monster Barely Made a Scratch";
                GameManager.Instance.CurrPlayer.Hp -= 1;
            }
            else
            {
                infoText.text = "You Got HIT!";
                GameManager.Instance.CurrPlayer.Damage(Math.Abs(GameManager.Instance.CurrPlayer.Level - monster.Attack));     
            }
            draw = false;
            // This Ensures that during testing it does not do unnecessary coroutines
            if (testing == false)
            {
                StartCoroutine(DisplayWinnerAndRestart());   
            }
            return -1;
        }

        else if (player_choice == GameChoices.SHIELD && opponent_choice == GameChoices.MAGIC)
        {
            // Player Loses
            if (((GameManager.Instance.CurrPlayer.Level - monster.Attack) == GameManager.Instance.CurrPlayer.Level)
                || ((GameManager.Instance.CurrPlayer.Level - monster.Attack) >= 0))
            {
                infoText.text = "Monster Barely Made a Scratch";
                GameManager.Instance.CurrPlayer.Hp -= 1;
            }
            else
            {
                infoText.text = "You Got HIT!";
                GameManager.Instance.CurrPlayer.Damage(Math.Abs(GameManager.Instance.CurrPlayer.Level - monster.Attack));   
            }
            draw = false;
            // This Ensures that during testing it does not do unnecessary coroutines
            if (testing == false)
            {
                StartCoroutine(DisplayWinnerAndRestart());   
            }
            return -1;
        }
        else if (player_choice == GameChoices.SWORD && opponent_choice == GameChoices.MAGIC)
        {
            // Player Wins   
            if ((monster.Defence - (2 * GameManager.Instance.CurrPlayer.Level)) <= 0)
            {
                infoText.text = "Successful hit!";
                monster.Damage(Math.Abs(monster.Defence - (2 * GameManager.Instance.CurrPlayer.Level)));  
            }
            else
            {
                infoText.text = "Monsters Defence Is Too Strong!";  
                monster.Damage(1);
            }
            draw = false;
            // This Ensures that during testing it does not do unnecessary coroutines
            if (testing == false)
            {
                StartCoroutine(DisplayWinnerAndRestart());   
            }
            return 1;
        }
        
        else if (player_choice == GameChoices.MAGIC && opponent_choice == GameChoices.SHIELD)
        {
            // Player Wins   
            if ((monster.Defence - (2 * GameManager.Instance.CurrPlayer.Level)) <= 0)
            {
                infoText.text = "Successful hit!";
                monster.Damage(Math.Abs(monster.Defence - (2 * GameManager.Instance.CurrPlayer.Level)));  
            }
            else
            {
                infoText.text = "Monsters Defence Is Too Strong!";
                monster.Damage(1);
            }
            draw = false;
            // This Ensures that during testing it does not do unnecessary coroutines
            if (testing == false)
            {
                StartCoroutine(DisplayWinnerAndRestart());   
            }
            return 1;
        }
        else if (player_choice == GameChoices.SWORD && opponent_choice == GameChoices.SHIELD)
        {
            // Player Loses
            if (((GameManager.Instance.CurrPlayer.Level - monster.Attack) == GameManager.Instance.CurrPlayer.Level)
                || ((GameManager.Instance.CurrPlayer.Level - monster.Attack) >= 0))
            {
                infoText.text = "Monster Barely Made a Scratch";
                GameManager.Instance.CurrPlayer.Hp -= 1;
            }
            else
            {
                infoText.text = "You Got HIT!";
                GameManager.Instance.CurrPlayer.Damage(Math.Abs(GameManager.Instance.CurrPlayer.Level - monster.Attack));    
            }
            draw = false;
            // This Ensures that during testing it does not do unnecessary coroutines
            if (testing == false)
            {
                StartCoroutine(DisplayWinnerAndRestart());   
            }
            return -1;
        }

        IEnumerator DisplayWinnerAndRestart()
        {
            yield return new WaitForSeconds(2f);
            infoText.gameObject.SetActive(true);
            
            yield return new WaitForSeconds(2f);
            infoText.gameObject.SetActive(false);
            
            animationController.ResetAnimation();

            // Update Health
            float maxHealth = GameManager.Instance.CurrPlayer.GetMaxHealth();
            playerHealth.fillAmount = GameManager.Instance.CurrPlayer.Hp/maxHealth;
            monsterHealh.fillAmount = monster.HealthPercent();
            // Normal Fight
            if (!monster.CompareTag("Bob"))
            {
                // Player Wins
                if (monster.Health <= 0)
                {
                    GameManager.Instance.CurrPlayer.addXp(monster.Xp);
                    GameManager.Instance.CurrPlayer.addMonster(monster);
                    FinishGame();
                
                }
                // Player Loses
                if (GameManager.Instance.CurrPlayer.Hp <= 1)
                {
                    FinishGame();
                    Destroy(monster.gameObject);
                }   
            }
            // BOB FIGHT
            else
            {
                if (GameManager.Instance.CurrPlayer.Hp < 1)
                {
                    infoText.text = "You're TOO Weak!";
                    yield return new WaitForSeconds(2f);
                    infoText.gameObject.SetActive(true);
            
                    yield return new WaitForSeconds(2f);
                    infoText.gameObject.SetActive(false);

                }
                
                if (!maxHealth.Equals(GameManager.Instance.CurrPlayer.Hp))
                {
                    currentStreak = 0;
                    scoreText.transform.GetChild(0).GetComponent<Text>().text = currentStreak.ToString();
                    GameManager.Instance.CurrPlayer.HealPlayer(maxHealth);
                    playerHealth.fillAmount = GameManager.Instance.CurrPlayer.Hp/maxHealth;
                }
                else if (maxHealth.Equals(GameManager.Instance.CurrPlayer.Hp) && draw == false)
                {
                    currentStreak += 1;
                    scoreText.transform.GetChild(0).GetComponent<Text>().text = currentStreak.ToString();
                }

                if (currentStreak > GameManager.Instance.CurrPlayer.BobHighScore)
                {
                    infoText.text = "NEW HIGHSCORE: " + currentStreak.ToString();
                    GameManager.Instance.CurrPlayer.UpdateBobScore(currentStreak);
                    
                    yield return new WaitForSeconds(2f);
                    infoText.gameObject.SetActive(true);
            
                    yield return new WaitForSeconds(2f);
                    infoText.gameObject.SetActive(false);
                }
                
            }

        }

        return 200;

    }

    public void FinishGame()
    {
        // If player decides to leave with a highscore it still saves
        if (monster.CompareTag("Bob") && currentStreak > GameManager.Instance.CurrPlayer.BobHighScore)
        {
            GameManager.Instance.CurrPlayer.UpdateBobScore(currentStreak);
        }

        if (monster.CompareTag("Bob"))
        {
            // Heal player after they leave the bob minigame
            GameManager.Instance.CurrPlayer.HealPlayer(GameManager.Instance.CurrPlayer.GetMaxHealth());
        }
        // If player presses run this if makes sure to destroy the object
        if (monster.Health > 0 || monster.CompareTag("Bob"))
        {
            Destroy(monster.gameObject);
        }
        //go back to the main scene
        SceneTransManager.Instance.SceneChange(GameConstants.SCENE_WORLD, new List<GameObject>());
        Destroy(GameObject.FindWithTag("BattleManager").gameObject);
        
    }

}
