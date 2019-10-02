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

        public PlayerComponent player
        {
            get
            {
                return DuelManager.instance.localPlayer;
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
                return player.manaZone.cards.FindAll( _card => !_card.tapped );
            }
        }

        private List<Card> unavailableMana
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

        private List<ICastCondition> castFilters = new List<ICastCondition>();

        public List<Card> castableCards
        {
            get;
            private set;
        } = new List<Card>();

        private Card currentlyCastingCard;
        private PhotonView photonView;

        protected override void Enable()
        {
            if ( photonView == null )
            {
                photonView = GetComponent<PhotonView>();
            }
        }

        private void Update()
        {
            Run(); // TODO: remove this from update and only call whent he hand state has changed
        }

        public void Cast(Card card)
        {

        }

        private void Run()
        {
            castableCards.Clear();

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
                        castableCards.Add( _card );
                    }
                }
            }
        }

        private bool CanPayFor(Card card)
        {
            return card.manaCost <= availableMana.Count && CanPayCivCost( card );
        }

        private bool CanPayCivCost(Card card)
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

        private bool Filter(Card card)
        {
            foreach(ICastCondition _filter in castFilters)
            {
                if(!_filter.CanPlayCard(card))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
