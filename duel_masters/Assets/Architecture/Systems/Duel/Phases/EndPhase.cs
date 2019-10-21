/*
 Author: Aaron Hines
 Description: defines the end phase of a players turn
*/
using UnityEngine;
using GameFramework.Phases;
using DuelMasters.Systems.Turns;

namespace DuelMasters.Systems.Duel.Phases
{
    public class EndPhase : IPhase
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

        public void EnterPhase()
        {
            DuelManager.instance.endPhaseEnteredEvent.Invoke( TurnManager.instance.currentTurnPlayer );
        }

        public void RunPhase( float deltaTime )
        {
            currentTime += deltaTime;
            if ( currentTime >= delayTime )
            {
                phaseManager.MoveToNextPhase();
            }
        }

        public void ExitPhase()
        {
            DuelManager.instance.endPhaseExitedEvent.Invoke( TurnManager.instance.currentTurnPlayer );
            TurnManager.instance.PassTurn();
            Debug.Log("End Phase ended, pass turn");
        }
    }
}
