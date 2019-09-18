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
    public class CreatureSummonedEvent : UnityEvent<Player, Card> { }

    [System.Serializable]
    public class CardDrawnEvent : UnityEvent<Player, Card> { }

    [System.Serializable]
    public class ShieldAddedEvent : UnityEvent<Player, Card> { }

    [System.Serializable]
    public class ShieldBrokenEvent : UnityEvent<Player, Card> { }

    [System.Serializable]
    public class ManaAddedEvent : UnityEvent<Player, Card> { }
}
