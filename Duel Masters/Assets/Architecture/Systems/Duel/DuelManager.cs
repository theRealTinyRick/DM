using UnityEngine;
using Sirenix.OdinInspector;

using GameFramework;

using DM.Systems;
using DM.Systems.Players;
using DM.Systems.Actions;
using DM.Systems.Cards;

namespace Systems.Duel
{
    public class DuelManager: SerializedMonoBehaviour
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

        [TabGroup( "Setup" )]
        [SerializeField]
        private PlayerComponent_DM player1Component;

        [TabGroup( "Setup" )]
        [SerializeField]
        private PlayerComponent_DM player2Component;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        private Player[] players = new Player[2];

        private PlayerComponent_DM[] playerComponentArray = new PlayerComponent_DM[2];

        public Player player1
        {
            get
            {
                if(players != null && players.Length > 1)
                {
                    return players[0];
                }

                return null;
            }
        }

        public Player player2
        {
            get
            {
                if ( players != null && players.Length > 1 )
                {
                    return players[1];
                }

                return null;
            }
        }

        [Button]
        public void StartDuel()
        {
            players[0] = new Player( player1Deck, 0 );
            players[1] = new Player( player2Deck, 1 );

            player1Component.AssignPlayer( players[0] );
            player2Component.AssignPlayer( players[1] );

            players = new Player[]{ player1, player2};
            playerComponentArray = new PlayerComponent_DM[] { player1Component, player2Component };

            foreach(Player _player in players)
            {
                _player.deck.Shuffle();
                Actions.AddToShieldsFromTopOfDeck( _player, DM.Systems.Constants.STARTING_SHIELD_COUNT + 2 );
                Actions.Draw( _player, DM.Systems.Constants.STARTING_HAND_COUNT );
            }
        }
    }
}
