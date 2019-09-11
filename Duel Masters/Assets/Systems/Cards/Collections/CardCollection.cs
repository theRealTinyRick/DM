/*
 Author: Aaron Hines
 Description: Holds a collection of cards like an instance of a deck or hand or graveyard
 */
using System;
using System.Linq;
using System.Collections.Generic;
using DM.Systems.Players;
using GameFramework.Events;
using UnityEngine;

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
        }

        public CardCollection(Dictionary<CardData, int> collection, Player owner)
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
        }

        [SerializeField]
        private Player _owner;
        public Player owner
        {
            get => _owner;
            private set => _owner = value;
        }

        [SerializeField]
        private Dictionary<CardData, List<Card>> _collection;
        public Dictionary<CardData, List<Card>> collection
        {
            get => _collection;
            private set => _collection = value;
        }

        private List<Card> cards
        {
            get
            {
                return collection.Values.ToList().SelectMany(x => x).ToList();
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

        public void Add(Card card)
        {
            if (collection.ContainsKey(card.data))
            {
                collection[card.data].Add(card);
            }
            else
            {
                collection.Add(card.data, new List<Card>() { card });
            }

            cardAddedEvent?.Invoke(card);
        }

        public bool Remove(Card card)
        {
            if (collection.ContainsKey(card.data))
            {
                if(collection[card.data].Contains(card))
                {
                    collection[card.data].Remove(card);
                    cardRemovedEvent?.Invoke(card);
                    return true;
                }
            }

            return false;
        }

        public bool Contains(Card card)
        {
            return cards.Contains(card);
        }

        public Card Get(CardData data)
        {
            if(collection.ContainsKey(data))
            {
                if(collection[data].Count() > 0)
                {
                    return collection[data][0];
                }
            }
            return null;
        }

        public List<Card> GetAll(CardData data)
        {
            List<Card> _result = new List<Card>();

            if(collection.ContainsKey(data))
            {
                foreach(Card _card in collection[data])
                {
                    _result.Add(_card);
                }
            }

            return _result;
        }

        public void Transfer(Card card, CardCollection from, CardCollection to)
        {
            if(from.Contains(card))
            {
                from.Remove(card);
                to.Add(card);
            }
        }
    }
}
