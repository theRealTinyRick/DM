/*
 Author: Aaron Hines
 Description: Events invoked by the GameStateManager
*/
using UnityEngine.Events;

namespace GameFramework.GameState
{
    [System.Serializable]
    public class GameStateChangedEvent : UnityEvent<GameStateIdentifier>
    {
    }
}
