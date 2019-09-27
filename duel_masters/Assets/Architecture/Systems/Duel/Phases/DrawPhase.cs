/*
 Author: Aaron Hines
 Description: defines the draw phase of a players turn
*/
using UnityEngine;
using GameFramework.Phases;

namespace DM.Systems.Duel.Phases
{
    public class DrawPhase : IPhase
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
            //DuelManager.instance.drawPhaseEnteredEvent.Invoke( DuelManager.instance.currentTurnPlayer );
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
            currentTime = 0;
           // DuelManager.instance.drawPhaseExitedEvent.Invoke( DuelManager.instance.currentTurnPlayer );
        }
    }
}
