/*
 Author: Aaron Hines
 Edits By: 
 Description: Manages connections and incoming data from photon
*/
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;
using Sirenix.OdinInspector;

namespace GameFramework.Netorking.Photon
{
    public class NetworkManager : Singleton_MonobehaviourPunCallbacks<NetworkManager>
    {
        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private string gameVersion = "1"; // TODO: do this with a settings window later

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private byte maxPlayersPerRoom = 4; // TODO: do this with a settings window later

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private string launcherSceneName = "";

        #region Network Events
        public ConnectedToMasterEvent connectedToMasterEvent
        {
            get;
            private set;
        } = new ConnectedToMasterEvent();

        public DisconnectedFromMasterEvent disconnectedFromMasterEvent
        {
            get;
            private set;
        } = new DisconnectedFromMasterEvent();

        public RoomJoinedEvent roomJoinedEvent
        {
            get;
            private set;
        } = new RoomJoinedEvent();


        public LeftRoomEvent leftRoomEvent
        {
            get;
            private set;
        } = new LeftRoomEvent();

        public LobbyJoinedEvent lobbyJoinedEvent
        {
            get;
            private set;
        } = new LobbyJoinedEvent();

        public LeftLobbyEvent leftLobbyEvent
        {
            get;
            private set;
        } = new LeftLobbyEvent();

        public PlayerEnteredRoomEvent playerEnteredRoomEvent
        {
            get;
            private set;
        } = new PlayerEnteredRoomEvent();

        public PlayerExitedRoomEvent playerExitedRoomEvent
        {
            get;
            private set;
        } = new PlayerExitedRoomEvent();
        #endregion

        public bool isConnectedToMaster
        {
            get;
            private set;
        }

        public bool isInRoom
        {
            get;
            private set;
        }

        public bool isMasterClient
        {
            get
            {
                return PhotonNetwork.IsMasterClient;
            }
        }

        private bool isConnecting = false;

        protected override void Enable()
        {
            base.Enable();
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start()
        {
            Connect();
        }

        #region Connecting and joining
        [Button]
        public void Connect()
        {
            isConnecting = true;
            PhotonNetwork.GameVersion = gameVersion; // TODO: do this with settings!
            PhotonNetwork.ConnectUsingSettings();
        }

        [Button]
        public void Disconnect()
        {
            PhotonNetwork.Disconnect();
        }

        [Button]
        public void CreateRoom()
        {
            PhotonNetwork.CreateRoom( null, new RoomOptions { MaxPlayers = maxPlayersPerRoom } );
        }

        [Button]
        public void JoinRandomRoom()
        {
            if(PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                Debug.LogError( "Cannot join a room with out first connecting to the network" );
            }
        }

        [Button]
        public void JoinRoom (/*args here for what room to joing*/)
        {

        }
        
        [Button]
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
        #endregion

        #region Level Loading
        [Button]
        public void LoadLevel( /*string levelName*/ )
        {
            if(!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError( "PhotonNetwork : Trying to Load a level but we are not the master Client" );
            }

            Debug.LogFormat( "PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount );
            PhotonNetwork.LoadLevel( "RoomFor" + PhotonNetwork.CurrentRoom.PlayerCount );
        }
        #endregion

        #region User settings
        public void SetUsername(string username = "Unknown")
        {
            PhotonNetwork.NickName = username;
        }
        #endregion

        #region Photon Callbacks
        public override void OnConnectedToMaster()
        {
            if(isConnecting)
            {
                Debug.Log( "Network Manager:  OnConnectedToMaster() was called by PUN" );
                connectedToMasterEvent?.Invoke();
                isConnectedToMaster = true;
            }
        }

        public override void OnDisconnected( DisconnectCause cause )
        {
            Debug.LogFormat( "Network Manager:  OnDisconnected() was called by PUN with reason {0}", cause );
            disconnectedFromMasterEvent?.Invoke();
        }

        public override void OnJoinRandomFailed( short returnCode, string message )
        {
            Debug.Log( "Network Manager: OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom" );
            PhotonNetwork.CreateRoom( null, new RoomOptions() );
        }

        public override void OnJoinedRoom()
        {
            Debug.Log( "Network Manager:  OnJoinedRoom() called by PUN. Now this client is in a room." );
            roomJoinedEvent?.Invoke();
        }

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene( launcherSceneName ); // TODO: handle this more gracefully!!!
            leftRoomEvent?.Invoke();
        }

        public override void OnJoinedLobby()
        {
            Debug.Log( "Network Manager: Joined lobby" );
            lobbyJoinedEvent?.Invoke();
        }

        public override void OnLeftLobby()
        {
            SceneManager.LoadScene( launcherSceneName ); // TODO: handle this more gracefully!!!
            leftLobbyEvent?.Invoke();
        }

        public override void OnPlayerEnteredRoom( Player other )
        {
            Debug.LogFormat( "OnPlayerEnteredRoom() {0}", other.NickName ); // not seen if you're the player connecting

            if ( PhotonNetwork.IsMasterClient )
            {
                Debug.LogFormat( "OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient ); // called before OnPlayerLeftRoom
            }

            playerEnteredRoomEvent?.Invoke();
        }

        public override void OnPlayerLeftRoom( Player other )
        {
            Debug.LogFormat( "OnPlayerLeftRoom() {0}", other.NickName ); // seen when other disconnects

            if ( PhotonNetwork.IsMasterClient )
            {
                Debug.LogFormat( "OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient ); // called before OnPlayerLeftRoom
            }

            playerExitedRoomEvent?.Invoke();
        }
        #endregion
    }
}

