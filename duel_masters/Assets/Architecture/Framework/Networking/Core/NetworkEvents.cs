/*
 Author: Aaron Hines
 Edits By: 
 Description: holder for all network events
 */
using UnityEngine.Events;

namespace GameFramework.Netorking
{
    public class ConnectedToMasterEvent : UnityEvent
    {
    }

    public class DisconnectedFromMasterEvent : UnityEvent
    {
    }

    public class RoomJoinedEvent : UnityEvent
    {
    }

    public class LeftRoomEvent : UnityEvent
    {
    }

    public class LobbyJoinedEvent : UnityEvent
    {
    }

    public class LeftLobbyEvent : UnityEvent
    {
    }

    public class PlayerEnteredRoomEvent : UnityEvent
    {
    }

    public class PlayerExitedRoomEvent : UnityEvent
    {
    }
}
