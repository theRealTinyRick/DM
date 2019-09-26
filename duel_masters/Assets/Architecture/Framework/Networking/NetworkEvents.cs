/*
 Author: Aaron Hines
 Edits By: 
 Description: holder for all network events
 */
using System.Collections.Generic;
using UnityEngine.Events;
using Photon.Realtime;

namespace GameFramework.Networking
{
    [System.Serializable]
    public class ConnectedToMasterEvent : UnityEvent
    {
    }

    [System.Serializable]
    public class DisconnectedFromMasterEvent : UnityEvent
    {
    }

    [System.Serializable]
    public class RoomJoinedEvent : UnityEvent
    {
    }

    [System.Serializable]
    public class LeftRoomEvent : UnityEvent
    {
    }

    [System.Serializable]
    public class LobbyJoinedEvent : UnityEvent
    {
    }

    [System.Serializable]
    public class LeftLobbyEvent : UnityEvent
    {
    }

    [System.Serializable]
    public class PlayerEnteredRoomEvent : UnityEvent<Player>
    {
    }

    [System.Serializable]
    public class PlayerExitedRoomEvent : UnityEvent<Player>
    {
    }

    [System.Serializable]
    public class RoomListUpdateEvent : UnityEvent<List<RoomInfo>>
    {
    }

    [System.Serializable]
    public class AllPlayerLevelsLoadedEvent : UnityEvent
    {
    }
}
