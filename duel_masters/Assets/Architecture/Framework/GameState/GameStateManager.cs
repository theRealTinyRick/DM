using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using Sirenix.OdinInspector;

namespace GameFramework.GameState
{
    // TODO: make states take a priority
    public enum StatePriority : int
    {
        Low,
        High    
    }

    public class State
    {
        public State(string owner, GameStateIdentifier gameState, StatePriority priority)
        {
            this.owner = owner;
            this.gameState = gameState;
            this.priority = priority;
        }

        public string owner = "unidentified";
        public GameStateIdentifier gameState;
        public StatePriority priority;
    }

    class GameStateManager : Singleton_SerializedMonobehaviour<GameStateManager>
    {

        public State currentState
        {
            get
            {
                if(gameStates.Count > 0)
                {
                    return gameStates.Peek();
                }

                return null;
            }
        }

        public Stack<State> gameStates
        {
            get;
            private set;

        } = new Stack<State>();

        public GameStateChangedEvent gameStateChangedEvent
        {
            get;
            private set;
        } = new GameStateChangedEvent();

        public void PushState(string owner, GameStateIdentifier gameState, StatePriority statePriority = StatePriority.Low)
        {
            State _state = new State(owner, gameState, statePriority);
            if (currentState != null)
            {
                if(statePriority < currentState.priority)
                {
                    // insert below all of the other ones
                    Stack<State> _states = new Stack<State>(gameStates);
                    while(_states.Peek().priority > statePriority && _states.Count > 0)
                    {
                        _states.Pop();
                    }

                    _states.Push( _state );
                    return;
                }
            }

            gameStates.Push( _state );
        }

        /// <summary>
        /// End this state and return to default
        /// </summary>
        /// <param name="gameState"></param>
        public bool PopState(GameStateIdentifier gameState)
        {
            if(currentState.gameState == gameState)
            {
                return true;
            }
            else
            {
                Debug.LogWarning("Tried to pop a state that is not the current state");
                return false;
            }
        }
    }
}
