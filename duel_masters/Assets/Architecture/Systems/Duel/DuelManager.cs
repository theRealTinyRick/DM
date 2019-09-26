/*
 Author: Aaron Hines
 Edits By: 
 Description: Manages the duel
 */
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Sirenix.OdinInspector;

using GameFramework;
using GameFramework.Networking;
using GameFramework.Phases;

using DM.Systems.Players;
using DM.Systems.Cards;
using DM.Systems.GameEvents;
using DM.Systems.Actions;
using System.Collections.Generic;

namespace DM.Systems
{
    public class DuelManager : Singleton_MonobehaviourPunCallbacks<DuelManager>
    {
        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private string playerPrefabName;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public Deck playerDeck;

        [TabGroup( "Setup" )]
        [SerializeField]
        private DuelistComponent player1Component;

        [TabGroup( "Setup" )]
        [SerializeField]
        private DuelistComponent player2Component;

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private List<DuelistComponent> players = new List<DuelistComponent>();
        private DM.Systems.Players.DuelistComponent[] playerComponentArray = new DuelistComponent[2];

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

        public DuelistComponent player1
        {
            get
            {
                if(players != null && players.Count > 1)
                {
                    return players[0];
                }

                return null;
            }
        }

        public DuelistComponent player2
        {
            get
            {
                if ( players != null && players.Count > 1 )
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

        public DuelistComponent currentTurnPlayer
        {
            get;
            private set;
        }

        protected override void Enable()
        {
            phaseManager = GetComponentInChildren<PhaseManager>();

            NetworkManager.instance.allPlayerLevelsLoadedEvent.AddListener( StartDuel );
        }

        protected override void Disable()
        {
            NetworkManager.instance.allPlayerLevelsLoadedEvent.RemoveListener( StartDuel );
        }

        public void StartDuel()
        {
            players.Add(PhotonNetwork.Instantiate( playerPrefabName, Vector3.zero, Quaternion.identity).GetComponent<DuelistComponent>());
        }

        public void RegisterRemotePlayer(DuelistComponent duelistComponent)
        {
            //foreach( DuelistComponent _player in players)
            //{
            //    _player.deck.Shuffle();
            //    Action.AddToShieldsFromTopOfDeck( _player, Constants.STARTING_SHIELD_COUNT );
            //    Action.Draw( _player, Constants.STARTING_HAND_COUNT );
            //}

            if(!players.Contains(duelistComponent))
            {
                players.Add( duelistComponent );
            }
            else
            {
                Debug.LogError( "WHAT" );
            }

            gameStartedEvent?.Invoke();
            phaseManager?.StartPhases();
        }
    }
}
