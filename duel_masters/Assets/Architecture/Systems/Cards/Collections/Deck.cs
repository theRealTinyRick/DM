/*
 Author: Aaron Hines
 Description: Defines what a deck is or the trunk. This is how games are loaded. NOTE: this is not the deck that is used in the game. Just a way of storing it. This can create an instance of a deck however. 
 */
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

using DM.Systems.Players;

namespace DM.Systems.Cards
{
    [CreateAssetMenu( fileName = "New Deck", menuName = Constants.CREATE_NEW_DECK, order = 1 )]
    public class Deck : SerializedScriptableObject
    {
        [SerializeField]
        public string deckName;

        [SerializeField]
        private Dictionary<CardData, int>_collection = new Dictionary<CardData, int>();
        public Dictionary<CardData, int> collection
        {
            get => _collection;
            private set => _collection = value;
        }

        /// <summary>
        ///     Generates an instance of a deck using this as a base. 
        /// </summary>
        /// <param name="owner">
        ///     Requires you to pass in an owning player. 
        /// </param>
        /// <returns></returns>
        public CardCollection GenerateDeckInstance(DuelistComponent owner)
        {
            // TODO: do check here
            //if(GetDeckCount() >= Constants.MIN_DECK_COUNT)
            {
                return new CardCollection( collection, owner );
            }
            //return null;
        }

        public int GetDeckCount()
        {
            int _result = 0;

            List<int> _counts = collection.Values.ToList();
            foreach(int _int in _counts)
            {
                _result += _int;
            }

            return _result;
        }

        public bool Add( CardData data, int amount = 1 )
        {
            if ( collection.ContainsKey( data ) )
            {
                collection[data] += amount;
            }
            else
            {
                collection.Add( data, amount );
            }

            if ( collection[data] > Constants.MAX_CARD_DUPLICATE )
            {
                collection[data] = Constants.MAX_CARD_DUPLICATE;
            }

            //_cardAddedToTrunkEvent?.Invoke( data );

            return false;
        }

        public void Remove()
        {
        }
    }
}
