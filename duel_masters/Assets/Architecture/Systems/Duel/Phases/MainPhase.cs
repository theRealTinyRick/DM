/*
 Author: Aaron Hines
 Description: defines the main phase of a players turn
*/
using UnityEngine;
using DM.Systems.Cards;
using DM.Systems.Players;
using GameFramework.Phases;

namespace DM.Systems.Duel.Phases
{
    public class MainPhase : IPhase
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
        }
    }
}
