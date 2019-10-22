/*
 Author: Aaron Hines
 Description: Represents a card effect. Essentially a list of mechanics that get carried out sequentially
*/

using System;
using System.Collections.Generic;
using UnityEngine;

using DuelMasters.Systems.Cards;
using DuelMasters.Systems.Gameplay.Locations;

namespace DuelMasters.Systems.Effects
{
    public enum Frequency
    {
        Unlimited,
        OneInADuel, 
        OnceInATurn
    }

    public class Effect
    {
        public Effect() { }

        /// <summary>
        ///     Copy Constructor: will create new instances of 
        /// </summary>
        /// <param name="effect"></param>
        public Effect(Effect effect) 
        {
            this.triggerableLocations = effect.triggerableLocations;

            // create new instances of the interfaces
            triggers = new List<IMechanicTrigger>();
            foreach (var _trig in effect.triggers)
            {
                if(_trig != null)
                {
                    triggers.Add(_trig.Copy());
                }
                else
                {
                    Debug.LogError("Effect: trigger was null");
                }
            }

            // create new instances of the interfaces
            mechanics = new List<ICardMechanic>();
            foreach (var _mech in effect.mechanics)
            {
                if(_mech != null)
                {
                    mechanics.Add(_mech.Copy());
                }
                else
                {
                    Debug.LogError("Effect: mechanic was null");
                }
            }
        }

        [SerializeField]
        public List<CardLocation> triggerableLocations = new List<CardLocation>(); 

        [SerializeField]
        public List<IMechanicTrigger> triggers = new List<IMechanicTrigger>();

        [SerializeField]
        public List<ICardMechanic> mechanics = new List<ICardMechanic>();

        [HideInInspector]
        public Card card;

        public bool resolved
        {
            get;
            private set;
        }

        public void Initialize(Card card)
        {
            this.card = card;

            foreach(var _trigger in triggers)
            {
                _trigger.Initialize(card, this);
            }

            foreach (var _mech in mechanics)
            {
                _mech.Initialize(card);
            }
        }

        public void Trigger(object source)
        {
            // TODO: this needs to be more elegant and passed to a manager to carry out
            if ( card.owner.isLocal && triggerableLocations.Contains(card.currentLocation))
            {
                foreach (ICardMechanic _mech in mechanics)
                {
                    _mech.Use();
                }
            }
        }

        public void StartEffect()
        {

        }

        public void EndEffect()
        {

        }
    }
}
