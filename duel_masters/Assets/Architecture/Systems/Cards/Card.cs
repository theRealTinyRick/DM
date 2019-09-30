/*
 Author: Aaron Hines
 Description: The instance of a card in the game
*/
using System;
using System.Collections.Generic;
using UnityEngine;

using DM.Systems.Players;
using DM.Systems.CardMechanics;
using DM.Systems.Gameplay.Locations;

namespace DM.Systems.Cards
{
    [Serializable]
    public class Card   
    {
        public Card(CardData data, DuelistComponent owner)
        {
            this.data = data;
            this.owner = owner;
            instanceId = Guid.NewGuid();

            Initialize();
        }

        [SerializeField]
        private Guid _id;
        public Guid instanceId
        {
            get => _id;
            private set => _id = value;
        }

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

        private DuelistComponent _owner;
        public DuelistComponent owner
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

        private DuelistComponent _overrideOwner;
        public DuelistComponent overrideOwner
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

        [SerializeField]
        private string _cardName;
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

        [SerializeField]
        private bool _tapped = false;
        public bool tapped
        {
            get => _tapped;
            private set => _tapped = value;
        }

        [SerializeField]
        private CardLocation _currentLocation;
        public CardLocation currentLocation
        {
            get
            {
                return _currentLocation;
            }
            private set
            {
                if(_currentLocation != value)
                {
                    _currentLocation = value;
                }
            }
        }

        private bool overrideName = false;
        private bool overrideCost = false;

        public int cardIndex;


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
        }

        public void UpdateCardLocation(CardLocation location)
        {
            currentLocation = location;
        }

        public void SetTap(bool tap)
        {
            tapped = tap;
        }

        /// <summary>
        ///     Used to sync network
        /// </summary>
        /// <param name="instanceId"></param>
        public void SetId(Guid instanceId)
        {
            this.instanceId = instanceId;
        }
    }
}

