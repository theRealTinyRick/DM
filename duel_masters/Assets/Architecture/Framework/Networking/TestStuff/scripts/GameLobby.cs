using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.SceneManagement;

public class GameLobby : Singleton_MonobehaviourPunCallbacks<GameLobby>, ILobbyCallbacks
{
    public string roomName;   
    public int roomSize;
    public GameObject roomListingsprefab;
    public Transform roomsPanel;

    public void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log( "Network Manager:  OnConnectedToMaster() was called by PUN" );
    }


    public override void OnRoomListUpdate( List<RoomInfo> roomList )
    {
        base.OnRoomListUpdate( roomList );

        RemoveRoomListings();
        foreach(RoomInfo _room in roomList)
        {
            ListRoom( _room );
        }
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

    public override void OnPlayerEnteredRoom( Player other )
    {
        Debug.LogFormat( "OnPlayerEnteredRoom() {0}", other.NickName ); // not seen if you're the player connecting

        if ( PhotonNetwork.IsMasterClient )
        {
            Debug.LogFormat( "OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient ); // called before OnPlayerLeftRoom
        }
    }

    public override void OnPlayerLeftRoom( Player other )
    {
        Debug.LogFormat( "OnPlayerLeftRoom() {0}", other.NickName ); // seen when other disconnects

        if ( PhotonNetwork.IsMasterClient )
        {
            Debug.LogFormat( "OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient ); // called before OnPlayerLeftRoom
        }
    }

    public void LoadLevel()
    {
        PhotonNetwork.LoadLevel( "TEST" );
    }

    public void CreateRoom()
    {
        RoomOptions _roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom( roomName, _roomOps );
    }

    public override void OnCreateRoomFailed( short returnCode, string message )
    {
        Debug.Log( "room could not be created: " + message );
    }

    public void OnRoomNameChanged(string name)
    {
        roomName = name;
    }

    public void OnRoomSizeChanged(string size)
    {
        roomSize = int.Parse( size );
    }

    public void JoinLobbyOnClick()
    {
        if(!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log( "Network Manager:  OnJoinedRoom() called by PUN. Now this client is in a room." );
    }
}
