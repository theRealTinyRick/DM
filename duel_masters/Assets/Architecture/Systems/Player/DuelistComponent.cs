/*
 Author: Aaron Hines
 Description: represents the player in the scene. Handles spawning and despawning cards. Some coroutines may be called here too
*/
using System.Collections.Generic;
using UnityEngine;

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
        private float cardSpeed;

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        public Dictionary<CardLocation, List<Transform>> cardLocations = new Dictionary<CardLocation, List<Transform>>();

        [HideInInspector]
        public Deck deckData;

        [SerializeField]
        public int playerNumber;

        [SerializeField]
        public CardCollection deck;

        [SerializeField]
        public CardCollection hand = new CardCollection();

        [SerializeField]
        public CardCollection graveyard = new CardCollection();

        [SerializeField]
        public CardCollection sheildZone = new CardCollection();

        [SerializeField]
        public CardCollection manaZone = new CardCollection();

        [SerializeField]
        public CardCollection battleZone = new CardCollection();

        [SerializeField]
        private List<CardComponent> spawnedCards = new List<CardComponent>();

        public override void InitializeComponent()
        {
            DuelManager.instance.cardDrawnEvent.AddListener( SpawnNewCard );
            DuelManager.instance.shieldAddedEvent.AddListener( SpawnNewCard );

            DuelManager.instance.RegisterRemotePlayer(this);
        }

        public override void DisableComponent()
        {
            DuelManager.instance.cardDrawnEvent.RemoveListener( SpawnNewCard );
            DuelManager.instance.shieldAddedEvent.RemoveListener( SpawnNewCard );
        }

        public void SetupDuelist( Deck deck, int playerNumber = 0 )
        {
            deckData = deck;
            this.deck = deck.GenerateDeckInstance( this );
            this.playerNumber = playerNumber;
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

        private void Update()
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
                    _card.transform.position = Vector3.Lerp( _card.transform.position, cardLocations[_card.card.currentLocation][_index].position, cardSpeed );
                    _card.transform.rotation = Quaternion.Lerp( _card.transform.rotation, cardLocations[_card.card.currentLocation][_index].rotation, cardSpeed );

                    locationIndexMap[_card.card.currentLocation]++;
                }
            }
        }
    }
}