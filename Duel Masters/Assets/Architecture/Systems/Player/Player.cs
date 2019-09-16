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
        public Player(Deck deck, int playerNumber = 0)
        {
            deckData = deck;
            this.deck = deck.GenerateDeckInstance(this);
            this.playerNumber = playerNumber;
        }

        [SerializeField]
        public int playerNumber;

        [SerializeField]
        public Deck deckData;

        [SerializeField]
        public CardCollection deck;

        [SerializeField]
        public CardCollection hand;

        [SerializeField]
        public CardCollection graveyard;

        [SerializeField]
        public CardCollection sheildZone;

        [SerializeField]
        public CardCollection manaZone;

        [SerializeField]
        public CardCollection battleZone;
    }
}
