/*
 Author: Aaron Hines
 Edits By: 
 Description: Manages the duel. This will spawn in players and have events for listening
 */
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Photon.Pun;
using Sirenix.OdinInspector;

using GameFramework.Networking;
using GameFramework.Phases;

using DM.Systems.Players;
using DM.Systems.GameEvents;
using DM.Systems.Actions;
using DM.Systems.Turns;

namespace DM.Systems
{
    public class DuelManager : Singleton_MonobehaviourPunCallbacks<DuelManager>
    {
        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private string playerPrefabName;

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        public List<PlayerComponent> players = new List<PlayerComponent>();

        private ActionManager actionManager
        {
            get => ActionManager.instance;
        }


        private PhaseManager phaseManager
        {
            get => PhaseManager.instance;
        }

        private TurnManager turnManager
        {
            get => TurnManager.instance;
        }

        public PlayerComponent player1
        {
            get
            {
                return players.Find( _duelist => _duelist.playerNumber == 1 );
            }
        }

        public PlayerComponent player2
        {
            get
            {
                return players.Find( _duelist => _duelist.playerNumber == 2 );
            }
        }

        public PlayerComponent localPlayer
        {
            get
            {
                return players.Find( _duelist => _duelist.GetComponent<PhotonView>().IsMine );
            }
        }

        public PlayerComponent remotePlayer
        {
            get
            {
                return players.Find( _duelist => !_duelist.GetComponent<PhotonView>().IsMine );
            }
        }

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

        protected override void Enable()
        {
            NetworkManager.instance.allPlayerLevelsLoadedEvent.AddListener( StartGame );
        }

        protected override void Disable()
        {
            NetworkManager.instance.allPlayerLevelsLoadedEvent.RemoveListener( StartGame );
        }

        public void StartGame()
        {
            // this should call the function to spawn a player - then wait for the next player to be spawned
            SpawnLocalPlayer();
        }

        public void Update() // need to check if master client and only do this stuff on there
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                DoStartStuff();
            }

            if(Input.GetKeyDown(KeyCode.Return))
            {
                StartDuel();
            }
        }

        public PlayerComponent GetPlayer( int playerNumber )
        {
            return players.Find( _player => _player.playerNumber == playerNumber );
        }

        private void SpawnLocalPlayer()
        {
            PlayerComponent _localPlayer = PhotonNetwork.Instantiate( playerPrefabName, Vector3.zero, Quaternion.identity ).GetComponent<PlayerComponent>();

            if ( NetworkManager.instance.isHost )
            {
                _localPlayer.playerNumber = 1;
            }
            else
            {
                _localPlayer.playerNumber = 2;
            }

            players.Add( _localPlayer );
        }

        public void RegisterRemotePlayer( PlayerComponent remotePlayer )
        {
            PhotonView _view = remotePlayer.GetComponent<PhotonView>();
            if ( _view != null )
            {
                if ( !_view.IsMine )
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

                    remotePlayer.transform.rotation = new Quaternion( 0, 180, 0, 0 );

                    remotePlayer.GetComponentInChildren<Camera>().gameObject.SetActive( false );
                    remotePlayer.GetComponentInChildren<CardManipulation.CardManipulatorComponent>().enabled = false;
                }
            }
        }

        public void DoStartStuff()
        {
            if(players.Count >= Constants.MIN_PLAYER_COUNT && PhotonNetwork.IsMasterClient)
            {
                StartCoroutine( StartStuffRoutine() );
            }
        }

        private IEnumerator StartStuffRoutine()
        {
            foreach ( PlayerComponent _player in players )
            {
                actionManager.TriggerAddShieldsFromDeck( _player, 5, false );
            }

            yield return new WaitForSeconds( 4 );

            foreach ( PlayerComponent _player in players )
            {
                actionManager.TriggerDraw( _player, 5, false );
            }
        }

        public void StartDuel()
        {
            turnManager.Init();
            phaseManager.StartPhases();
        }
    }
}
