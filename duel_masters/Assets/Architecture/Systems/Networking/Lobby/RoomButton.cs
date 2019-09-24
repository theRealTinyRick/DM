using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomButton : MonoBehaviour
{
    public TextMeshProUGUI nameText;

    public string roomName;
    public int roomSize;

    public void SetRoom()
    {
        nameText.text = roomName;
    }

    public void JoinRoomOnClick()
    {
        PhotonNetwork.JoinRoom( roomName );
    }
}
