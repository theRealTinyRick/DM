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
    public class PlayerComponent_DM : ActorComponent
    {
        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        private Identity cardIdentity;

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private Transform cardSpawnPoint;

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private Dictionary<CardLocation, List<Transform>> cardLocations = new Dictionary<CardLocation, List<Transform>>();

        private Player player;

        [SerializeField]
        private List<CardComponent> spawnedCards = new List<CardComponent>();

        public override void InitializeComponent()
        {
            DuelManager.instance.cardDrawnEvent.AddListener( SpawnNewCard );
            DuelManager.instance.shieldAddedEvent.AddListener( SpawnNewCard );
        }

        public override void DisableComponent()
        {
            DuelManager.instance.cardDrawnEvent.RemoveListener( SpawnNewCard );
            DuelManager.instance.shieldAddedEvent.RemoveListener( SpawnNewCard );
        }

        public void AssignPlayer(Player player)
        {
            this.player = player;
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
        private void SpawnNewCard(Player player, Card card)
        {
            if (player == this.player)
            {
                SpawnCard( card );
            }
        }

        private void Update()
        {
            for ( int _i = 0; _i < spawnedCards.Count; _i++ )
            {
                Debug.Log( _i );

                CardComponent _card = spawnedCards[_i];

                if ( cardLocations.ContainsKey( _card.card.currentLocation ) )
                {
                    if ( cardLocations[_card.card.currentLocation].Count > _i )
                    {
                        _card.transform.position = Vector3.Lerp( _card.transform.position, cardLocations[_card.card.currentLocation][_i].position, 0.5f );
                        _card.transform.rotation = Quaternion.Lerp( _card.transform.rotation, cardLocations[_card.card.currentLocation][_i].rotation, 0.5f );
                    }
                }
            }
        }
    }
}