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

        private Dictionary<Guid, ICastFilter> castFilters = new Dictionary<Guid, ICastFilter>();
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
                if ( phaseManager.currentPhase != null && castablePhases.Contains( phaseManager.currentPhase.identifier) )
                {
                    // loop through the filters
                    return;
                }
            }

            cardsThatYouCanCast.Clear();
        }

        public void AddFilter( Card owner, ICastFilter filter )
        {
        }
    }
}
