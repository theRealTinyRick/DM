using System.Collections.Generic;

using UnityEngine;

using Cinemachine;
using Sirenix.OdinInspector;

using GameFramework.Actors.Components;
using GameFramework.State;

namespace GameFramework.Gameplay.Components.Camera
{
    [System.Serializable]
    public class CameraOverrideEvent : UnityEngine.Events.UnityEvent<CameraPropertyOverride>
    {
    }

    [System.Serializable]
    public class CameraPropertyOverride
    {
        public int priority = 0;
        public float transitionSpeed = 0;

        [Space]
        public bool overrideInputAxisName = false;
        [Indent]
        [ShowIf("overrideInputAxisName")]
        public string inputAxisXName = "";
        [Indent]
        [ShowIf("overrideInputAxisName")]
        public string inputAxisYName = "";


        [Space]
        public bool overrideInputValue = false;
        [Indent]
        [ShowIf("overrideInputValue")]
        public float inputAxisXValue = 0;
        [Indent]
        [ShowIf("overrideInputValue")]
        public float inputAxisYValue = 0;


        [Space]
        public bool overrideRecenter = false;
        [Indent]
        [ShowIf("overrideRecenter")]
        public bool recenterToTargetHeading;
        [Indent]
        [Indent]
        [ShowIf("recenterToTargetHeading")]
        public float recenterToTargetHeadingWaitTime;
        [Indent]
        [Indent]
        [ShowIf("recenterToTargetHeading")]
        public float recenterToTargetHeadingTime;

        [Indent]
        [ShowIf("overrideRecenter")]
        public bool recenterYAxis;
        [Indent]
        [Indent]
        [ShowIf("recenterYAxis")]
        public float recenterYAxisTime;
        [Indent]
        [Indent]
        [ShowIf("recenterYAxis")]
        public float recenterYAxisWaitTime;


        [Space]
        public bool overrideOffset = false;
        [Indent]
        [ShowIf("overrideOffset")]
        public Vector3 cameraOffset;
        

        [Space]
        public bool overrideLookAt = false;
        [Indent]
        [ShowIf("overrideLookAt")]
        public Transform cameraLookAt;


        [Space]
        public bool overrideZoom = false;
        [Indent]
        [ShowIf("overrideZoom")]
        public float width = 2.0f;
        [Indent]
        [ShowIf("overrideZoom")]
        [Range(0, 20)]
        public float damping = 1.0f;
        [Indent]
        [ShowIf("overrideZoom")]
        [Range(1, 179)]
        public float minFOV;
        [Indent]
        [ShowIf("overrideZoom")]
        [Range(1, 179)]
        public float maxFOV;


        [Space]
        public bool overrideBindingMode = false;
        [Indent]
        [ShowIf("overrideBindingMode")]
        public CinemachineTransposer.BindingMode bindingMode;

        [Space]
        public bool overrideSpline = false;
        [Indent]
        [ShowIf("overrideSpline")]
        [Range(0, 1)]
        public float splineCurvature;

        [Space]
        public bool oveerrideTopRig = false;
        [Indent]
        [ShowIf("oveerrideTopRig")]
        public float topRigHeight;
        [Indent]
        [ShowIf("oveerrideTopRig")]
        public float topRigRadius;

        [Space]
        public bool oveerrideMiddleRig = false;
        [Indent]
        [ShowIf("oveerrideMiddleRig")]
        public float middleRigHeight;
        [Indent]
        [ShowIf("oveerrideMiddleRig")]
        public float middleRigRadius;

        [Space]
        public bool oveerrideBottomRig = false;
        [Indent]
        [ShowIf("oveerrideBottomRig")]
        public float bottomRigHeight;
        [Indent]
        [ShowIf("oveerrideBottomRig")]
        public float bottomRigRadius;

    }

    [RequireComponent(typeof(StateComponent))]
    public class CameraOverrideComponent : ActorComponent
    {
        [TabGroup(Tabs.OVERRIDES)]
        [SerializeField]
        private Dictionary<StateType, CameraPropertyOverride> overrides = new Dictionary<StateType, CameraPropertyOverride>();

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        private float defaultTransitionSpeed;


        [TabGroup(Tabs.EVENTS)]
        [SerializeField]
        public CameraOverrideEvent cameraOverrideEvent;

        private bool initialized = false;
        private CameraPropertyOverride original;
        private CameraPropertyOverride currentOverride;
        private Vector3 originalLookAtOffset;

        // components
        private CinemachineFreeLook freeLookComponent;
        private CinemachineFollowZoom followZoomComponent;
        private CinemachineCameraOffset cameraOffsetComponent;

        private StateComponent stateComponent;

        public override void InitializeComponent()
        {
            if(freeLookComponent == null)
            {
                freeLookComponent = owner.GetComponentInChildren<CinemachineFreeLook>();
                if (freeLookComponent == null)
                {
                    return;
                }
            }

            if(followZoomComponent == null)
            {
                followZoomComponent = owner.GetComponentInChildren<CinemachineFollowZoom>();
                if (followZoomComponent == null)
                {
                    Debug.LogError("Please Add the Follow Zoom Extenstion to your Cinemachine Free Look Camera");
                    return;
                }
            }

            if(cameraOffsetComponent == null)
            {
                cameraOffsetComponent = owner.GetComponentInChildren<CinemachineCameraOffset>();
                if (cameraOffsetComponent == null)
                {
                    Debug.LogError("Please Add the Follow Camera Offset to your Cinemachine Free Look Camera");
                    return;
                }
            }

            if(stateComponent == null)
            {
                stateComponent = owner.GetActorComponent<StateComponent>();
                if (stateComponent == null) return;
            }

            original = CreateOriginal();
            initialized = true;
        }

        public override void DisableComponent()
        {
            initialized = false;
            if(original != null)
            {
                SetOverride(original, false);
            }
        }

        private void LateUpdate()
        {
            EvalStates();
        }

        public void EvalStates()
        {
            if(initialized)
            {
                // eval all states on the state component
                List<State.StateType> _trueStates = new List<State.StateType>();

                foreach (var _pair in overrides)
                {
                    if(stateComponent.GetState(_pair.Key))
                    {
                        _trueStates.Add(_pair.Key);
                    }
                }

                if(_trueStates.Count > 0)
                {
                    StateType _highestPriorityState = _trueStates[0];
                    int _highestPrioity = overrides[_highestPriorityState].priority;

                    foreach (StateType _state in _trueStates)
                    {
                        if(_state != _highestPriorityState)
                        {
                            if(overrides[_state].priority > _highestPrioity)
                            {
                                _highestPriorityState = _state;
                                _highestPrioity = overrides[_state].priority;
                            }
                        }
                    }

                    SetOverride(original, true); // reset values then set the overrides
                    SetOverride(overrides[_highestPriorityState]);
                    return;
                }

                SetOverride(original, true);
            }
        }

        public void SetFromState(StateType state)
        {
            if(overrides.ContainsKey(state))
            {
                SetOverride(overrides[state]);
            }
        }

        private float currentTransitionTime = 0;

        public void SetOverride(CameraPropertyOverride camOverride, bool settingOriginal = false)
        {
            if(currentOverride != camOverride)
            {
                currentTransitionTime = 0;
            }

            currentTransitionTime += Time.deltaTime * camOverride.transitionSpeed;
            if(currentTransitionTime > 1)
            {
                currentTransitionTime = 1;
            }


            if(camOverride.overrideInputAxisName || settingOriginal)
            {
                freeLookComponent.m_XAxis.m_InputAxisName = camOverride.inputAxisXName;
                freeLookComponent.m_YAxis.m_InputAxisName = camOverride.inputAxisYName;
            }

            if(camOverride.overrideInputValue || settingOriginal)
            {
                freeLookComponent.m_XAxis.m_InputAxisValue = camOverride.inputAxisXValue;
                freeLookComponent.m_YAxis.m_InputAxisValue = camOverride.inputAxisYValue;
            }

            if(camOverride.overrideRecenter || settingOriginal)
            {
                freeLookComponent.m_RecenterToTargetHeading.m_enabled = camOverride.recenterToTargetHeading;
                freeLookComponent.m_RecenterToTargetHeading.m_WaitTime = camOverride.recenterToTargetHeadingWaitTime;
                freeLookComponent.m_RecenterToTargetHeading.m_RecenteringTime = camOverride.recenterToTargetHeadingTime;

                freeLookComponent.m_YAxisRecentering.m_enabled = camOverride.recenterYAxis;
                freeLookComponent.m_YAxisRecentering.m_WaitTime = camOverride.recenterYAxisWaitTime;
                freeLookComponent.m_YAxisRecentering.m_RecenteringTime = camOverride.recenterYAxisTime;
            }

            if(camOverride.overrideLookAt || settingOriginal)
            {
                freeLookComponent.LookAt = camOverride.cameraLookAt;
            }

            if (camOverride.overrideOffset || settingOriginal)
            {
                cameraOffsetComponent.m_Offset = Vector3.Lerp(cameraOffsetComponent.m_Offset, camOverride.cameraOffset, currentTransitionTime);
            }

            if(camOverride.overrideZoom || settingOriginal)
            {
                followZoomComponent.m_Damping = camOverride.damping;
                followZoomComponent.m_MinFOV = camOverride.minFOV;
                followZoomComponent.m_MaxFOV = camOverride.maxFOV;
                followZoomComponent.m_Width = camOverride.width;
            }

            if(camOverride.overrideBindingMode || settingOriginal)
            {
                freeLookComponent.m_BindingMode = camOverride.bindingMode;
            }

            if(camOverride.overrideSpline || settingOriginal)
            {
                freeLookComponent.m_SplineCurvature = camOverride.splineCurvature;
            }

            if(camOverride.oveerrideTopRig || settingOriginal)
            {
                freeLookComponent.m_Orbits[0].m_Height = Mathf.Lerp(freeLookComponent.m_Orbits[0].m_Height, camOverride.topRigHeight, currentTransitionTime);
                freeLookComponent.m_Orbits[0].m_Radius = Mathf.Lerp(freeLookComponent.m_Orbits[0].m_Radius, camOverride.topRigRadius, currentTransitionTime) ;
            }

            if(camOverride.oveerrideMiddleRig || settingOriginal)
            {
                freeLookComponent.m_Orbits[1].m_Height = Mathf.Lerp(freeLookComponent.m_Orbits[1].m_Height, camOverride.middleRigHeight, currentTransitionTime);
                freeLookComponent.m_Orbits[1].m_Radius = Mathf.Lerp(freeLookComponent.m_Orbits[1].m_Radius, camOverride.middleRigRadius, currentTransitionTime) ;
            }

            if(camOverride.oveerrideBottomRig || settingOriginal)
            {
                freeLookComponent.m_Orbits[2].m_Height = Mathf.Lerp(freeLookComponent.m_Orbits[2].m_Height, camOverride.bottomRigHeight, currentTransitionTime);
                freeLookComponent.m_Orbits[2].m_Radius = Mathf.Lerp(freeLookComponent.m_Orbits[2].m_Radius, camOverride.bottomRigRadius, currentTransitionTime);
            }

            currentOverride = camOverride;
        }

        private CameraPropertyOverride CreateOriginal()
        {
            CameraPropertyOverride _newOverride = new CameraPropertyOverride();

            if (freeLookComponent != null)
            {
                _newOverride.inputAxisXName = freeLookComponent.m_XAxis.m_InputAxisName;
                _newOverride.inputAxisYName = freeLookComponent.m_YAxis.m_InputAxisName;

                _newOverride.recenterToTargetHeading = freeLookComponent.m_RecenterToTargetHeading.m_enabled;
                _newOverride.recenterToTargetHeadingWaitTime = freeLookComponent.m_RecenterToTargetHeading.m_WaitTime;
                _newOverride.recenterToTargetHeadingTime = freeLookComponent.m_RecenterToTargetHeading.m_RecenteringTime;

                _newOverride.recenterYAxis = freeLookComponent.m_YAxisRecentering.m_enabled;
                _newOverride.recenterYAxisWaitTime = freeLookComponent.m_YAxisRecentering.m_WaitTime;
                _newOverride.recenterYAxisTime = freeLookComponent.m_YAxisRecentering.m_RecenteringTime;

                _newOverride.bindingMode = freeLookComponent.m_BindingMode;

                _newOverride.splineCurvature = freeLookComponent.m_SplineCurvature;

                _newOverride.cameraOffset = cameraOffsetComponent.m_Offset;

                _newOverride.topRigHeight = freeLookComponent.m_Orbits[0].m_Height;
                _newOverride.topRigRadius = freeLookComponent.m_Orbits[0].m_Radius;

                _newOverride.middleRigHeight = freeLookComponent.m_Orbits[1].m_Height;
                _newOverride.middleRigRadius = freeLookComponent.m_Orbits[1].m_Radius;

                _newOverride.bottomRigHeight = freeLookComponent.m_Orbits[2].m_Height;
                _newOverride.bottomRigRadius = freeLookComponent.m_Orbits[2].m_Radius;

                _newOverride.cameraLookAt = freeLookComponent.LookAt;

                originalLookAtOffset = freeLookComponent.LookAt.position - owner.transform.position;

                _newOverride.transitionSpeed = defaultTransitionSpeed;

                _newOverride.minFOV = followZoomComponent.m_MinFOV;
                _newOverride.maxFOV = followZoomComponent.m_MaxFOV;
                _newOverride.width = followZoomComponent.m_Width;
                _newOverride.damping = followZoomComponent.m_Damping;

                return _newOverride;
            }

            return _newOverride;
        }
    }
}
