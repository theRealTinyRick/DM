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

    public class ShieldAddedEvent : GameEvent<Player, Card>
    {
        public ShieldAddedEvent ( Player source ) : base( source )
        {
        }
    }

    public class ShieldBrokenEvent : GameEvent<Player, Card>
    {
        public ShieldBrokenEvent( Player source ) : base( source )
        {
        }
    }

    public class ManaAddedEvent : GameEvent<Player, Card>
    {
        public ManaAddedEvent( Player source ) : base( source )
        {
        }
    }
}
