/*
 Author: Aaron Hines
 Description: defines the draw phase of a players turn
*/
using UnityEngine;
using GameFramework.Phases;
using DuelMasters.Systems.Turns;
using DuelMasters.Systems.Actions;

namespace DuelMasters.Systems.Duel.Phases
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
            Debug.Log( "draw phase entered" );
            DuelManager.instance.drawPhaseEnteredEvent.Invoke( TurnManager.instance.currentTurnPlayer );

            if( phaseManager.GetComponent<Photon.Pun.PhotonView>().IsMine )
            {
                ActionManager.instance.Draw( TurnManager.instance.currentTurnPlayer, 1, false );
            }
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
            DuelManager.instance.drawPhaseExitedEvent.Invoke( TurnManager.instance.currentTurnPlayer );
        }
    }
}
