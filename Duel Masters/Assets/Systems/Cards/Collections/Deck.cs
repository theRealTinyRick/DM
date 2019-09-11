using System.Collections.Generic;

using UnityEngine;

namespace DM.Systems.Cards
{
    public class Deck
    {
        public Deck() { }

        [SerializeField]
        private Dictionary<CardData, int> _collection;
        public Dictionary<CardData, int> collection
        {
            get => _collection;
            private set => _collection = value;
        }
    }
}
