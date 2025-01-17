using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Global")]
    [SerializeField] private Card cardPrefab;
    [SerializeField] private int startMoney = 1000;
    [SerializeField] private int anteMoney = 10;
    private List<string> Deck=new();
    private int moneyPool;

    [Header("AI Content")]
    [SerializeField] private Transform[] aiCardSlots;
    private List<Card> aiCards = new List<Card>();
    private List<string> AIHand = new();
    private int aiMoney;
    private int aiBetMoney;

    [Header("Player Content")]
    [SerializeField] private Transform[] playerCardSlots;
    private List<Card> playerCards = new List<Card>();
    private List<string> PlayerHand=new();
    private int playerMoney;
    private int playerBetMoney;

    private GameStates gameState;

    private void Start()
    {
        SetStartMoney();
        StartCoroutine(TimeBetweenStates(GameStates.ROUND_START));
    }
    private void InitilizeDeck()
    {
        Deck.Clear();
        AIHand.Clear();
        PlayerHand.Clear();
        foreach (var suit in GameConstantData.Suits)
        {
            foreach (var rank in GameConstantData.Ranks)
            {
                Deck.Add($"{rank} of {suit}");
            }
        }
    }
    private void ShuffleDeck()
    {
        for (int i = Deck.Count - 1; i > 0; i--)
        {
            int randomNum = Random.Range(0, i + 1);
            string temp = Deck[i];
            Deck[i] = Deck[randomNum];
            Deck[randomNum] = temp;
        }
    }
    private void HandOutCards()
    {
        for (int i = 0; i < 5; i++)
        {
            PlayerHand.Add(Deck[0]);
            Deck.RemoveAt(0);

            AIHand.Add(Deck[0]);
            Deck.RemoveAt(0);
        }
    }
    private void CreateCardVisual()
    {
        ClearCardList(playerCards);
        ClearCardList(aiCards);
        InstantiateAndSetCardData(AIHand,aiCardSlots,aiCards,true);
        InstantiateAndSetCardData(PlayerHand,playerCardSlots,playerCards,false);
    }

    private void InstantiateAndSetCardData(List<string> handList, Transform[] cardSlots, List<Card>cardList, bool isAIcard)
    {
        for (int i = 0; i < handList.Count; i++)
        {
            Card newCard = Instantiate(cardPrefab, cardSlots[i], false);
            string[] cardNameParts = handList[i].Split(' ');
            newCard.SetCardData(cardNameParts[2], cardNameParts[0], isAIcard);
            cardList.Add(newCard);
        }
    }

    private void ClearCardList(List<Card> cardList)
    {
        foreach (var card in cardList)
        {
            Destroy(card.gameObject);
        }
        cardList.Clear();
    }
    private void SetStartMoney()
    {
        aiMoney = startMoney;
        playerMoney = startMoney;
        EventManager.UpdateAIMoeny(aiMoney,0);
        EventManager.UpdatePlayerMoeny(playerMoney, 0);
        EventManager.UpdateMoenyPool(moneyPool,0);
        EventManager.UpdatePlayerBetAmount(playerBetMoney,0);
    }

   //-------------------------------------------------------------------//
    private void ChangeGameState(GameStates _gameState)
    {
        gameState = _gameState;
        switch (gameState)
        {
            case GameStates.ROUND_START:
                //clean up the game field/cards
                ClearCardList(aiCards);
                ClearCardList(playerCards);
                StartCoroutine(TimeBetweenStates(GameStates.ANTE));
                break;

            case GameStates.ANTE:
                //pay 10$ to money by each player
                EventManager.UpdateAIMoeny(aiMoney, -anteMoney);
                EventManager.UpdatePlayerMoeny(playerMoney, -anteMoney);
                EventManager.UpdateMoenyPool(moneyPool, anteMoney*2);
                StartCoroutine(TimeBetweenStates(GameStates.HAND_OUT_CARDS));
                //what if noone can pay
                break;

            case GameStates.HAND_OUT_CARDS:
                //create new deck
                InitilizeDeck();
                //shuffle deck
                ShuffleDeck();
                //handout all cards
                HandOutCards();
                //create card visuals
                StartCoroutine(HandoutTheCards());
                break;

            case GameStates.BETTING_ROUND_1:
                //active button function
                break;

            case GameStates.DISCARD:
                //discard picked card by player and ai
                break;

            case GameStates.BETTING_ROUND_2:
                //activate the buttons for betting
                break;

            case GameStates.CHECK_WINNER:
                //show all cards
                //check round winner
                //pass money to the winner
                //check game over
                break;

            default:
                break;
        }
    }
    private IEnumerator TimeBetweenStates(GameStates gameState)
    {
        yield return new WaitForSeconds(3f);
        ChangeGameState(gameState);
    }
    private IEnumerator HandoutTheCards()
    {
        for (int i = 0; i < 5; i++)
        {
            var aiCardNameParts = AIHand[i].Split(' ');
            var playerCardNameParts = PlayerHand[i].Split(' ');
            InstantiateCard(aiCardSlots[i], aiCardNameParts, true);  // AI card
            yield return new WaitForSeconds(0.25f);
            InstantiateCard(playerCardSlots[i], playerCardNameParts, false);  // Player card
            yield return new WaitForSeconds(0.25f);
        }
        ChangeGameState(GameStates.BETTING_ROUND_1);
    }
    private void InstantiateCard(Transform slot, string[] cardNameParts, bool isAI)
    {
        var newCard = Instantiate(cardPrefab, slot, false);
        newCard.SetCardData(cardNameParts[2], cardNameParts[0], isAI);
        aiCards.Add(newCard);
    }
}
public enum GameStates
{
    ROUND_START,
    ANTE,
    HAND_OUT_CARDS,
    BETTING_ROUND_1,
    DISCARD,
    BETTING_ROUND_2,
    CHECK_WINNER
}