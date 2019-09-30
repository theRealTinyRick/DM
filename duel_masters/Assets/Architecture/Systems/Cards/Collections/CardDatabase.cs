/*
 Author: Aaron Hines
 Description: central location for constant strings used in code
*/

using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using Sirenix.OdinInspector;

namespace DM.Systems.Cards
{
    [CreateAssetMenu(fileName = "New Card Database", menuName = Constants.CREATE_NEW_CARD_DATABASE, order = 1)]
    public class CardDatabase : SerializedScriptableObject
    {
        [SerializeField] // set to card list
        private Dictionary<SetIdentifier, List<CardData>> _collection = new Dictionary<SetIdentifier, List<CardData>>();
        public Dictionary<SetIdentifier, List<CardData>> collection
        {
            get => _collection;
        }

        public List<CardData> cards
        {
            get
            {
                List<CardData> _cards = new List<CardData>();
                foreach(var _pair in collection)
                {
                    _cards = _cards.Concat( _pair.Value ).ToList();
                }

                return _cards;
            }
        }

        public CardData GetById(string id)
        {
            return cards.Find( _card => _card.cardId == id );
        }

        public CardData GetByName( string name )
        {
            return cards.Find( _card => _card.cardName == name );
        }
    }
}
