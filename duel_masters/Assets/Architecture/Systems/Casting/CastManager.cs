using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Photon.Pun;
using Sirenix.OdinInspector;

using GameFramework.Phases;

using DM.Systems.Turns;
using DM.Systems.Players;
using DM.Systems.Cards;
using DM.Systems.Actions;

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

        private Card currentlyCastingCard;
        private PhotonView photonView;

        protected override void Enable()
        {
            if ( photonView == null )
            {
                photonView = GetComponent<PhotonView>();
            }
        }

        public void Cast( Card card, bool shieldtrigger = false )
        {
            if ( CanCast( card ) )
            {
                StartCoroutine( CastRoutine( card ) );
            }
        }

        private IEnumerator CastRoutine( Card card )
        {
            currentlyCastingCard = card;

            // add a wait here for tapping mana

            if ( card.castRequirements.Count <= 0 )
            {
                yield return new WaitForSeconds( 1f );
            }
            else
            {
                Debug.Log( "check reqs" );
                foreach ( ICastRequirements _req in card.castRequirements )
                {
                    _req.Start();

                    while ( _req.running )
                    {
                        yield return new WaitForEndOfFrame();
                        if ( _req.failed )
                        {
                            // TODO: cancel summon
                        }
                    }

                    _req.Stop();
                }
            }

            switch ( card.cardType )
            {
                case CardType.Creature:
                    Debug.Log( "summon" );
                    ActionManager.instance.Summon( card );
                    break;
                case CardType.Spell:
                    break;
                case CardType.EvolutionCreature:
                    break;
            }

            currentlyCastingCard = null;
        }

        private bool CanCast( Card card )
        {
            if ( turnManager.currentTurnPlayer != player )
            {
                return false;
            }

            if ( phaseManager.currentPhase == null )
            {
                return false;
            }

            if ( mainPhase != phaseManager.currentPhase.identifier )
            {
                return false;
            }

            if ( CanPayFor( card ) && Filter(card) )
            {
                return true;
            }

            return false;
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
