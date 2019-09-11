﻿/*
 Author: Aaron Hines
 Description: Scriptable object that is the very identity of a card
 */
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

using DM.Systems.CardMechanics;

namespace DM.Systems.Cards
{
    public enum CardType
    {
        Creature,
        Spell
    }

    [CreateAssetMenu(fileName = "New Card", menuName = Constants.CreateNewCardData, order = 1)]
    public class CardData : SerializedScriptableObject
    {
        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public Image cardImage;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public string cardName;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public CardType cardType;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public Civilization civilization = new Civilization();

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public int manaCost;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public List<Race> creatureRace = new List<Race>();

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public Dictionary<IMechanicTrigger, Effect> mechanics = new Dictionary<IMechanicTrigger, Effect>();

    }
}
