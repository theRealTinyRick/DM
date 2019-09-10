/*
 Author: Aaron Hines
 Description: The instance of a card in the game
*/
using System.Collections.Generic;
using UnityEngine;

using DM.Systems.Players;

namespace DM.Systems.Cards
{
    [System.Serializable]
    public class Card
    {
        public Card(CardData data, Player owner)
        {
            this.data = data;
            this.owner = owner;
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

        private bool overrideCost;
        private int manaCostOverride;
    }
}

