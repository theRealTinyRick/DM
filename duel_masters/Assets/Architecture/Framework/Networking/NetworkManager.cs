/*
 Author: Aaron Hines
 Edits By: 
 Description: holder for all network events
 */
using System.Collections.Generic;

using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Sirenix.OdinInspector;

namespace GameFramework.Networking
{
    public class NetworkManager : Singleton_MonobehaviourPunCallbacks<NetworkManager>, ILobbyCallbacks
    {
        #region UNITY SERIALIZED PROPERTIES
        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private DebugLevel debugLevel;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        private GameObject roomListingsprefab;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        private Transform roomsPanel;

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private string gameSceneName = "";

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        private int maxPlayerCount = 2;

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private bool autoStartGame;
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
        #endregion

        #region PROPERTIES
        public string roomName
        {
            get;
            private set;
        }
        #endregion

        public void Connect()
        {
            if(!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.AutomaticallySyncScene = true; // TODO: move this somewhere else. We don't ALWAYS want this option
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        // call these to functions to 
        #region PUBLIC FUNCIONS 
        public void OnRoomNameChanged( string name ) // TODO: move this somewhere else to handle ui changes - then call create room and joing room with the string passed in
        {
            roomName = name;
        }

        private void RemoveRoomListings()
        {
            while(roomsPanel.childCount != 0)
            {
                Destroy( roomsPanel.GetChild(0).gameObject );
            }
        }

        private void ListRoom( RoomInfo room )
        {
            if(room.IsOpen && room.IsVisible)
            {
                GameObject _listing = Instantiate( roomListingsprefab, roomsPanel );
                RoomButton _button = _listing.GetComponent<RoomButton>();
                _button.roomName = room.Name;
                _button.roomSize = room.MaxPlayers;

                _button.SetRoom();
            }
        }

        public void StartGame()
        {
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
        }

        public override void OnJoinedRoom()
        {
            if ( debugLevel > DebugLevel.None )
            {
                Debug.Log( "Network Manager:  OnJoinedRoom() called by PUN. Now this client is in a room." );
            }
        }

        public override void OnJoinedLobby()
        {
            {
                Debug.Log( "Network Manager:  OnJoinedLobby() called by PUN. Now this client is in a lobby." );
            }
        }

        public override void OnRoomListUpdate( List<RoomInfo> roomList )
        {
            foreach ( RoomInfo _room in roomList )
            {
                ListRoom( _room );
            }
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

            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;

            if(autoStartGame)
            {
                StartGame();
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
    }
}

