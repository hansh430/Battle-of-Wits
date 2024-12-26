using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CardData", order = 1)]
public class CardData : ScriptableObject
{
    public Sprite spadeSprite;
    public Sprite clubSprite;
    public Sprite diamondSprite;
    public Sprite heartSprite;
}
