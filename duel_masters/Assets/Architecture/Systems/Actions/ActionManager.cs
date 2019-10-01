/*
 Author: Aaron Hines
 Edits By: 
 Description: Defines general actions that can be done in a game. This is more related to moveing cards around
 */
using System.Collections;

using UnityEngine;
using Photon.Pun;
using Sirenix.OdinInspector;

using DM.Systems.Cards;
using DM.Systems.Players;
using DM.Systems.Gameplay.Locations;
using System;

namespace DM.Systems.Actions
{
    public class ActionManager : Singleton_SerializedMonobehaviour<ActionManager>
    {
        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private float repeatedActionDelay;

        private PhotonView photonView;

        private void OnEnable()
        {
            if(photonView == null)
            {
                photonView = GetComponentInChildren<PhotonView>();
            }
        }

        #region SHIELDS
        public void TriggerAddShieldsFromDeck( DuelistComponent targetPlayer, int amount = 1, bool waitForResponse = true )
        {
            photonView.RPC( "AddShieldsFromDeckRPC", RpcTarget.All, targetPlayer.playerNumber, amount, waitForResponse );
        }

        [PunRPC]
        public void AddShieldsFromDeckRPC( int targetPlayer, int amount, bool waitForResponse )
        {
            DuelistComponent _player = DuelManager.instance.GetPlayer( targetPlayer );
            StartCoroutine( AddShieldsRoutine( _player, amount, waitForResponse ) );
        }

        IEnumerator AddShieldsRoutine( DuelistComponent targetPlayer, int amount, bool waitForResponse )
        {
            for ( int i = 0; i < amount; i++ )
            {
                yield return new WaitForSeconds( repeatedActionDelay );
                Action.AddToShieldsFromTopOfDeck( targetPlayer );
            }
        }
        
        #endregion

        #region DRAW
        public void TriggerDraw( DuelistComponent targetPlayer, int amount = 1, bool waitForResponse = true )
        {
            photonView.RPC( "DrawRPC", RpcTarget.All, targetPlayer.playerNumber, amount, waitForResponse );
        }

        [PunRPC]
        public void DrawRPC( int targetPlayer, int amount = 1, bool waitForResponse = true )
        {
            DuelistComponent _player = DuelManager.instance.GetPlayer( targetPlayer );
            StartCoroutine( DrawRoutine( _player, amount, waitForResponse ) );
        }

        IEnumerator DrawRoutine( DuelistComponent targetPlayer, int amount = 1, bool waitForResponse = true )
        {
            for( int i = 0; i < amount; i++ )
            {
                yield return new WaitForSeconds(repeatedActionDelay);
                Action.Draw( targetPlayer );
            }
        }
        #endregion

        #region MANA
        public void TriggerAddManaFromHand(DuelistComponent player, Card card)
        {
            photonView.RPC( "AddManaFromHandRPC", RpcTarget.All, player.playerNumber, card.instanceId.ToString() );
        }

        [PunRPC]
        public void AddManaFromHandRPC(int player, string cardId)
        {
            DuelistComponent _player = DuelManager.instance.GetPlayer( player );
            Action.AddToManaFromHand( _player, _player.hand.Get( Guid.Parse( cardId ) ) );
        }
        #endregion
    }
}
