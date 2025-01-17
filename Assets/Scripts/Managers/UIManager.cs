using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyPoolText;
    [Header("AI Content")]
    [SerializeField] private TMP_Text aiMoneyText;

    [Header("Player Content")]
    [SerializeField] private TMP_Text playerMoneyText;
    [SerializeField] private TMP_Text playerBetMoneyText;

    private void OnEnable()
    {
        EventManager.OnPlayerMoneyUpdate += UpdatePlayerMoney;
        EventManager.OnAIMoneyUpdate += UpdateAIMoney;
        EventManager.OnMoneyPoolUpdate += UpdateMoneyPool;
        EventManager.OnPlayerBetUpdate += UpdatePlayerBet;
    }
    private void UpdatePlayerMoney(int playerMoney)
    {
        playerMoneyText.text = "$" + playerMoney;
    }
    private void UpdateAIMoney(int playerMoney)
    {
        aiMoneyText.text = "$" + playerMoney;
    }
    private void UpdateMoneyPool(int amount)
    {
        moneyPoolText.text = "$" + amount;
    }
    private void UpdatePlayerBet(int amount)
    {
        playerBetMoneyText.text = "$" + amount;
    }
    private void OnDisable()
    {
        EventManager.OnPlayerMoneyUpdate -= UpdatePlayerMoney;
        EventManager.OnAIMoneyUpdate -= UpdateAIMoney;
        EventManager.OnMoneyPoolUpdate -= UpdateMoneyPool;
        EventManager.OnPlayerBetUpdate -= UpdatePlayerBet;
    }
}
