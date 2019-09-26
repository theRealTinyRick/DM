/*
 Author: Aaron Hines
 Edits By: 
 Description: defines a room button in the lobby menu
 */
using UnityEngine;
using Photon.Pun;
using TMPro;

namespace DM.Systems.Networking.UI
{
    public class RoomButton : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI nameText;

        [SerializeField]
        private TextMeshProUGUI descriptionText;

        [SerializeField]
        private TextMeshProUGUI hostNameText;

        private string roomName;

        public void SetRoom(string roomName, string description, string hostName)
        {
            this.roomName = roomName;

            nameText.text = roomName;
            descriptionText.text = description;
            hostNameText.text = hostName;
        }

        public void JoinRoomOnClick()
        {
            PhotonNetwork.JoinRoom( roomName );
        }
    }
}
