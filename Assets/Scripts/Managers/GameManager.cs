using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<string> Deck;
    public List<string> PlayerHand;
    public List<string> AIHand;
    private void Start()
    {
        InitilizeDeck();
        ShuffleDeck();
        HandOutCards();
    }
    private void InitilizeDeck()
    {
        Deck = new List<string>();
        PlayerHand = new List<string>();
        AIHand = new List<string>();
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
}
