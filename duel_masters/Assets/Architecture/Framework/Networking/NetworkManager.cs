/*
 Author: Aaron Hines
 Edits By: 
 Description: manages primary connection. Other classes should listen to this scripts events rather than photon to prevent unnessisay coupling to a thirdparty system.
 */
using System.Collections.Generic;

using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

namespace GameFramework.Networking
{
    public class NetworkManager : Singleton_MonobehaviourPunCallbacks<NetworkManager>, ILobbyCallbacks
    {
        #region UNITY SERIALIZED PROPERTIES
        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private DebugLevel debugLevel;

        [TabGroup( Tabs.PROPERTIES )]
        [Tooltip("Connection to master will be made on start")]
        [SerializeField]
        private bool connectOnLoad;

        [TabGroup( Tabs.PROPERTIES )]
        [Tooltip("When the max number of players in the room is reached, go ahead and start the game (load the scene)")]
        [SerializeField]
        private bool autoStartGame;

        [TabGroup( "Scenes" )]
        [SerializeField]
        private string gameSceneName = "";

        [TabGroup( "Scenes" )]
        [SerializeField]
        private string lobbyScene = "";

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        private int maxPlayerCount = 2;

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private int minPlayerCount = 2;
        #endregion

        #region NETWORK EVENTS
        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public ConnectedToMasterEvent connectedToMasterEvent = new ConnectedToMasterEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public DisconnectedFromMasterEvent disconnectedFromMasterEvent = new DisconnectedFromMasterEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public LobbyJoinedEvent lobbyJoinedEvent = new LobbyJoinedEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public LeftLobbyEvent leftLobbyEvent = new LeftLobbyEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public RoomJoinedEvent roomJoinedEvent = new RoomJoinedEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public LeftRoomEvent leftRoomEvent = new LeftRoomEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public PlayerEnteredRoomEvent playerEnteredRoomEvent = new PlayerEnteredRoomEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public PlayerExitedRoomEvent playerExitedRoomEvent = new PlayerExitedRoomEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public RoomListUpdateEvent roomListUpdateEvent = new RoomListUpdateEvent();

        [TabGroup( Tabs.EVENTS )]
        [SerializeField]
        public AllPlayerLevelsLoadedEvent allPlayerLevelsLoadedEvent = new AllPlayerLevelsLoadedEvent();
        #endregion

        #region PROPERTIES
        public string roomName
        {
            get;
            private set;
        }

        public int playerLoadedInCount
        {
            get;
            private set;
        } = 0;

        public bool allPlayersLoadedIn
        {
            get => playerLoadedInCount >= maxPlayerCount;
        }
        #endregion

        private void Start()
        {
            if(connectOnLoad)
            {
                Connect();
            }
        }

        // call these to functions to 
        #region PUBLIC FUNCIONS 
        public void Connect()
        {
            if(!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.AutomaticallySyncScene = true; // TODO: move this somewhere else. We don't ALWAYS want this option
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public void StartGame()
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel( gameSceneName ); 
        }

        public void CreateRoom(string roomName = "")
        {
            if(PhotonNetwork.IsConnected && !PhotonNetwork.InRoom)
            {
                RoomOptions _roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)maxPlayerCount };
                PhotonNetwork.CreateRoom( string.IsNullOrEmpty(roomName) ? this.roomName : roomName, _roomOps );
            }
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene( lobbyScene ); // display some UI here later
            playerLoadedInCount = 0;
        }

        public void OnLevelInitialized()
        {
            GetComponent<PhotonView>().RPC( "RegisterLevelLoadedRPC", RpcTarget.All );
        }
        #endregion

        #region PRIVATE FUNCTIONS
        #endregion

        #region PUN CALLBACKS
        public override void OnConnectedToMaster()
        {
            PhotonNetwork.AutomaticallySyncScene = true;

            if(debugLevel > DebugLevel.None)
            {
                Debug.Log( "Network Manager:  OnConnectedToMaster() was called by PUN" );
            }

            if ( !PhotonNetwork.InLobby )
            {
                PhotonNetwork.JoinLobby();
            }

            connectedToMasterEvent?.Invoke();
        }

        public override void OnJoinedRoom()
        {
            if ( debugLevel > DebugLevel.None )
            {
                Debug.Log( "Network Manager:  OnJoinedRoom() called by PUN. Now this client is in a room." );
            }

            roomJoinedEvent?.Invoke();
        }

        public override void OnJoinedLobby()
        {
            if ( debugLevel > DebugLevel.None )
            {
                Debug.Log( "Network Manager:  OnJoinedLobby() called by PUN. Now this client is in a lobby." );
            }

            lobbyJoinedEvent?.Invoke();
        }

        public override void OnRoomListUpdate( List<RoomInfo> roomList )
        {
            roomListUpdateEvent?.Invoke( roomList );
        }

        public override void OnPlayerEnteredRoom( Player other )
        {
            if ( debugLevel > DebugLevel.None )
            {
                Debug.LogFormat( "OnPlayerEnteredRoom() {0}", other.NickName ); // not seen if you're the player connecting
                if ( PhotonNetwork.IsMasterClient )
                {
                    Debug.LogFormat( "OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient ); // called before OnPlayerLeftRoom
                }
            }

            playerEnteredRoomEvent?.Invoke( other );

            if ( PhotonNetwork.PlayerList.Length >= maxPlayerCount )
            {
                if(autoStartGame) /// TODO: count players
                {
                    StartGame();
                }
            }
        }

        public override void OnPlayerLeftRoom( Player other )
        {
            if ( debugLevel > DebugLevel.None )
            {
                Debug.LogFormat( "OnPlayerLeftRoom() {0}", other.NickName ); // seen when other disconnects
                if ( PhotonNetwork.IsMasterClient )
                {
                    Debug.LogFormat( "OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient ); // called before OnPlayerLeftRoom
                }
                playerExitedRoomEvent?.Invoke( other );

                if (PhotonNetwork.PlayerList.Length <= minPlayerCount)
                {
                    LeaveRoom();
                }
            }
        }

        public override void OnCreateRoomFailed( short returnCode, string message )
        {
            if ( debugLevel > DebugLevel.None )
            {
                Debug.Log( "Room could not be created: " + message );
            }
        }
        #endregion

        #region RPCs
        [PunRPC]
        private void RegisterLevelLoadedRPC()
        {
            playerLoadedInCount++;
            if(playerLoadedInCount >= maxPlayerCount)
            {
                allPlayerLevelsLoadedEvent?.Invoke();
            }
        }
        #endregion
    }
}

