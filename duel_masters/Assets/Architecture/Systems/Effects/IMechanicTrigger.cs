/*
 Author: Aaron Hines
 Description: Base interface for triggers that can cause mechanics
*/
using DuelMasters.Systems.Cards;

namespace DuelMasters.Systems.Effects
{
    public interface IMechanicTrigger
    {
        Card card { get; }     // left out set so it can be made private
        Effect effect { get; } // left out set so it can be made private

        IMechanicTrigger Copy();
        void Initialize(Card card, Effect effect);
        void DeInitialize();
        void Trigger(object arg);
    }
}
