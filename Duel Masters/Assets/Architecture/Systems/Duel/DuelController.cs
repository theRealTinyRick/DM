using UnityEngine;
using Sirenix.OdinInspector;

using GameFramework;
using GameFramework.Actors;

using DM.Systems.Players;
using DM.Systems.Cards;

namespace Systems.Duel
{
    public class DuelController : SerializedMonoBehaviour
    {
        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private Identity playerIdentity;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public Deck player1Deck;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public Deck player2Deck;

        [TabGroup( "Spawn Points" )]
        [SerializeField]
        public Transform player1SpawnPoint;

        [TabGroup( "Spawn Points" )]
        [SerializeField]
        public Transform player2SpawnPoint;

        [TabGroup( "Spawn Points" )]
        [SerializeField]
        public Transform cardSpawnPoint;

        [SerializeField]
        private Player[] playerArray = new Player[2];
        private DM_PlayerComponent[] playerComponentArray = new DM_PlayerComponent[2];

        public Player player1
        {
            get
            {
                if(playerArray != null && playerArray.Length > 1)
                {
                    return playerArray[0];
                }

                return null;
            }
        }

        public Player player2
        {
            get
            {
                if ( playerArray != null && playerArray.Length > 1 )
                {
                    return playerArray[1];
                }

                return null;
            }
        }

        [Button]
        public void StartDuel()
        {
            playerArray[0] = new Player( player1Deck );
            playerArray[1] = new Player( player2Deck );

            GameObject _player1Actor = ActorManager.SpawnActor( playerIdentity, player1SpawnPoint );
            GameObject _player2Actor = ActorManager.SpawnActor( playerIdentity, player2SpawnPoint );
            DM_PlayerComponent _player1Component = _player1Actor.GetComponentInChildren<DM_PlayerComponent>();
            DM_PlayerComponent _player2Component = _player2Actor.GetComponentInChildren<DM_PlayerComponent>();
            _player1Component.AssignPlayer( playerArray[0] );
            _player2Component.AssignPlayer( playerArray[1] );

            playerArray = new Player[]{ player1, player2};
            playerComponentArray = new DM_PlayerComponent[] { _player1Component, _player2Component };

            foreach(Card _card in player1.deck.cards)
            {
                _player1Component.SpawnCard( _card, cardSpawnPoint );
            }

            foreach ( Card _card in player2.deck.cards )
            {
                _player2Component.SpawnCard( _card, cardSpawnPoint );
            }
        }

        public void SpawnDeck()
        {
        }
    }
}
