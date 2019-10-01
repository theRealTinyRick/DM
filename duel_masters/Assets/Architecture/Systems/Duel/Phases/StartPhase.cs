/*
 Author: Aaron Hines
 Description: defines the start phase of a players turn
*/
using UnityEngine;
using GameFramework.Phases;
using DM.Systems.Turns;

namespace DM.Systems.Duel.Phases
{
    public class StartPhase : IPhase
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
            Debug.Log( "Start Phase entered" );
            DuelManager.instance.startPhaseEnteredEvent.Invoke( TurnManager.instance.currentTurnPlayer );
        }

        public void RunPhase( float deltaTime )
        {
            currentTime += deltaTime;
            if(currentTime >= delayTime)
            {
                phaseManager.MoveToNextPhase();
            }
        }

        public void ExitPhase()
        {
            currentTime = 0;
            DuelManager.instance.startPhaseExitedEvent.Invoke( TurnManager.instance.currentTurnPlayer );
        }
    }
}
