using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using Sirenix.OdinInspector;

namespace GameFramework.GameState
{
    class GameStateManager : Singleton_SerializedMonobehaviour<GameStateManager>
    {
        public GameStateIdentifier currentState
        {
            get;
            private set;
        }

        public List<GameStateIdentifier> gameStates
        {
            get;
            private set;

        } = new List<GameStateIdentifier>();

        public GameStateChangedEvent gameStateChangedEvent
        {
            get;
            private set;
        } = new GameStateChangedEvent();

        /// <summary>
        ///     Start this state
        /// </summary>
        /// <param name="gameState"></param>
        public void ForceState(GameStateIdentifier gameState)
        {

        }

        /// <summary>
        ///     Start this state
        /// </summary>
        /// <param name="gameState"></param>
        public void PushState(GameStateIdentifier gameState)
        {

        }

        /// <summary>
        /// End this state and return to default
        /// </summary>
        /// <param name="gameState"></param>
        public void PopState(GameStateIdentifier gameState)
        {

        }
    }
}
