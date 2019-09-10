/*
 Author: Aaron Hines
 Description: Scriptable object that is the very identity of a card
 */
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Sirenix.OdinInspector;

using DM.Systems.CardMechanics;

namespace DM.Systems.Cards
{
    [CreateAssetMenu(fileName = "New Card", menuName = Constants.CreateNewCardData, order = 1)]
    public class CardData : SerializedScriptableObject
    {
        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public string cardName;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public Civilization civilization = new Civilization();

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public int manaCost;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public Dictionary<IMechanicTrigger, ICardMechanic> mechanics = new Dictionary<IMechanicTrigger, ICardMechanic>();

    }
}

