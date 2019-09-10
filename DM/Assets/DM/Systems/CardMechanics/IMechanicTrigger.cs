/*
 Author: Aaron Hines
 Description: Base interface for triggers that can cause mechanics
*/
using DM.Systems.Cards;

namespace DM.Systems.CardMechanics
{
    public interface IMechanicTrigger
    {
        void Initialize(Card card);
        void DeInitialize();
    }
}
