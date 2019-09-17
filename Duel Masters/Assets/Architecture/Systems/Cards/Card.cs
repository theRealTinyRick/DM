/*
 Author: Aaron Hines
 Description: The instance of a card in the game
*/
using System.Collections.Generic;
using UnityEngine;

using DM.Systems.Players;
using DM.Systems.CardMechanics;
using System;
using DM.Systems.Gameplay.Locations;

namespace DM.Systems.Cards
{
    [Serializable]
    public class Card   
    {
        public Card(CardData data, Player owner)
        {
            this.data = data;
            this.owner = owner;

            Initialize();
        }

        private Guid _id;
        public Guid id
        {
            get => _id;
            private set => _id = value;
        }

        [SerializeField]
        private CardData _data;
        public CardData data
        {
            get
            {
                return _data;
            }
            private set
            {
                _data = value;
            }
        }

        private Player _owner;
        [HideInInspector]
        public Player owner
        {
            get
            {
                if(overrideOwner != null)
                {
                    return overrideOwner;
                }
                return _owner;
            }
            private set
            {
                _owner = value;
            }
        }

        private Player _overrideOwner;
        [HideInInspector]
        public Player overrideOwner
        {
            get
            {
                return _overrideOwner;
            }
            private set
            {
                _overrideOwner = value;
            }
        }

        private string _cardName;
        [HideInInspector]
        public string cardName
        {
            get
            {
                if(!string.IsNullOrEmpty(nameOverride) && overrideName)
                {
                    return nameOverride;
                }
                return _cardName;
            }
            private set
            {
                _cardName = value;
            }
        }

        private string _nameOverride;
        [HideInInspector]
        public string nameOverride
        {
            get
            {
                return _nameOverride;
            }
            private set
            {
                _nameOverride = value;
            }
        }

        private CardType _cardType;
        [HideInInspector]
        public CardType cardType
        {
            get
            {
                return _cardType;
            }
            private set
            {
                _cardType = value;
            }
        }

        private List<Race> _creatureRace;
        [HideInInspector]
        public List<Race> creatureRace
        {
            get
            {
                return _creatureRace;
            }
            private set
            {
                _creatureRace = value;
            }
        }

        private Civilization _civilization;
        [HideInInspector]
        public Civilization civilization
        {
            get
            {
                if(civilizationOverride != null)
                {
                    return civilizationOverride;
                }

                return _civilization;
            }
            private set
            {
                _civilization = value;
            }
        }

        private Civilization _civilizationOverride;
        [HideInInspector]
        public Civilization civilizationOverride
        {
            get
            {
                return _civilizationOverride;
            }
            private set
            {
                _civilizationOverride = value;
            }
        }

        private int _manaCost;
        [HideInInspector]
        public int manaCost
        {
            get
            {
                if(overrideCost)
                {
                    return manaCostOverride;
                }

                return _manaCost;
            }
            private set
            {
                _manaCost = value;
            }
        }

        private int _manaCostOverride;
        [HideInInspector]
        public int manaCostOverride
        {
            get
            {
                return _manaCostOverride;
            }
            private set
            {
                _manaCostOverride = value;
            }
        }

        private Dictionary<IMechanicTrigger, Effect> _mechanics = new Dictionary<IMechanicTrigger, Effect>();
        [HideInInspector]
        public Dictionary<IMechanicTrigger, Effect> mechanics
        {
            get
            {
                return _mechanics;
            }
            private set
            {
                _mechanics = value;
            }
        } 

        [HideInInspector]
        public Sprite cardImage;

        [HideInInspector]
        public Material cardMaterial;

        private CardLocation _currentLocation;
        [HideInInspector]
        public CardLocation currentLocation
        {
            get => _currentLocation;
            private set => _currentLocation = value;
        }

        private bool overrideName = false;
        private bool overrideCost = false;

        /// <summary>
        ///     This function will set up any relationships between this card instance and the mechanics and triggers
        /// </summary>
        private void Initialize()
        {
            cardImage = data.cardImage;
            cardMaterial = data.cardMaterial;
            cardName = data.cardName;
            cardType = data.cardType;
            civilization = data.civilization;
            manaCost = data.manaCost;
            mechanics = data.mechanics;
            creatureRace = data.creatureRace;

            foreach (var _pair in mechanics)
            {
                _pair.Key.Initialize(this);
                _pair.Value.Initialize(this);
            }

            id = Guid.NewGuid();
        }
        
        public void SetCurrentLocation(CardLocation newLocation)
        {
            currentLocation = newLocation;
        }
    }
}

