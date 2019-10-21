/*
 Author: Aaron Hines
 Description: The instance of a card in the game
*/
using System;
using System.Collections.Generic;

using UnityEngine;
using Sirenix.OdinInspector;

using DuelMasters.Systems.Players;
using DuelMasters.Systems.Effects;
using DuelMasters.Systems.Gameplay.Locations;
using DuelMasters.Systems.Casting;

namespace DuelMasters.Systems.Cards
{
    public enum CardStatus
    {
        Hidden,
        Private,
        Uptapped,
        Tapped
    }

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

        public CardStatus cardStatus
        {
            get;
            private set;

        } = CardStatus.Hidden;

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

        [SerializeField]
        public List<Effect> effects = new List<Effect>();

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

        private void Initialize()
        {
            foreach(var _req in castRequirements)
            {
                _req.card = this;
            }

            SetupEffects();
        }

        private void SetupEffects()
        {
            effects = new List<Effect>();
            foreach(var _eff in data.effects)
            {
                effects.Add(new Effect(_eff));
            }

            foreach (var _effect in effects)
            {
                _effect.Initialize(this);
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

        public void SetId(Guid instanceId)
        {
            this.instanceId = instanceId;
        }
    }
}

