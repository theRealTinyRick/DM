using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using Photon.Pun;
using Sirenix.OdinInspector;

using DM.Systems.Turns;
using DM.Systems.Players;
using DM.Systems.Cards;
using GameFramework.Phases;

namespace DM.Systems.Casting
{
    public class CastManager : Singleton_SerializedMonobehaviour<CastManager>
    {
        [SerializeField]
        [TabGroup( Tabs.PROPERTIES )]
        private PhaseIdentifier mainPhase;

        [SerializeField]
        [TabGroup( Tabs.PROPERTIES )]
        private PhaseIdentifier battlePhase;

        private PhaseManager phaseManager
        {
            get
            {
                return PhaseManager.instance;
            }
        }

        private TurnManager turnManager
        {
            get
            {
                return TurnManager.instance;
            }
        }

        private PlayerComponent _player;
        public PlayerComponent player
        {
            get
            {
                if ( _player == null )
                {
                    _player = DuelManager.instance.localPlayer;
                }
                return _player;
            }
        }

        private List<Card> allMana
        {
            get
            {
                return player.manaZone.cards;
            }
        }

        private List<Card> availableMana
        {
            get
            {
                return player.manaZone.cards.FindAll( _card => _card.tapped );
            }
        }

        private List<Civ> availableCivilizations
        {
            get
            {
                List<Civ> _civs = new List<Civ>();
                foreach ( Card _card in availableMana )
                {
                    foreach ( Civ _civ in _card.civilization.civs )
                    {
                        if ( !_civs.Contains( _civ ) )
                        {
                            _civs.Add( _civ );
                        }
                    }
                }

                return _civs;
            }
        }

        private List<Card> unavailableMana
        {
            get
            {
                return player.manaZone.cards.FindAll( _card => !_card.tapped );
            }
        }

        private List<ICastFilter> castFilters = new List<ICastFilter>();

        [SerializeField]
        private List<Card> cardsThatYouCanCast = new List<Card>();

        private PhotonView photonView;

        protected override void Enable()
        {
            if ( photonView == null )
            {
                photonView = GetComponent<PhotonView>();
            }
        }

        public void AddFilter( Card owner, ICastFilter filter )
        {

        }

        public void RemoveFilter( Card owner, ICastFilter filter )
        {

        }

        private void Update()
        {
            Run();
        }

        private void Run()
        {
            cardsThatYouCanCast.Clear();

            if ( turnManager.currentTurnPlayer != player )
            {
                return;
            }

            if ( phaseManager.currentPhase == null )
            {
                return;
            }

            if ( mainPhase != phaseManager.currentPhase.identifier )
            {
                return;
            }

            foreach ( Card _card in player.hand.cards ) // TODO: add away to add spaces to this - phycics and playing cards in the grave
            {
                if ( CanPayFor(_card ) )
                {
                    if(Filter(_card))
                    {
                        cardsThatYouCanCast.Add( _card );
                    }
                }
            }
        }

        private bool CanPayFor(Card card)
        {
            return card.manaCost <= availableMana.Count && AllCivsAreAvailable( card );
        }

        private bool Filter(Card card)
        {
            foreach(ICastFilter _filter in castFilters)
            {
                if(!_filter.CanPlayCard(card))
                {
                    return false;
                }
            }

            return true;
        }

        #region Private Methods

        private bool AllCivsAreAvailable(Card card)
        {
            foreach(Civ _civ in card.civilization.civs)
            {
                if(!availableCivilizations.Contains(_civ))
                {
                    return false;
                }
            }

            return true; 
        }

        #endregion
    }
}
