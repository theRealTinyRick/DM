/*
 Author: Aaron Hines
 Description: The instance of a card in the game
*/
using System;
using System.Collections.Generic;

using UnityEngine;
using Sirenix.OdinInspector;

using DM.Systems.Players;
using DM.Systems.CardMechanics;
using DM.Systems.Gameplay.Locations;
using DM.Systems.Casting;

namespace DM.Systems.Cards
{
    [Serializable]
    public class Card   
    {
        public Card(CardData data, PlayerComponent owner)
        {
            this.data = data;
            this.owner = owner;
            instanceId = Guid.NewGuid();

            Initialize();
        }

        [ShowInInspector]
        private Guid _id;
        public Guid instanceId
        {
            get => _id;
            private set => _id = value;
        }

        [ShowInInspector]
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

        private PlayerComponent _owner;
        public PlayerComponent owner
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

        private PlayerComponent _overrideOwner;
        public PlayerComponent overrideOwner
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

        public string cardName
        {
            get
            {
                if(!string.IsNullOrEmpty(nameOverride) && overrideName)
                {
                    return nameOverride;
                }
                return data.cardName;
            }
        }

        public string nameOverride
        {
            get;
            private set;
        }

        public CardType cardType
        {
            get
            {
                return data.cardType;
            }
        }

        public List<Race> creatureRace
        {
            get
            {
                return data.creatureRace;
            }
        }

        public Civilization civilization
        {
            get
            {
                if(civilizationOverride != null)
                {
                    return civilizationOverride;
                }

                return data.civilization;
            }
        }

        public Civilization civilizationOverride
        {
            get;
            private set;
        }

        public int manaCost
        {
            get
            {
                if(overrideCost)
                {
                    return manaCostOverride;
                }

                return data.manaCost;
            }
        }

        public int manaCostOverride
        {
            get;
            private set;
        }

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        public List<ICastRequirements> castRequirements
        {
            get;
        } = new List<ICastRequirements>();

        public Dictionary<IMechanicTrigger, Effect> mechanics
        {
            get;
            private set;
        } 

        public Sprite sprite
        {
            get
            {
                return data.cardSprite;
            }
        }

        public Material cardMaterial
        {
            get
            {
                return data.cardMaterial;
            }
        }

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

            foreach(var _req in castRequirements)
            {
                _req.card = this;
            }

            mechanics = data.mechanics;

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

        public void SetOwner(PlayerComponent owner)
        {
            this.owner = owner;
        }

        public void SetTap(bool tap)
        {
            tapped = tap;
        }

        /// <summary>
        ///     Used to sync network. 
        /// </summary>
        /// <param name="instanceId"></param>
        public void SetId(Guid instanceId)
        {
            this.instanceId = instanceId;
        }
    }
}

