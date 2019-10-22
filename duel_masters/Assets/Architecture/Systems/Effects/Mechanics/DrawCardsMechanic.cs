/*
 Author: Aaron Hines
 Description: Simply draws a card from the players deck
*/
using UnityEngine;
using DuelMasters.Systems.Cards;
using DuelMasters.Systems.Players;
using DuelMasters.Systems.Actions;

namespace DuelMasters.Systems.Effects.Mechanics
{
    public class DrawCardsMechanic : ICardMechanic
    {
        public DrawCardsMechanic() { }

        public DrawCardsMechanic(DrawCardsMechanic drawCardsMechanic)
        {
            this.amountToDraw = drawCardsMechanic.amountToDraw;
        }

        [SerializeField]
        private int amountToDraw = 1;

        private PlayerComponent player;

        public ICardMechanic Copy()
        {
            return new DrawCardsMechanic(this);
        }

        public void Initialize(Card card)
        {
            player = card.owner;
        }

        public void Use()
        {
            ActionManager.instance.Draw(player);
        }
    }
}
