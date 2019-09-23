/*
 Author: Aaron Hines
 Description: Represents a card effect. Essentially a list of mechanics that get carried out sequentially
*/
using DM.Systems.Cards;
using System;
using System.Collections.Generic;

namespace DM.Systems.CardMechanics
{
    [Serializable]
    public class Effect
    {
        public List<ICardMechanic> mechanics = new List<ICardMechanic>();
        public Card card;

        public void Initialize(Card card)
        {
            this.card = card;
        }

        public void Trigger(object source)
        {
        }
    }
}
