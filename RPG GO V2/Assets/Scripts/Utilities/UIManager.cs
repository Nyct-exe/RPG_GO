using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text lvl;
    [SerializeField] private GameObject monsterLog;
    [SerializeField] private Image hpBar;
    [SerializeField] private Image xpBar;
    [SerializeField] private Image log;
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private Image FirstTimeMessage;
    [SerializeField] private Text infoText;

    private void Awake()
    {
        Assert.IsNotNull(hpBar);
        Assert.IsNotNull(lvl);
        Assert.IsNotNull(monsterLog);
        Assert.IsNotNull(FirstTimeMessage);
    }

    /* Checks if the player has seen the starting message
        Once the Graphical User Interface Loads,
        Changed from onStart since OnStart was too fast.
     */
    private void OnGUI()
    {
        if (GameManager.Instance.CurrPlayer.MessageShown == false)
        {
            FirstTimeMessage.gameObject.SetActive(true);
            GameManager.Instance.CurrPlayer.MessageShown = true;
        }
    }

    private void Update()
    {
        UpdateLevel();
        UpdateHealthBar();
        UpdateXp();
        UpdateMonsterLog();
        UpdateBobScoreText();
    }

    public void UpdateLevel()
    {
        lvl.text = GameManager.Instance.CurrPlayer.Level.ToString();
    }

    public void UpdateHealthBar()
    {
        float maxHealth = GameManager.Instance.CurrPlayer.GetMaxHealth();
        var rectTransformRect = hpBar.rectTransform.rect;
        rectTransformRect.width = maxHealth * 10;
        hpBar.fillAmount = GameManager.Instance.CurrPlayer.Hp/maxHealth;

    }

    public void UpdateXp()
    {
        var rectTransformRect = xpBar.rectTransform.rect;
        rectTransformRect.width = GameManager.Instance.CurrPlayer.LevelBase * 10;
        xpBar.fillAmount = GameManager.Instance.CurrPlayer.Xp / GameManager.Instance.CurrPlayer.RequiredXp;

    }

    public void ShowMonsterLog()
    {
        // Shows The Log if its not active and turns of on click
        if (log.gameObject.activeSelf)
        {
            log.gameObject.SetActive(false);
        }
        else
        {
            log.gameObject.SetActive(true);
        }

    }

    public void UpdateMonsterLog()
    {
        List<Monster> monsterList = GameManager.Instance.CurrPlayer.Monsters;
        if (monsterList.Count > 0)
        {
            foreach (Monster m in monsterList)
            {
                switch (m.name)
                {
                    case "SlimePBR(Clone)":
                        grid.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                        grid.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                        break;
                    case "TurtleShellPBR(Clone)":
                        grid.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
                        grid.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                        break;
                    case "DogPBR(Clone)":
                        grid.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
                        grid.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
                        break;
                    case "ChestMonsterPBR(Clone)":
                        grid.transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
                        grid.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
                        break;
                    case "BeholderPBR(Clone)":
                        grid.transform.GetChild(4).GetChild(0).gameObject.SetActive(false);
                        grid.transform.GetChild(4).GetChild(1).gameObject.SetActive(true);
                        break;
                    case "Free Burrow(Clone)":
                        grid.transform.GetChild(5).GetChild(0).gameObject.SetActive(false);
                        grid.transform.GetChild(5).GetChild(1).gameObject.SetActive(true);
                        break;
                    case "Toon Chicken(Clone)":
                        grid.transform.GetChild(6).GetChild(0).gameObject.SetActive(false);
                        grid.transform.GetChild(6).GetChild(1).gameObject.SetActive(true);
                        break;
                }
            }
        }
    }

    public void UpdateBobScoreText(){
            // Bob Score
            grid.transform.GetChild(7).GetChild(2).GetComponent<Text>().text = GameManager.Instance.CurrPlayer.BobHighScore.ToString();
    }

    // Disables the Panel text
    public void RemovePanel()
    {
        FirstTimeMessage.gameObject.SetActive(false);
    }
    
    public IEnumerator DisplayInfoText(string text)
    {
        infoText.text = text;
        infoText.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        infoText.gameObject.SetActive(false);
    }

}
