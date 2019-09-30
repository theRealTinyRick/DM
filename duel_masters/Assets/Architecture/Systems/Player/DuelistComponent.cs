/*
 Author: Aaron Hines
 Description: represents the player in the scene. Handles spawning and despawning cards. Some coroutines may be called here too
*/
using System.Collections.Generic;

using UnityEngine;
using Photon.Pun;
using Sirenix.OdinInspector;

using GameFramework;
using GameFramework.Actors;
using GameFramework.Actors.Components;
using DM.Systems.Cards;
using DM.Systems.Gameplay.Locations;
using DM.Systems.Actions;

namespace DM.Systems.Players
{
    public class DuelistComponent : ActorComponent
    {
        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private Deck testDeck;

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private CardDatabase cardDatabase;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        private Identity cardIdentity;

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private Transform cardSpawnPoint;

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private float cardMoveSpeed;

        [SerializeField]
        [TabGroup( Tabs.PROPERTIES )]
        private List<CardComponent> spawnedCards = new List<CardComponent>();

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        public Dictionary<CardLocation, List<Transform>> cardLocations = new Dictionary<CardLocation, List<Transform>>();

        [SerializeField]
        [TabGroup( "Cards" )]
        public int playerNumber;

        [SerializeField]
        [TabGroup( "Cards" )]
        public CardCollection deck;

        [SerializeField]
        [TabGroup( "Cards" )]
        public CardCollection hand = new CardCollection();

        [SerializeField]
        [TabGroup( "Cards" )]
        public CardCollection graveyard = new CardCollection();

        [SerializeField]
        [TabGroup( "Cards" )]
        public CardCollection sheildZone = new CardCollection();

        [SerializeField]
        [TabGroup( "Cards" )]
        public CardCollection manaZone = new CardCollection();

        [SerializeField]
        [TabGroup( "Cards" )]
        public CardCollection battleZone = new CardCollection();

        [HideInInspector]
        public PhotonView photonView;

        public override void InitializeComponent()
        {
            if(photonView == null)
            {
                photonView = GetComponent<PhotonView>();
            }

            DuelManager.instance.cardDrawnEvent.AddListener( OnNeedsToSpawnCard );
            DuelManager.instance.shieldAddedEvent.AddListener( OnNeedsToSpawnCard );
        }

        public override void DisableComponent()
        {
            DuelManager.instance.cardDrawnEvent.RemoveListener( OnNeedsToSpawnCard );
            DuelManager.instance.shieldAddedEvent.RemoveListener( OnNeedsToSpawnCard );
        }

        private void Start()
        {
            SetupDeck();
            DuelManager.instance.RegisterRemotePlayer(this);
        }

        private void Update()
        {
            UpdateCardPosition();
        }

        public void SpawnCard( Card card )
        {
            GameObject _spawnedCard = ActorManager.SpawnActor( cardIdentity, cardSpawnPoint.position, cardSpawnPoint.rotation, transform );
            if ( _spawnedCard != null )
            {
                CardComponent _cardComponent = _spawnedCard.GetComponentInChildren<CardComponent>();
                if(_cardComponent != null)
                {
                    _cardComponent.AssignCard( card );
                    spawnedCards.Add( _cardComponent );
                }
            }
        }

        public void DespawnCard(Card card)
        {
            CardComponent _cardActorCom = spawnedCards.Find( _card => _card.card.instanceId == card.instanceId );
            if(_cardActorCom != null)
            {
                spawnedCards.Remove( _cardActorCom );
                ActorManager.Despawn( _cardActorCom.owner );
            }
        }

        public void DespawnAll()
        {
            foreach(CardComponent _card in spawnedCards)
            {
                ActorManager.Despawn( _card.owner );
            }

            spawnedCards.Clear();
        }

        private void OnNeedsToSpawnCard(DuelistComponent player, Card card)
        {
            if (player == this)
            {
                SpawnCard( card );
            }
        }

        private void UpdateCardPosition()
        {
            if(spawnedCards.Count == 0)
            {
                return;
            }

            Dictionary<CardLocation, int> locationIndexMap = new Dictionary<CardLocation, int>()
            {
                { CardLocation.Hand, 0 },
                { CardLocation.Deck, 0 },
                { CardLocation.BattleZone, 0 },
                { CardLocation.ShieldZone, 0 },
                { CardLocation.Graveyard, 0 },
                { CardLocation.ManaZone, 0 },
            };

            foreach ( CardComponent _card in spawnedCards )
            {
                if(_card.externallyManipulated)
                {
                    //locationIndexMap[_card.card.currentLocation]++;
                    continue;
                }

                int _index = locationIndexMap[_card.card.currentLocation];
                if ( cardLocations[_card.card.currentLocation].Count > _index)
                {
                    _card.transform.position = Vector3.Lerp( _card.transform.position, cardLocations[_card.card.currentLocation][_index].position, cardMoveSpeed );
                    _card.transform.rotation = Quaternion.Lerp( _card.transform.rotation, cardLocations[_card.card.currentLocation][_index].rotation, cardMoveSpeed );

                    locationIndexMap[_card.card.currentLocation]++;
                }
            }
        }

        #region DECK SETUP
        /// <summary>
        ///     Creates a deck then replicates in over the network
        /// </summary>
        public void SetupDeck()
        {
            if(photonView.IsMine)
            {
                deck = testDeck.GenerateDeckInstance( this );

                List<string> _cardIds = new List<string>();
                List<string> _instanceIds = new List<string>();

                foreach(Card _card in deck.cards)
                {
                    if(!string.IsNullOrEmpty(_card.data.cardId))
                    {
                        _cardIds.Add( _card.data.cardId );
                        _instanceIds.Add( _card.instanceId.ToString() );
                    }
                }

                photonView.RPC( "SetupDeckOverNetwork", RpcTarget.Others, _cardIds.ToArray(), _instanceIds.ToArray() );
            }
        }

        /// <summary>
        ///     This function will be called after SetupDeck. It will send that data over the server to the opponents machine. 
        /// </summary>
        [PunRPC]
        public void SetupDeckOverNetwork(string[] cardIds, string[] cardInstanceIds)
        {
            Dictionary<CardData, int> _collection = new Dictionary<CardData, int>();

            for(int i = 0; i < cardIds.Length; i++)
            {
                CardData _data = cardDatabase.GetById( cardIds[i] );
                if(_collection.ContainsKey(_data))
                {
                    _collection[_data]++;
                }
                else
                {
                    _collection.Add( _data, 1 );
                }
            }

            deck = new CardCollection( _collection, this, cardInstanceIds );
        }
        #endregion

        #region SHUFFLING
        public void ShuffleCards() // becuase this is random, we will do it on the local and replicate those results
        {
            if(photonView.IsMine)
            {
                int[] cardindexes = deck.Shuffle( repositionCards: true );
                photonView.RPC( "SetDeckIndexesRPC", RpcTarget.Others, cardindexes );
            }
            else
            {
                Debug.Log( "Cannot shuffle a deck that I do not own" );
            }
        }

        [PunRPC]
        public void SetDeckIndexesRPC( int[] indexes )
        {
            deck.SetCardIndexes( indexes, repositionCards: true );
        }
        #endregion
    }
}