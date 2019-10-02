/*
 Author: Aaron Hines
 Description: defines the start phase of a players turn
*/
using UnityEngine;

using GameFramework.Phases;

using DM.Systems.Cards;
using DM.Systems.Players;
using DM.Systems.Turns;

namespace DM.Systems.Duel.Phases
{
    public class ManaPhase : IPhase
    {
        [SerializeField]
        private float delayTime = 0;
        private float currentTime = 0;

        public PhaseManager phaseManager
        {
            get;
            set;
        }

        [SerializeField]
        private PhaseIdentifier _phaseIdentifier;
        public PhaseIdentifier identifier
        {
            get => _phaseIdentifier;
            set => _phaseIdentifier = value;
        }

        public bool manaAdded
        {
            get;
            private set;
        }

        public void EnterPhase()
        {
            Debug.Log( "Mana Phase entered" );
            DuelManager.instance.manaAddedEvent.AddListener( OnManaAdded );
            DuelManager.instance.manaPhaseEnteredEvent.Invoke( TurnManager.instance.currentTurnPlayer );
        }

        private void OnManaAdded(PlayerComponent player, Card card)
        {
            if( player == TurnManager.instance.currentTurnPlayer )
            {
                manaAdded = true;
            }
        }

        public void RunPhase( float deltaTime )
        {
            if(manaAdded)
            {
                currentTime += deltaTime;
                if ( currentTime >= delayTime )
                {
                    phaseManager.MoveToNextPhase();
                }
            }
        }

        public void ExitPhase()
        {
            DuelManager.instance.manaAddedEvent.RemoveListener( OnManaAdded );
            DuelManager.instance.manaPhaseExitedEvent.Invoke( TurnManager.instance.currentTurnPlayer );
            manaAdded = false;
        }
    }
}
