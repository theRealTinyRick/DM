using System;
using System.Collections.Generic;

using UnityEngine;
using Sirenix.OdinInspector;
using GameFramework.Events;

namespace DuelMasters.Systems.Cards
{
    [Serializable]
    public class CardAddedToTrunkEvent : GameEvent<Trunk, CardData>
    {
        public CardAddedToTrunkEvent( Trunk source ) : base( source )
        {
        }
    }

    [Serializable]
    public class CardRemovedFromTrunkEvent : GameEvent<Trunk, Card>
    {
        public CardRemovedFromTrunkEvent( Trunk source ) : base( source )
        {
        }
    }

    [CreateAssetMenu( fileName = "New Trunk", menuName = Constants.CREATE_NEW_TRUNK, order = 1 )]
    public class Trunk : SerializedScriptableObject
    {
        [SerializeField]
        private Dictionary<CardData, int> _collection = new Dictionary<CardData, int>();
        public Dictionary<CardData, int> collection
        {
            get => _collection;
            private set => _collection = value;
        }

        private CardAddedToTrunkEvent _cardAddedToTrunkEvent;
        public CardAddedToTrunkEvent cardAddedToTrunkEvent
        {
            get
            {
                if(_cardAddedToTrunkEvent == null)
                {
                    _cardAddedToTrunkEvent = new CardAddedToTrunkEvent( this );
                }
                return _cardAddedToTrunkEvent;
            }
        }

        private CardRemovedFromTrunkEvent _cardRemovedFromTrunkEvent;
        public CardRemovedFromTrunkEvent cardRemovedFromTrunkEvent
        {
            get
            {
                if(_cardRemovedFromTrunkEvent == null)
                {
                    _cardRemovedFromTrunkEvent = new CardRemovedFromTrunkEvent( this );
                }
                return _cardRemovedFromTrunkEvent;
            }
        }

        public bool Add(CardData data, int amount = 1)
        {
            if(collection.ContainsKey(data))
            {
                collection[data] += amount;
            }
            else
            {
                collection.Add( data, amount );
            }

            if(collection[data] > Constants.MAX_CARD_DUPLICATE)
            {
                collection[data] = Constants.MAX_CARD_DUPLICATE;
            }

            _cardAddedToTrunkEvent?.Invoke( data );

            return false;
        }
    }
}
