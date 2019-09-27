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

namespace DM.Systems.Players
{
    public class DuelistComponent : ActorComponent
    {
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
        public Deck deckData;

        [HideInInspector]
        public PhotonView photonView;

        public override void InitializeComponent()
        {
            if(photonView == null)
            {
                photonView = GetComponent<PhotonView>();
            }

            DuelManager.instance.cardDrawnEvent.AddListener( SpawnNewCard );
            DuelManager.instance.shieldAddedEvent.AddListener( SpawnNewCard );

            DuelManager.instance.RegisterRemotePlayer(this);
        }

        public override void DisableComponent()
        {
            DuelManager.instance.cardDrawnEvent.RemoveListener( SpawnNewCard );
            DuelManager.instance.shieldAddedEvent.RemoveListener( SpawnNewCard );
        }

        private void Update()
        {
            UpdateCardPosition();
        }

        public void SetupDuelist( Deck deck )
        {
            deckData = deck;
            this.deck = deck.GenerateDeckInstance( this );
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
            CardComponent _cardActorCom = spawnedCards.Find( _card => _card.card.id == card.id );
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

        // Event callbacks
        private void SpawnNewCard(DuelistComponent player, Card card)
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

        #region Actions
        [PunRPC]
        public void ShuffleCards()
        {
            if(photonView.IsMine) // only call this function on the owning player - then set the card indexes on both
            {
                int[] cardindexes = deck.Shuffle();
                photonView.RPC( "SetDeckIndexes", RpcTarget.Others, cardindexes );
            }
            else
            {
                Debug.Log( "Cannot shuffle a deck that I do not own" );
            }
        }

        [PunRPC]
        public void SetDeckIndexes(int[] indexes)
        {
            deck.SetCardIndexes( indexes );
        }
        #endregion
    }
}