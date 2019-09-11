/*
 Author: Aaron Hines
 Description: The instance of a card in the game
*/
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using DM.Systems.Players;
using DM.Systems.CardMechanics;

namespace DM.Systems.Cards
{
    [System.Serializable]
    public class Card   
    {
        public Card(CardData data, Player owner)
        {
            this.data = data;
            this.owner = owner;

            Initialize();
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

        [SerializeField]
        private Player _owner;
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

        [SerializeField]
        private Player _overrideOwner;
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

        [SerializeField]
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

        [SerializeField]
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

        [SerializeField]
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

        [SerializeField]
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

        [SerializeField]
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

        [SerializeField]
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

        [SerializeField]
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

        [SerializeField]
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

        [SerializeField]
        public Image cardImage;

        private bool overrideName = false;
        private bool overrideCost = false;

        /// <summary>
        ///     This function will set up any relationships between this card instance and the mechanics and triggers
        /// </summary>
        private void Initialize()
        {
            cardImage = data.cardImage;
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
    }
}

