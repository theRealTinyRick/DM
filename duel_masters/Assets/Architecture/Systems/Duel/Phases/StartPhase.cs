/*
 Author: Aaron Hines
 Description: defines the start phase of a players turn
*/
using UnityEngine;
using GameFramework.Phases;

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
            DuelManager.instance.startPhaseEnteredEvent.Invoke( DuelManager.instance.currentTurnPlayer );
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
            DuelManager.instance.startPhaseExitedEvent.Invoke( DuelManager.instance.currentTurnPlayer );
        }
    }
}
