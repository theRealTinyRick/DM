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
using System.Collections;

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
                return players.Find( _duelist => _duelist.playerNumber == 1 );
            }
        }

        public DuelistComponent player2
        {
            get
            {
                return players.Find( _duelist => _duelist.playerNumber == 2 );
            }
        }

        protected override void Enable()
        {
            phaseManager = GetComponentInChildren<PhaseManager>();

            NetworkManager.instance.allPlayerLevelsLoadedEvent.AddListener( StartGame );
        }

        protected override void Disable()
        {
            NetworkManager.instance.allPlayerLevelsLoadedEvent.RemoveListener( StartGame );
        }

        public void StartGame()
        {
            DuelistComponent _localPlayer = PhotonNetwork.Instantiate( playerPrefabName, Vector3.zero, Quaternion.identity ).GetComponent<DuelistComponent>();

            if(NetworkManager.instance.isHost)
            {
                _localPlayer.playerNumber = 1;
            }
            else
            {
                _localPlayer.playerNumber = 2;
            }

            players.Add(_localPlayer);
        }

        public void RegisterRemotePlayer(DuelistComponent remotePlayer)
        {
            PhotonView _view = remotePlayer.GetComponent<PhotonView>();
            if(_view != null)
            {
                if(!_view.IsMine)
                {
                    players.Add( remotePlayer );

                    if ( NetworkManager.instance.isHost )
                    {
                        remotePlayer.playerNumber = 2;
                    }
                    else
                    {
                        remotePlayer.playerNumber = 1;
                    }

                    // rotate the remove player
                    remotePlayer.transform.rotation = new Quaternion( 0, 180, 0, 0 );
                    remotePlayer.GetComponentInChildren<Camera>().gameObject.SetActive( false );
                }
            }

            gameStartedEvent?.Invoke();
            phaseManager?.StartPhases();

            StartDuel();
        }

        public void StartDuel()
        {
            // shuffle decks
            foreach(DuelistComponent _player in players)
            {
                _player.SetupDuelist( playerDeck );
                _player.ShuffleCards();
            }

            // add shields
            // draw cards
            StartCoroutine( RunSetup() );
        }
        
        private IEnumerator RunSetup()
        {
            yield return new WaitForEndOfFrame();
        }

    }
}
