/*
 Author: Aaron Hines
 Description: defines the main phase of a players turn
*/
using UnityEngine;
using DuelMasters.Systems.Cards;
using DuelMasters.Systems.Players;
using GameFramework.Phases;

namespace DuelMasters.Systems.Duel.Phases
{
    public class MainPhase : IPhase
    {
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
            Debug.Log("Main Phase Started");
        }

        public void RunPhase( float deltaTime )
        {
        }

        public void ExitPhase()
        {
            Debug.Log("Main Phase Ended, move to end");
        }
    }
}
