/*
 Author: Aaron Hines
 Description: Manages game states.
*/
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace GameFramework.GameState
{
    public enum StatePriority : int
    {
        Low,
        High    
    }

    public class State
    {
        public State( string owner, GameStateIdentifier gameState, StatePriority priority = StatePriority.Low )
        {
            this.owner = owner;
            this.gameState = gameState;
            this.priority = priority;
        }

        public string owner = "unidentified";
        public GameStateIdentifier gameState;
        public StatePriority priority;

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return owner + "_" + gameState.name;
        }

        public static bool operator ==(State rhs, State lhs)
        {
            return rhs.owner == lhs.owner && rhs.gameState == lhs.gameState;
        }

        public static bool operator !=(State rhs, State lhs)
        {
            return rhs.owner != lhs.owner || rhs.gameState != lhs.gameState;
        }
    }

    public class GameStateManager : Singleton_SerializedMonobehaviour<GameStateManager>
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
            State _state = new State(owner, gameState, statePriority);

            if(gameStates.Contains(_state))
            {
                Debug.LogWarning("Tried to push a state that is already in the the game state stack");
                return false;
            }

            if (currentState != null)
            {
                if (_state.priority < currentState.priority)
                {
                    List<State> _states = new List<State>(gameStates.ToList());
                    int _index = _states.IndexOf(_states.Find(_elem => _elem.priority < currentState.priority));
                    _states.Insert(_index, _state);
                    _states.Reverse();

                    gameStates = new Stack<State>(_states);
                    gameStatePushedEvent.Invoke(_state);
                    return true;
                }
            }

            gameStates.Push( _state );
            gameStatePushedEvent.Invoke(_state);
            return true;
        }

        public bool PopState(string owner, GameStateIdentifier gameState, bool queueForPop = false)
        {
            State _state = new State(owner, gameState);

            if( currentState == _state )
            {
                gameStates.Pop();
                gameStatePoppedEvent.Invoke(_state);

                while (currentState == popQueue.Peek())
                {
                    State _nextState = gameStates.Peek();
                    gameStates.Pop();
                    popQueue.Pop();
                    gameStatePoppedEvent.Invoke(_nextState);
                }

                return true;
            }
            else
            {
                if( queueForPop )
                {
                    popQueue.Push(_state);
                    Debug.LogWarning("Tried to pop a state that is not the current state");
                    return true;
                }
            }
            return false;
        }
    }
}
