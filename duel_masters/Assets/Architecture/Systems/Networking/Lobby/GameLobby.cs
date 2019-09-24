using System.Collections.Generic;

using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace DM.Systems.Networking.Lobby
{
    public class GameLobby : Singleton_MonobehaviourPunCallbacks<GameLobby>, ILobbyCallbacks
    {
        public string roomName;   
        public GameObject roomListingsprefab;
        public Transform roomsPanel;

        public void Start()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }

        public void OnRoomNameChanged( string name )
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

        public void OnFindGamesClick()
        {
            if(!PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinLobby();
            }
        }

        public void OnStartGameClick()
        {
            PhotonNetwork.LoadLevel( "Duel_Standard" ); // TODO: change to use the manifest system
        }

        public void OnCreateRoomClick()
        {
            RoomOptions _roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)5 };
            PhotonNetwork.CreateRoom( roomName, _roomOps );
        }

        public void ClearName()
        {
            roomName = "";
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            Debug.Log( "Network Manager:  OnConnectedToMaster() was called by PUN" );
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
            Debug.LogFormat( "OnPlayerEnteredRoom() {0}", other.NickName ); // not seen if you're the player connecting

            if ( PhotonNetwork.IsMasterClient )
            {
                Debug.LogFormat( "OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient ); // called before OnPlayerLeftRoom
            }

            OnStartGameClick();
        }

        public override void OnPlayerLeftRoom( Player other )
        {
            Debug.LogFormat( "OnPlayerLeftRoom() {0}", other.NickName ); // seen when other disconnects

            if ( PhotonNetwork.IsMasterClient )
            {
                Debug.LogFormat( "OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient ); // called before OnPlayerLeftRoom
            }
        }

        public override void OnCreateRoomFailed( short returnCode, string message )
        {
            Debug.Log( "room could not be created: " + message );
        }


        public override void OnJoinedRoom()
        {
            Debug.Log( "Network Manager:  OnJoinedRoom() called by PUN. Now this client is in a room." );
        }

        public override void OnJoinedLobby()
        {
            Debug.Log( "Network Manager:  OnJoinedLobby() called by PUN. Now this client is in a lobby." );
        }
    }
}

