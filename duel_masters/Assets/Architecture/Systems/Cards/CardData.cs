/*
 Author: Aaron Hines
 Description: Scriptable object that is the very identity of a card
 */
using System.Collections.Generic;

using UnityEngine;
using Sirenix.OdinInspector;

using DuelMasters.Systems.Effects;
using DuelMasters.Systems.Casting;

namespace DuelMasters.Systems.Cards
{

    [CreateAssetMenu(fileName = "New Card", menuName = Constants.CREATE_NEW_CARD_DATA, order = 1)]
    public class CardData : SerializedScriptableObject
    {
        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public Sprite cardSprite;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public Material cardMaterial;

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        public SetIdentifier set;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public string cardName;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public string cardId;

        //public CardType cardType;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public CardType cardType = new CardType();

        [ShowIf( "IsCreature" )]
        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public int power;

        [ShowIf( "IsCreature" )]
        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        public int numberOfShieldsBroken = 1;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public Civilization civilization = new Civilization();

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public int manaCost;

        [ShowIf( "IsCreature" )]
        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public List<Race> creatureRace = new List<Race>();

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        public List<ICastRequirements> castRequirements = new List<ICastRequirements>();

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public List<Effect> effects = new List<Effect>();

        public bool IsCreature()
        {
            return cardType.cardTypes.Contains(CType.Creature) || cardType.cardTypes.Contains(CType.EvolutionCreature) | cardType.cardTypes.Contains(CType.PsychicCreature);
        }
    }
}

