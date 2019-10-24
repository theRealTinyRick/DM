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

        public Stack<State> popQueue
        {
            get;
            private set;

        } = new Stack<State>();

        public GameStateChangedEvent gameStatePushedEvent
        {
            get;
            private set;

        } = new GameStateChangedEvent();

        public GameStateChangedEvent gameStatePoppedEvent
        {
            get;
            private set;
        } = new GameStateChangedEvent();

        public bool PushState(string owner, GameStateIdentifier gameState, StatePriority statePriority = StatePriority.Low)
        {
            List<State> _states = new List<State>(gameStates.ToList());
            State _state = new State(owner, gameState, statePriority);
            if (gameStates.Contains(_state))
            {
                Debug.Log("Tried to push a state that already exists");
                return false;
            }

            if (currentState != null)
            {
                if(_state.priority < currentState.priority)
                {
                    int _index = _states.IndexOf(_states.Find(_elem => _elem.priority < currentState.priority));
                    _states.Insert(_index, _state);
                    _states.Reverse();

                    gameStates = new Stack<State>(_states);
                    return true;
                }
            }

            gameStates.Push( _state );
            return false;
        }

        public bool PopState(GameStateIdentifier gameState, bool queueForPop = false)
        {
            if(currentState.gameState == gameState)
            {
                gameStates.Pop();



                return true;
            }
            else
            {
                if(queueForPop)
                {

                    Debug.LogWarning("Tried to pop a state that is not the current state");
                    return true;
                }
            }
            return false;
        }
    }
}
