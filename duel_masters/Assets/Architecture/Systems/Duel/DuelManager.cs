/*
 Author: Aaron Hines
 Edits By: 
 Description: Manages the duel
 */
using UnityEngine;
using Sirenix.OdinInspector;

using GameFramework;
using GameFramework.Phases;

using DM.Systems.Players;
using DM.Systems.Cards;
using DM.Systems.GameEvents;
using DM.Systems.Actions;

namespace DM.Systems
{
    public class DuelManager : Singleton_MonobehaviourPunCallbacks<DuelManager>
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

        #region Events
        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public GameStartedEvent gameStartedEvent = new GameStartedEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public GameEndedEvent gameEndedEvent = new GameEndedEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public TurnChangedEvent turnChangedEvent = new TurnChangedEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public StartPhaseEvent startPhaseEnteredEvent = new StartPhaseEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public StartPhaseEvent startPhaseExitedEvent = new StartPhaseEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public DrawPhaseEvent drawPhaseEnteredEvent = new DrawPhaseEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public DrawPhaseEvent drawPhaseExitedEvent = new DrawPhaseEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public ManaPhaseEvent manaPhaseEnteredEvent = new ManaPhaseEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public ManaPhaseEvent manaPhaseExitedEvent = new ManaPhaseEvent();

        [SerializeField]
        [TabGroup( Tabs.EVENTS )]
        public MainPhaseEvent mainPhaseEnteredEvent = new MainPhaseEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public MainPhaseEvent mainPhaseExitedEvent = new MainPhaseEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public BattlePhaseEvent battlePhaseEnteredEvent = new BattlePhaseEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public BattlePhaseEvent battlePhaseExitedEvent = new BattlePhaseEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public EndPhaseEvent endPhaseEnteredEvent = new EndPhaseEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public EndPhaseEvent endPhaseExitedEvent = new EndPhaseEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public CreatureSummonedEvent creatureSummonedEvent = new CreatureSummonedEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public CardDrawnEvent cardDrawnEvent = new CardDrawnEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public ShieldAddedEvent shieldAddedEvent = new ShieldAddedEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public ShieldBrokenEvent shieldBrokenEvent = new ShieldBrokenEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public ManaAddedEvent manaAddedEvent = new ManaAddedEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public CardTappedEvent cardTappedEvent = new CardTappedEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public CardUntappedEvent cardUntappedEvent = new CardUntappedEvent();
        #endregion

        public PhaseManager phaseManager
        {
            get;
            private set;
        }

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

        public int currentPlayerIndex
        {
            get;
            private set;
        } = 0;

        public Player currentTurnPlayer
        {
            get;
            private set;
        }

        protected override void Enable()
        {
            phaseManager = GetComponentInChildren<PhaseManager>();
        }

        protected override void Disable()
        {
        }

        [Button]
        public void StartDuel()
        {
            players[0] = new Player( player1Deck, 0 );
            players[1] = new Player( player2Deck, 1 );

            player1Component.AssignPlayer( players[0] );
            player2Component.AssignPlayer( players[1] );

            players = new Player[]{ player1, player2 };
            playerComponentArray = new PlayerComponent_DM[] { player1Component, player2Component };

            foreach(Player _player in players)
            {
                _player.Enable();

                _player.deck.Shuffle();

                Action.AddToShieldsFromTopOfDeck( _player, Constants.STARTING_SHIELD_COUNT );
                Action.Draw( _player, Constants.STARTING_HAND_COUNT );
            }

            currentPlayerIndex = 0;
            SetCurrentTurnPlayer( players[currentPlayerIndex] );

            gameStartedEvent?.Invoke();
            phaseManager?.StartPhases();
        }

        public void NextPlayersTurn()
        {
            currentPlayerIndex++;
            if(currentPlayerIndex >= players.Length)
            {
                currentPlayerIndex = 0;
            }

            SetCurrentTurnPlayer( players[currentPlayerIndex] );
        }

        public void SetCurrentTurnPlayer( Player player )
        {
            currentTurnPlayer = player;
            turnChangedEvent?.Invoke( player );
        }
    }
}
