using System.Collections;
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
    public class DM_PlayerComponent : ActorComponent
    {
        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        private Identity cardIdentity;

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private Dictionary<CardLocation, Transform> cardLocations;

        private Player player;
        private List<CardComponent> spawnedCards = new List<CardComponent>();

        public override void DisableComponent() {}
        public override void InitializeComponent() {}

        public void AssignPlayer(Player player)
        {
            this.player = player;
        }

        public void SpawnCard( Card card, Transform spawnpoint = null)
        {
            GameObject _spawnedCard = ActorManager.SpawnActor( cardIdentity, spawnpoint );
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
    }
}