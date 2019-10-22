/*
 Author: Aaron Hines
 Edits By: 
 Description: Defines general actions that can be done in a game. This is more related to moveing cards around
 */
using System;
using System.Collections;

using UnityEngine;
using Photon.Pun;
using Sirenix.OdinInspector;

using DuelMasters.Systems.Cards;
using DuelMasters.Systems.Players;
using DuelMasters.Systems.Gameplay.Locations;

namespace DuelMasters.Systems.Actions
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
        public void TriggerAddShieldsFromDeck( PlayerComponent targetPlayer, int amount = 1, bool waitForResponse = true )
        {
            photonView.RPC( "AddShieldsFromDeckRPC", RpcTarget.All, targetPlayer.playerNumber, amount, waitForResponse );
        }

        [PunRPC]
        public void AddShieldsFromDeckRPC( int targetPlayer, int amount, bool waitForResponse )
        {
            PlayerComponent _player = DuelManager.instance.GetPlayer( targetPlayer );
            StartCoroutine( AddShieldsRoutine( _player, amount, waitForResponse ) );
        }

        private IEnumerator AddShieldsRoutine( PlayerComponent targetPlayer, int amount, bool waitForResponse )
        {
            for ( int i = 0; i < amount; i++ )
            {
                yield return new WaitForSeconds( repeatedActionDelay );
                Action.AddToShieldsFromTopOfDeck( targetPlayer );
            }
        }
        #endregion

        #region DRAW
        public void Draw( PlayerComponent targetPlayer, int amount = 1, bool waitForResponse = true )
        {
            photonView.RPC( "DrawRPC", RpcTarget.All, targetPlayer.playerNumber, amount, waitForResponse );
        }

        [PunRPC]
        public void DrawRPC( int targetPlayer, int amount = 1, bool waitForResponse = true )
        {
            PlayerComponent _player = DuelManager.instance.GetPlayer( targetPlayer );
            StartCoroutine( DrawRoutine( _player, amount, waitForResponse ) );
        }

        private IEnumerator DrawRoutine( PlayerComponent targetPlayer, int amount = 1, bool waitForResponse = true )
        {
            for( int i = 0; i < amount; i++ )
            {
                yield return new WaitForSeconds(repeatedActionDelay);
                Action.Draw( targetPlayer );
            }
        }
        #endregion

        #region MANA
        public void TriggerAddManaFromHand(PlayerComponent player, Card card)
        {
            photonView.RPC( "AddManaFromHandRPC", RpcTarget.All, player.playerNumber, card.instanceId.ToString() );
        }

        [PunRPC]
        public void AddManaFromHandRPC(int player, string cardId)
        {
            PlayerComponent _player = DuelManager.instance.GetPlayer( player );
            Action.AddToManaFromHand( _player, _player.hand.Get( Guid.Parse( cardId ) ) );
        }

        public void TapMana(Card card)
        {
            photonView.RPC( "TapManaRPC", RpcTarget.All, card.owner.playerNumber, card.instanceId.ToString() );
        }

        [PunRPC]
        public void TapManaRPC( int player, string cardId )
        {
            PlayerComponent _player = DuelManager.instance.GetPlayer( player );
            Action.TapCard( _player.manaZone.Get( Guid.Parse( cardId ) ) );
        }
        #endregion

        #region SUMMON
        public void Summon(Card card)
        {
            photonView.RPC( "SummonRPC", RpcTarget.All, card.owner.playerNumber, card.instanceId.ToString() );
        }

        [PunRPC]
        public void SummonRPC(int targetPlayer, string instanceId)
        {
            PlayerComponent _player = DuelManager.instance.GetPlayer( targetPlayer );
            Card _card = _player.hand.Get( Guid.Parse( instanceId ) );
            if(_card != null)
            {
                Action.Summon( _player.hand, _card );
            }
        }
        #endregion

        #region Untap
        public void UntapAllCards(PlayerComponent player)
        {
            photonView.RPC("UntapAllCardsRPC", RpcTarget.All, player.playerNumber);
        }

        [PunRPC]
        public void UntapAllCardsRPC(int playerNumber)
        {
            PlayerComponent _player = DuelManager.instance.GetPlayer(playerNumber);
            Action.UntapAll(_player.battleZone);
            Action.UntapAll(_player.manaZone);
        }
        #endregion
    }
}
