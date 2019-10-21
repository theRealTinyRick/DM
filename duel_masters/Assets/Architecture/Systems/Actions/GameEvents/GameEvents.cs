/*
 Author: Aaron Hines
 Description: Core events that happen in the game
*/
using UnityEngine.Events;

using DuelMasters.Systems.Cards;
using DuelMasters.Systems.Players;

namespace DuelMasters.Systems.GameEvents
{
    [System.Serializable]
    public class GameStartedEvent : UnityEvent { }

    [System.Serializable]
    public class GameEndedEvent : UnityEvent { }

    [System.Serializable]
    public class TurnChangedEvent : UnityEvent<PlayerComponent> { }


    [System.Serializable]
    public class StartPhaseEvent : UnityEvent<PlayerComponent> { }

    [System.Serializable]
    public class DrawPhaseEvent : UnityEvent<PlayerComponent> { }

    [System.Serializable]
    public class ManaPhaseEvent : UnityEvent<PlayerComponent> { }

    [System.Serializable]
    public class MainPhaseEvent : UnityEvent<PlayerComponent> { }

    [System.Serializable]
    public class BattlePhaseEvent : UnityEvent<PlayerComponent> { }

    [System.Serializable]
    public class EndPhaseEvent : UnityEvent<PlayerComponent> { }

    [System.Serializable]
    public class CreatureSummonedEvent : UnityEvent<PlayerComponent, Card> { }

    [System.Serializable]
    public class SpellCastEvent : UnityEvent<Card> { }

    [System.Serializable]
    public class CardPlayedEvent : UnityEvent<Card> { }

    [System.Serializable]
    public class CardDrawnEvent : UnityEvent<PlayerComponent, Card> { }

    [System.Serializable]
    public class ShieldAddedEvent : UnityEvent<PlayerComponent, Card> { }

    [System.Serializable]
    public class ShieldBrokenEvent : UnityEvent<PlayerComponent, Card> { }

    [System.Serializable]
    public class ManaAddedEvent : UnityEvent<PlayerComponent, Card> { }

    [System.Serializable]
    public class CardTappedEvent : UnityEvent<Card> { }

    [System.Serializable]
    public class CardUntappedEvent : UnityEvent<Card> { }
}
