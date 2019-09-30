/*
 Author: Aaron Hines
 Description: controls phases
*/
using System.Linq;
using System.Collections.Generic;
using Photon.Pun;

using UnityEngine;
using Sirenix.OdinInspector;

using UnityEngine.Events;

namespace GameFramework.Phases
{
    [System.Serializable]
    public class PhaseEnteredEvent : UnityEvent<IPhase>
    {
    }

    [System.Serializable]
    public class PhaseRunningEvent : UnityEvent<IPhase>
    {
    }

    [System.Serializable]
    public class PhaseEndedEvent : UnityEvent<IPhase>
    {
    }

    public class PhaseManager : SerializedMonoBehaviour
    {
        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        [Tooltip( "Checking this box will cause the phases to loop back to the beginning after reaching the end" )]
        private bool loop = false;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        private List<IPhase> _phases = new List<IPhase>();
        public List<IPhase> phases
        {
            get => _phases;
            private set => phases = value;
        }

        [TabGroup(Tabs.EVENTS)]
        [SerializeField]
        public PhaseEnteredEvent phaseEnteredEvent { get; set; }

        [TabGroup(Tabs.EVENTS)]
        [SerializeField]
        public PhaseRunningEvent phaseRunningEvent { get; set; }

        [TabGroup(Tabs.EVENTS)]
        [SerializeField]
        public PhaseEndedEvent phaseExitedEvent { get; set; }

        public IPhase currentPhase
        {
            get;
            private set;
        } = null;

        public IPhase previousPhase
        {
            get;
            private set;
        } = null;

        private int currentIndex = 0;
        private bool running = false;

        public PhotonView photonView;

        private void OnEnable()
        {
            photonView = GetComponent<PhotonView>();

            OnReset();
        }

        private void Update()
        {
            if(running)
            {
                Run();
            }
        }

        public void StartPhases()
        {
            photonView.RPC( "StartPhasesRPC", RpcTarget.All );
        }

        [PunRPC]
        public void StartPhasesRPC()
        {
            foreach ( var _phase in phases )
            {
                _phase.phaseManager = this;
            }

            if (phases.Count > 0)
            {
                OnReset();

                currentPhase = phases[currentIndex];
                EnterPhase( currentPhase );

                running = true;
            }
        }

        public void OnReset()
        {
            if(previousPhase != null)
            {
                ExitPhase( previousPhase );   
            }
            previousPhase = null;

            currentIndex = 0;
            currentPhase = null;
            running = false;
        }

        [Button]
        public void MoveToNextPhase()
        {
            if(PhotonNetwork.IsMasterClient)
            {
                photonView.RPC( "MoveToNextPhaseRPC", RpcTarget.All );
            }
        }

        [PunRPC]
        public void MoveToNextPhaseRPC()
        {
            int _nextIndex = currentIndex + 1;

            if ( _nextIndex >= phases.Count )
            {
                if ( loop )
                {
                    _nextIndex = 0;
                }
                else
                {
                    ExitPhase( currentPhase );
                    return;
                }
            }

            if ( currentPhase != null )
            {
                previousPhase = currentPhase;
            }

            if ( previousPhase != null )
            {
                ExitPhase( previousPhase );
            }

            currentIndex = _nextIndex;
            currentPhase = phases[currentIndex];

            EnterPhase( currentPhase );
        }

        public void EnterPhase(IPhase phase)
        {
            if(phase != null && phases.Contains(phase))
            {
                phase.EnterPhase();
                phaseEnteredEvent?.Invoke( phase );
            }
        }

        public void ExitPhase(IPhase phase)
        {
            if(phase != null && phases.Contains(phase))
            {
                phase.ExitPhase();
                phaseExitedEvent?.Invoke( phase );
            }
        }

        public void Run()
        {
            if( PhotonNetwork.IsMasterClient && currentPhase != null && phases.Contains( currentPhase ) )
            {
                currentPhase.RunPhase(Time.deltaTime);
                phaseRunningEvent?.Invoke( currentPhase );
            }
        }
    }
}
