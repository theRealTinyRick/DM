/*
 Author: Aaron Hines
 Description: Holds a collection of cards like an instance of a deck or hand or graveyard or even sheilds
 */
using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

using DM.Systems.Players;
using GameFramework.Events;

namespace DM.Systems.Cards
{
    [Serializable]
    public class CardAddedEvent : GameEvent<CardCollection, Card>
    {
        public CardAddedEvent(CardCollection source) : base(source)
        {
        }
    }

    [Serializable]
    public class CardRemovedEvent : GameEvent<CardCollection, Card>
    {
        public CardRemovedEvent(CardCollection source) : base(source)
        {
        }
    }

    [Serializable]
    public class CardCollection
    {
        public CardCollection()
        {
            collection = new Dictionary<CardData, List<Card>>();
            cards = new List<Card>();
        }

        public CardCollection(Dictionary<CardData, int> collection, PlayerComponent owner, string[] instanceIds = null /*used to manually set guids*/)
        {
            this.owner = owner;
            this.collection = new Dictionary<CardData, List<Card>>();

            foreach(var _pair in collection)
            {
                this.collection.Add(_pair.Key, new List<Card>());

                for (int _i = 0; _i < _pair.Value; _i++)
                {
                    this.collection[_pair.Key].Add(new Card(_pair.Key, this.owner));
                }
            }

            cards = this.collection.Values.ToList().SelectMany( _card => _card ).ToList(); 
            
            // set initial card index
            for( int _i = 0; _i < cards.Count; _i++ )
            {
                cards[_i].cardIndex = _i;

                // if ids have been passed through, set their ids here - these will be used to identify cards
                if (instanceIds != null && _i <= cards.Count)
                {
                    cards[_i].SetId( Guid.Parse(instanceIds[_i]) );
                }
            }
        }

        private Dictionary<CardData, List<Card>> collection;
        
        [SerializeField]
        private List<Card> _cards;
        public List<Card> cards
        {
            get => _cards;
            private set => _cards = value;
        }

        private PlayerComponent _owner;
        public PlayerComponent owner
        {
            get => _owner;
            private set => _owner = value;
        }

        public int Count
        {
            get
            {
                return cards.Count;
            }
        }

        private CardAddedEvent _cardAddedEvent;
        public CardAddedEvent cardAddedEvent
        {
            get
            {
                if (_cardAddedEvent == null)
                {
                    _cardAddedEvent = new CardAddedEvent(this);
                }
                return _cardAddedEvent;
            }
            private set => _cardAddedEvent = value;
        }

        private CardRemovedEvent _cardRemovedEvent;
        public CardRemovedEvent cardRemovedEvent
        {
            get
            {
                if(_cardRemovedEvent == null)
                {
                    _cardRemovedEvent = new CardRemovedEvent(this);
                }
                return _cardRemovedEvent;
            }
            private set => _cardRemovedEvent = value;
        }

        private void Add(Card card)
        {
            if(!cards.Contains(card))
            {
                cards.Add( card );
            }
            cardAddedEvent?.Invoke(card);
        }

        private bool Remove(Card card)
        {
            if (cards.Contains(card))
            {
                cards.Remove( card );
                cardRemovedEvent?.Invoke(card);
                return true;
            }

            return false;
        }

        public void Transfer(Card card, CardCollection to)
        {
            if(Contains(card))
            {
                Remove(card);
                to.Add(card);
            }
        }

        public bool Contains(Card card)
        {
            return cards.Contains(card);
        }

        public Card Get(CardData data)
        {
            return cards.Find( _card => _card.data == data );
        }

        public Card Get(Guid instanceId)
        {
            return cards.Find( _card => _card.instanceId == instanceId );
        }

        public List<Card> GetAll(CardData data)
        {
            return cards.FindAll( _card => _card.data == data );
        }

        public int[] Shuffle(bool repositionCards = false) // set card indexes instead of rearanging cards
        {
            // TODO: maybe find a better way to randomize these. 
            // maybe actually randomize them and call a second function to reorder them based on card id. That has to happen anyway
            List<int> _availableIndexes = new int[cards.Count].ToList();
            for(int _i = 0; _i < _availableIndexes.Count; _i++)
            {
                _availableIndexes[_i] = _i;
            }

            for (int _i = 0; _i < cards.Count; _i++)
            {
                int _index = _availableIndexes[UnityEngine.Random.Range( 0, _availableIndexes.Count )];
                _availableIndexes.Remove( _index );
                cards[_i].cardIndex = _index;
            }

            List<int> _result = new List<int>();
            foreach(Card _card in cards)
            {
                _result.Add( _card.cardIndex );
            }

            if ( repositionCards )
            {
                RepositionCardsToIndexes();
            }

            return _result.ToArray();
            

            //int _randomNumberOfTimesToShuffle = UnityEngine.Random.Range( 5, 10 );

            //for ( int _i = 0; _i < _randomNumberOfTimesToShuffle; _i++ )
            //{
            //    List<Card> _oldList = new List<Card>( cards );
            //    List<Card> _newCardList = new List<Card>();

            //    while(_oldList.Count > 0)
            //    {
            //        int _rando = UnityEngine.Random.Range( 0, _oldList.Count );
            //        Card _card = _oldList[_rando];

            //        _oldList.Remove(_card);
            //        _newCardList.Add( _card );
            //    }

            //    cards = _newCardList;
            //    cards = _newCardList;
            //}
        }

        public void SetCardIndexes(int[] indexes, bool repositionCards = false)
        {
            for(int _i = 0; _i < indexes.Length; _i++ )
            {
                if(_i < cards.Count)
                {
                    cards[_i].cardIndex = indexes[_i];
                }
            }

            if(repositionCards)
            {
                RepositionCardsToIndexes();
            }
        }

        public void RepositionCardsToIndexes()
        {
            List<Card> _newList = new List<Card>();
            for( int i = 0; i < cards.Count; i++ )
            {
                _newList.Add( cards.Find( _card => _card.cardIndex == i ) );
            }

            cards = _newList;
        }
    }
}
