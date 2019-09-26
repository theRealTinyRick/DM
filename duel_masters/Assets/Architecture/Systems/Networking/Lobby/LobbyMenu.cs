/*
 Author: Aaron Hines
 Edits By: 
 Description: defines a room button in the lobby menu
 */
using System.Collections.Generic;

using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Sirenix.OdinInspector;

using GameFramework.Networking;

namespace DM.Systems.Networking.UI
{

    public class LobbyMenu : MonoBehaviour
    {
        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private GameObject roomListingsprefab;

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private Transform roomsPanel;

        private string roomName;
        private string description;

        public void OnEnable()
        {
            NetworkManager.instance.roomListUpdateEvent.AddListener( ListRoom );
        }

        public void OnDisable()
        {
            NetworkManager.instance.roomListUpdateEvent.AddListener( ListRoom );
        }

        /// <summary>
        ///     Use this from the inspector to response to start button clicks - not needed if you are doing auto start
        /// </summary>
        public void OnStartClick()
        {
            NetworkManager.instance.StartGame();
        }

        public void OnCreateRoomClick()
        {
            if(!string.IsNullOrEmpty(roomName))
            {
                NetworkManager.instance.CreateRoom( roomName );
            }
            else
            {
                Debug.Log( "Cannot create a room with an empty name" );
            }
        }

        public void OnRoomNameChanged( string name ) 
        {
            roomName = name;
        }

        public void OnDescriptionChanged(string desc)
        {
            description = desc;
        }

        private void ListRoom( List<RoomInfo> roomList )
        {
            RemoveRoomListings(); // kill all of the current ui

            foreach ( RoomInfo _room in roomList )
            {
                if ( _room.IsOpen && _room.IsVisible )
                {
                    GameObject _listing = Instantiate( roomListingsprefab, roomsPanel );
                    RoomButton _button = _listing.GetComponent<RoomButton>();

                    _button.SetRoom( _room.Name, description, PhotonNetwork.NickName );
                }
            }
        }

        private void RemoveRoomListings()
        {
            while ( roomsPanel.childCount != 0 )
            {
                Destroy( roomsPanel.GetChild( 0 ).gameObject );
            }
        }
    }
}
