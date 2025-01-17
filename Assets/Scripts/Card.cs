using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [Header("CardData")]
    [SerializeField] private CardData cardSpriteData;

    [Header("Images")]
    [SerializeField] private Image middleImage;
    [SerializeField] private Image topImage;
    [SerializeField] private Image bottomImage;

    [Header("Texts")]
    [SerializeField] private TMP_Text middleText;
    [SerializeField] private TMP_Text topText;
    [SerializeField] private TMP_Text bottomText;

    [Header("Card faces")]
    [SerializeField] private GameObject cardFront;
    [SerializeField] private GameObject cardBack;
    private Button cardButton;
    private bool isDiscarded;
    private bool isShowingCard;

    private void Awake()
    {
        cardButton = GetComponent<Button>();
        cardButton.onClick.AddListener(FlipCard);
    }
    public void SetCardData(string suit, string cardRank,bool isAICard)
    {
        switch (suit)
        {
            case "Spades":
                middleImage.sprite = cardSpriteData.spadeSprite;
                topImage.sprite = cardSpriteData.spadeSprite;
                bottomImage.sprite = cardSpriteData.spadeSprite;
                break;
            case "Clubs":
                middleImage.sprite = cardSpriteData.clubSprite;
                topImage.sprite = cardSpriteData.clubSprite;
                bottomImage.sprite = cardSpriteData.clubSprite;
                break;

            case "Diamonds":
                middleImage.sprite = cardSpriteData.diamondSprite;
                topImage.sprite = cardSpriteData.diamondSprite;
                bottomImage.sprite = cardSpriteData.diamondSprite;
                break;
            case "Hearts":
                middleImage.sprite = cardSpriteData.heartSprite;
                topImage.sprite = cardSpriteData.heartSprite;
                bottomImage.sprite = cardSpriteData.heartSprite;
                break;
          
        }
        middleText.text = cardRank;
        bottomText.text = cardRank;
        topText.text = cardRank;
        cardButton.interactable = !isAICard;
        ShowCardFrontSide(!isAICard);
    }
    public void ShowCardFrontSide(bool canshow)
    {
        isShowingCard = canshow;
        cardFront.SetActive(canshow);
        cardBack.SetActive(!canshow);
    }
    public void FlipCard()
    {
        isShowingCard = !isShowingCard;
        cardFront.SetActive(isShowingCard);
        cardBack.SetActive(!isShowingCard);
    }
}
