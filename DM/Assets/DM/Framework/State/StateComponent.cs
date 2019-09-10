/*
 Author: Aaron Hines
 Edits By: 
 Description: General Inpout events that will be used by the PlayerInputComponent
 */
using System.Collections.Generic;

using UnityEngine;
using Sirenix.OdinInspector;

using GameFramework.Actors.Components;

namespace GameFramework.State
{
    [System.Serializable]
    public class StateChangedEvent : UnityEngine.Events.UnityEvent<StateType, bool> { }

    [System.Serializable]
    public class StateSlot
    {
        public StateSlot(bool status)
        {
            this.status = status;
        }

        [SerializeField]
        public StateChangedEvent stateTrueEvent = new StateChangedEvent();

        [SerializeField]
        public StateChangedEvent stateFalseEvent = new StateChangedEvent();

        public bool status;
    }

    public class StateComponent : ActorComponent
    {
        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        private Dictionary<StateType, StateSlot> states;

        [TabGroup(Tabs.EVENTS)]
        [SerializeField]
        StateChangedEvent stateChangedEvent = new StateChangedEvent();

        public bool GetState(StateType state)
        {
            StateSlot _stateValue;

            if (states.TryGetValue(state, out _stateValue))
            {
                return _stateValue.status;
            }

            Debug.LogWarning("The state component could not find the state: " + state + ", so we are refturning false ", gameObject);
            return false;
        }

        public bool GetOrCreateState(StateType state, bool defaultValue = false)
        {
            StateSlot _stateValue;

            if (states.TryGetValue(state, out _stateValue))
            {
                return _stateValue.status;
            }

            states.Add(state, new StateSlot(defaultValue));
            return defaultValue;
        }

        public bool HasState(StateType state)
        {
            if (states.ContainsKey(state))
            {
                return true;
            }

            return false;
        }

        public bool SetState(StateType state, bool value)
        {
            if (states.ContainsKey(state))
            {
                states[state].status = value;

                if (stateChangedEvent != null)
                {
                    stateChangedEvent.Invoke(state, value);

                    if (value)
                    {
                        states[state].stateTrueEvent?.Invoke(state, value);
                    }
                    else
                    {
                        states[state].stateFalseEvent?.Invoke(state, value);
                    }
                }

                return true;
            }

            return false;
        }

        public void SetStateFalse(StateType state)
        {
            SetState(state, false);
        }

        public void SetStateTrue(StateType state)
        {
            SetState(state, true);
        }

        public void ReverseState(StateType state)
        {
            if (states.ContainsKey(state))
            {
                states[state].status = !states[state].status;

                if (stateChangedEvent != null)
                {
                    stateChangedEvent.Invoke(state, states[state].status);
                }
            }
        }

        public bool AnyStateTrue()
        {
            foreach (StateType _state in states.Keys)
            {
                if (states[_state].status)
                {
                    return true;
                }
            }

            return false;
        }

        public bool AnyStateTrue(List<StateType> states)
        {
            foreach (StateType _state in states)
            {
                if (this.states[_state].status)
                {
                    return true;
                }
            }

            return false;
        }

        public bool AnyStateFalse()
        {
            foreach (StateType _state in states.Keys)
            {
                if (!states[_state].status)
                {
                    return true;
                }
            }

            return false;
        }

        public bool AnyStateFalse(List<StateType> states)
        {
            foreach (StateType _state in states)
            {
                if (this.states.ContainsKey(_state))
                {
                    if (!this.states[_state].status)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// satisfying the abstract class
        public override void InitializeComponent() { } 
        public override void DisableComponent() { }
    }
}
