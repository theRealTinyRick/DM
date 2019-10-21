
using System;
using System.Collections;
using UnityEngine;
using Photon.Pun;
using Sirenix.OdinInspector;

using GameFramework.Phases;

using DuelMasters.Systems.Cards;
using DuelMasters.Systems.Players;
using DuelMasters.Systems.Gameplay.Locations;

namespace DuelMasters.Systems.Turns
{
    public class TurnManager : Singleton_SerializedMonobehaviour<TurnManager>
    {
        public PlayerComponent currentTurnPlayer;
        public int currentTurnPlayerIndex = 0;

        public PlayerComponent currentPriorityPlayer;
        public int currentPriorityPlayerIndex;

        private PlayerComponent[] players;
        private PhotonView photonView;

        private void Start()
        {
            if(photonView == null)
            {
                photonView = GetComponent<PhotonView>();
            }
        }

        private void Update()
        {
            if(PhotonNetwork.IsMasterClient)
            {
                // run turn timer
            }
        }

        public void Init()
        {
            if( PhotonNetwork.IsMasterClient )
            {
                photonView.RPC( "InitRPC", RpcTarget.All );
            }
        }

        [PunRPC]
        public void InitRPC()
        {
            players = DuelManager.instance.players.ToArray();
            currentTurnPlayer = DuelManager.instance.GetPlayer( 1 );
            currentTurnPlayerIndex = Array.IndexOf( players, currentTurnPlayer );
        }

        [Button]
        public void PassTurn()
        {
            if ( PhotonNetwork.IsMasterClient )
            {
                photonView.RPC( "PassTurnRPC", RpcTarget.All );
            }
        }

        [PunRPC]
        public void PassTurnRPC()
        {
            int newTurnIndex = currentTurnPlayerIndex + 1;
            if( newTurnIndex >= players.Length )
            {
                newTurnIndex = 0;
            }

            currentTurnPlayerIndex = newTurnIndex;
            currentTurnPlayer = players[currentTurnPlayerIndex];
        }

        public void SetPriority(int player)
        {

        }

        public void SetPriority(PlayerComponent player)
        {

        }
    }
}
