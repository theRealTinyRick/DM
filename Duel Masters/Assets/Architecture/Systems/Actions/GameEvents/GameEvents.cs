/*
 Author: Aaron Hines
 Description: Core events that happen in the game
*/
using DM.Systems.Cards;
using DM.Systems.Players;
using GameFramework.Events;

namespace DM.Systems.GameEvents
{
    public class CreatureSummonedEvent : GameEvent<Player, Card>
    {
        public CreatureSummonedEvent( Player source ) : base( source )
        {
        }
    }

    public class CardDrawnEvent : GameEvent<Player, Card>
    {
        public CardDrawnEvent( Player source ) : base( source )
        {
        }
    }

    public class ShieldBroken : GameEvent<Player, Card>
    {
        public ShieldBroken( Player source ) : base( source )
        {
        }
    }

    public class ManaAdded : GameEvent<Player, Card>
    {
        public ManaAdded( Player source ) : base( source )
        {
        }
    }
}
