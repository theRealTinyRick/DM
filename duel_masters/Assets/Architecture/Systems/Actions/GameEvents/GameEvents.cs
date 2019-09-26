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
    public class TurnChangedEvent : UnityEvent<DuelistComponent> { }


    [System.Serializable]
    public class StartPhaseEvent : UnityEvent<DuelistComponent> { }

    [System.Serializable]
    public class DrawPhaseEvent : UnityEvent<DuelistComponent> { }

    [System.Serializable]
    public class ManaPhaseEvent : UnityEvent<DuelistComponent> { }

    [System.Serializable]
    public class MainPhaseEvent : UnityEvent<DuelistComponent> { }

    [System.Serializable]
    public class BattlePhaseEvent : UnityEvent<DuelistComponent> { }

    [System.Serializable]
    public class EndPhaseEvent : UnityEvent<DuelistComponent> { }


    [System.Serializable]
    public class CreatureSummonedEvent : UnityEvent<DuelistComponent, Card> { }

    [System.Serializable]
    public class CardDrawnEvent : UnityEvent<DuelistComponent, Card> { }

    [System.Serializable]
    public class ShieldAddedEvent : UnityEvent<DuelistComponent, Card> { }

    [System.Serializable]
    public class ShieldBrokenEvent : UnityEvent<DuelistComponent, Card> { }

    [System.Serializable]
    public class ManaAddedEvent : UnityEvent<DuelistComponent, Card> { }

    [System.Serializable]
    public class CardTappedEvent : UnityEvent<Card> { }

    [System.Serializable]
    public class CardUntappedEvent : UnityEvent<Card> { }
}
