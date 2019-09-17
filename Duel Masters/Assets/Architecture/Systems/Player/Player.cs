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

        [HideInInspector]
        public Deck deckData;

        [SerializeField]
        public int playerNumber;

        [SerializeField]
        public CardCollection deck;

        [SerializeField]
        public CardCollection hand = new CardCollection();

        [SerializeField]
        public CardCollection graveyard = new CardCollection();

        [SerializeField]
        public CardCollection sheildZone = new CardCollection();

        [SerializeField]
        public CardCollection manaZone = new CardCollection();

        [SerializeField]
        public CardCollection battleZone = new CardCollection();
    }
}
