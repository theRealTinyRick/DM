/*
 Author: Aaron Hines
 Description: Base interface for all card mechanics
*/
using DuelMasters.Systems.Cards;

namespace DuelMasters.Systems.Effects
{
    public interface ICardMechanic
    {
        ICardMechanic Copy();
        void Initialize(Card card);
        void Use();
    }
}
