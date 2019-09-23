/*
 Author: Aaron Hines
 Description: Base interface for triggers that can cause mechanics
*/
using DM.Systems.Cards;

namespace DM.Systems.CardMechanics
{
    public interface IMechanicTrigger
    {
        Card card { get; set; }

        void Initialize(Card card);
        void DeInitialize();
        void Trigger(object arg);
    }
}
