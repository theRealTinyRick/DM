/*
 Author: Aaron Hines
 Description: controls phases
*/
using System.Linq;
using System.Collections.Generic;

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

        [SerializeField]
        public int currentIndex = 0;

        [SerializeField]
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

        private bool running = false;

        private void OnEnable()
        {
            Reset();
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
            foreach ( var _phase in phases )
            {
                _phase.phaseManager = this;
            }

            if (phases.Count > 0)
            {
                Reset();

                currentPhase = phases[currentIndex];
                EnterPhase( currentPhase );

                running = true;
            }
        }

        public void Reset()
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

        public void MoveToNextPhase()
        {
            int _nextIndex = currentIndex + 1;

            if (_nextIndex >= phases.Count)
            {
                if(loop)
                {
                    _nextIndex = 0;
                }
                else
                {
                    ExitPhase( currentPhase );
                    return;
                }
            }

            if (currentPhase != null)
            {
                previousPhase = currentPhase;
            }

            if(previousPhase != null)
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
            if(currentPhase != null && phases.Contains( currentPhase ) )
            {
                currentPhase.RunPhase(Time.deltaTime);
                phaseRunningEvent?.Invoke( currentPhase );
            }
        }
    }
}
