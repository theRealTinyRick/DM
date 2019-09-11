/*
 Author: Aaron Hines
 Description: Base interface for all card mechanics
*/
using DM.Systems.Cards;

namespace DM.Systems.CardMechanics
{
    public interface ICardMechanic
    {
        void Use(Card card);
    }
}
