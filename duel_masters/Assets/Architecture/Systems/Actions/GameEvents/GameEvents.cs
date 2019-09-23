/*
 Author: Aaron Hines
 Description: Core events that happen in the game
*/
using UnityEngine.Events;

using DM.Systems.Cards;
using DM.Systems.Players;

namespace DM.Systems.GameEvents
{
    [System.Serializable]
    public class GameStartedEvent : UnityEvent { }

    [System.Serializable]
    public class GameEndedEvent : UnityEvent { }

    [System.Serializable]
    public class TurnChangedEvent : UnityEvent<Player> { }


    [System.Serializable]
    public class StartPhaseEvent : UnityEvent<Player> { }

    [System.Serializable]
    public class DrawPhaseEvent : UnityEvent<Player> { }

    [System.Serializable]
    public class ManaPhaseEvent : UnityEvent<Player> { }

    [System.Serializable]
    public class MainPhaseEvent : UnityEvent<Player> { }

    [System.Serializable]
    public class BattlePhaseEvent : UnityEvent<Player> { }

    [System.Serializable]
    public class EndPhaseEvent : UnityEvent<Player> { }


    [System.Serializable]
    public class CreatureSummonedEvent : UnityEvent<Player, Card> { }

    [System.Serializable]
    public class CardDrawnEvent : UnityEvent<Player, Card> { }

    [System.Serializable]
    public class ShieldAddedEvent : UnityEvent<Player, Card> { }

    [System.Serializable]
    public class ShieldBrokenEvent : UnityEvent<Player, Card> { }

    [System.Serializable]
    public class ManaAddedEvent : UnityEvent<Player, Card> { }

    [System.Serializable]
    public class CardTappedEvent : UnityEvent<Card> { }

    [System.Serializable]
    public class CardUntappedEvent : UnityEvent<Card> { }
}
