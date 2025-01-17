using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static Action<int> OnPlayerMoneyUpdate;
    public static Action<int> OnAIMoneyUpdate;
    public static Action<int> OnMoneyPoolUpdate;
    public static Action<int> OnPlayerBetUpdate;
    public static void UpdatePlayerMoeny(int playerMoney,int amount)
    {
        playerMoney += amount;
        OnPlayerMoneyUpdate?.Invoke(playerMoney);
    }
    public static void UpdateAIMoeny(int aiMoney, int amount)
    {
        aiMoney += amount;
        OnAIMoneyUpdate?.Invoke(aiMoney);
    }
    public static void UpdateMoenyPool(int poolMoney,int amount)
    {
        poolMoney += amount;
        OnMoneyPoolUpdate?.Invoke(poolMoney);
    }
    public static void UpdatePlayerBetAmount(int playerBetMoney,int amount)
    {
        playerBetMoney+=amount;
        OnPlayerBetUpdate?.Invoke(playerBetMoney);
    }
}
