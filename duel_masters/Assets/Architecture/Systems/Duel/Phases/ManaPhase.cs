/*
 Author: Aaron Hines
 Description: defines the start phase of a players turn
*/
using UnityEngine;
using DM.Systems.Cards;
using DM.Systems.Players;
using GameFramework.Phases;

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
            DuelManager.instance.manaAddedEvent.AddListener( OnManaAdded );
        }

        private void OnManaAdded(DuelistComponent player, Card card)
        {
            manaAdded = true;
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
            manaAdded = false;
        }
    }
}
