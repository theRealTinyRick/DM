/*
 Author: Aaron Hines
 Description: Core events that happen in the game
*/
using DM.Systems.Cards;
using GameFramework.Events;

namespace DM.Systems.GameEvents
{
    public class CreatureSummonedEvent : GameEvent<Card>
    {
        public CreatureSummonedEvent(Card source) : base(source)
        {
        }
    }

    public class CardDrawnEvent : GameEvent<Card>
    {
        public CardDrawnEvent(Card source) : base(source)
        {
        }
    }

    public class ShieldBroken : GameEvent<Card>
    {
        public ShieldBroken(Card source) : base(source)
        {
        }
    }

    public class ManaAdded : GameEvent<Card>
    {
        public ManaAdded(Card source) : base(source)
        {
        }
    }
}
