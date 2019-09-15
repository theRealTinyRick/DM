/*
 Author: Aaron Hines
 Description: Instance of a player - will contain collectins of cards for the duel instnace
*/

using DM.Systems.Cards;
using UnityEngine;

namespace DM.Systems.Players
{
    [System.Serializable]
    public class Player
    {
        public Player(Deck deck)
        {
            deckData = deck;
            this.deck = deck.GenerateDeckInstance(this);
        }

        public Deck deckData;

        [SerializeField]
        public CardCollectionInstance deck;

        [SerializeField]
        public CardCollectionInstance hand;

        [SerializeField]
        public CardCollectionInstance graveyard;

        [SerializeField]
        public CardCollectionInstance sheildZone;

        [SerializeField]
        public CardCollectionInstance manaZone;

        [SerializeField]
        public CardCollectionInstance battleZone;
    }
}
