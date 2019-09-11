/*
 Author: Aaron Hines
 Description: Simply draws a card from the players deck
*/
using UnityEngine;
using DM.Systems.Cards;
using DM.Systems.Players;

namespace DM.Systems.CardMechanics.Mechanics
{
    public class DrawCards : ICardMechanic
    {
        [SerializeField]
        private int amountToDraw;

        [SerializeField]
        private PlayerSelection targetPlayer;

        public void Use(Card card)
        {
            // does not exist yet, simply an example
            // card.owner.Draw(amountToDraw);
        }
    }
}
