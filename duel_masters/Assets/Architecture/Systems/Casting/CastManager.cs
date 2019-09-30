using System;
using System.Collections.Generic;

using UnityEngine;
using Photon.Pun;
using Sirenix.OdinInspector;

using DM.Systems.Turns;
using DM.Systems.Players;
using DM.Systems.Cards;
using GameFramework.Phases;

namespace DM.Systems.Casting
{
    public class CastManager : SerializedMonoBehaviour
    {
        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        private List<PhaseIdentifier> castablePhases;

        private PhaseManager phaseManager
        {
            get
            {
                return DuelManager.instance.phaseManager;
            }
        }

        private TurnManager turnManager
        {
            get
            {
                return DuelManager.instance.turnManager;
            }
        }

        private DuelistComponent _player;
        public DuelistComponent player
        {
            get
            {
                if(_player == null)
                {
                    _player = DuelManager.instance.localPlayer;
                }
                return _player;
            }
        }

        private List<ICastFilter> castFilters = new List<ICastFilter>();
        private List<Card> cardsThatYouCanCast = new List<Card>();

        private PhotonView photonView;

        private void OnEnable()
        {
            if(photonView == null)
            {
                photonView = GetComponent<PhotonView>();
            }
        }

        private void Update()
        {
            RunFilters();
        }

        private void RunFilters()
        {
            if ( turnManager.currentTurnPlayer == player )
            {
                if ( phaseManager.currentPhase == null )
                {
                    return;
                }

                if ( castablePhases.Contains( phaseManager.currentPhase.identifier) )
                {
                    // check available mana

                    // loop through the filters and remove any card that does not meet the conditions
                    return;
                }

                // if it
            }

            cardsThatYouCanCast.Clear();
        }

        public void AddFilter( Card owner, ICastFilter filter )
        {

        }

        public void RemoveFilter( Card owner, ICastFilter filter)
        {

        }
    }
}
