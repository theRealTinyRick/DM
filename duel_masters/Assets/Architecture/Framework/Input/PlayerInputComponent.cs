/*
 Author: Aaron Hines
 Edits By: 
 Description:
 */
using UnityEngine;
using Sirenix.OdinInspector;

using GameFramework.Core.GameFramework.InputEvents;

namespace GameFramework.Actors.Components
{
    public class PlayerInputComponent : ActorComponent
    {
        [FoldoutGroup("Locomotion")]
        public LocomotionInputEvent locomotionInputUpdateEvent = new LocomotionInputEvent();

        private Vector3 globalInput = new Vector3();// stored for better use in this component

        [FoldoutGroup("Locomotion")]
        public LocomotionInputEvent orientedLocomotionInputUpdateEvent = new LocomotionInputEvent();

        [FoldoutGroup("Mouse - Right Stick")]
        public CameraInputEvent cameraInputEvent = new CameraInputEvent();

        [FoldoutGroup("Mouse - Right Stick")]
        public SelectionInputEvent selectionInputEvent = new SelectionInputEvent();



        [FoldoutGroup("Jump")]
        public JumpButtonEvent jumpButtonDownEvent = new JumpButtonEvent();

        [FoldoutGroup("Jump")]
        public JumpButtonEvent jumpButtonUpEvent = new JumpButtonEvent();

        [FoldoutGroup("Jump")]
        public JumpButtonEvent jumpButtonHeldEvent = new JumpButtonEvent();



        [FoldoutGroup("Fire One")]
        public FireInputEvent fireOneDownEvent = new FireInputEvent();

        [FoldoutGroup("Fire One")]
        public FireInputEvent fireOneUpEvent = new FireInputEvent();

        [FoldoutGroup("Fire One")]
        public FireInputEvent fireOneHeldEvent = new FireInputEvent();

        [FoldoutGroup("Fire Two")]
        public FireInputEvent fireTwoDownEvent = new FireInputEvent();

        [FoldoutGroup("Fire Two")]
        public FireInputEvent fireTwoUpEvent = new FireInputEvent();

        [FoldoutGroup("Fire Two")]
        public FireInputEvent fireTwoHeldEvent = new FireInputEvent();



        [FoldoutGroup("FocusOne")]
        public FocusInputEvent focusOneDownEvent = new FocusInputEvent();

        [FoldoutGroup("FocusOne")]
        public FocusInputEvent focusOneUpEvent = new FocusInputEvent();

        [FoldoutGroup("FocusOne")]
        public FocusInputEvent focusOneHeldEvent = new FocusInputEvent();



        [FoldoutGroup("FocusOne")]
        public FocusInputEvent focusTwoDownEvent = new FocusInputEvent();

        [FoldoutGroup("FocusOne")]
        public FocusInputEvent focusTwoUpEvent = new FocusInputEvent();

        [FoldoutGroup("FocusOne")]
        public FocusInputEvent focusTwoHeldEvent = new FocusInputEvent();



        [FoldoutGroup("Action One")]
        public ActionButtonEvent actionOneDownEvent = new ActionButtonEvent();

        [FoldoutGroup("Action One")]
        public ActionButtonEvent actionOneUpEvent = new ActionButtonEvent();

        [FoldoutGroup("Action One")]
        public ActionButtonEvent actionOneHeldEvent = new ActionButtonEvent();



        [FoldoutGroup("Action Two")]
        public ActionButtonEvent actionTwoDownEvent = new ActionButtonEvent();

        [FoldoutGroup("Action Two")]
        public ActionButtonEvent actionTwoUpEvent = new ActionButtonEvent();

        [FoldoutGroup("Action Two")]
        public ActionButtonEvent actionTwoHeldEvent = new ActionButtonEvent();



        [FoldoutGroup("Action Three")]
        public ActionButtonEvent actionThreeDownEvent = new ActionButtonEvent();

        [FoldoutGroup("Action Three")]
        public ActionButtonEvent actionThreeUpEvent = new ActionButtonEvent();

        [FoldoutGroup("Action Three")]
        public ActionButtonEvent actionThreeHeldEvent = new ActionButtonEvent();



        [FoldoutGroup("Action Four")]
        public ActionButtonEvent actionFourDownEvent = new ActionButtonEvent();

        [FoldoutGroup("Action Four")]
        public ActionButtonEvent actionFourUpEvent = new ActionButtonEvent();

        [FoldoutGroup("Action Four")]
        public ActionButtonEvent actionFourHeldEvent = new ActionButtonEvent();



        [FoldoutGroup("Action Five")]
        public ActionButtonEvent actionFiveDownEvent = new ActionButtonEvent();

        [FoldoutGroup("Action Five")]
        public ActionButtonEvent actionFiveUpEvent = new ActionButtonEvent();

        [FoldoutGroup("Action Five")]
        public ActionButtonEvent actionFiveHeldEvent = new ActionButtonEvent();



        [FoldoutGroup("Action Six")]
        public ActionButtonEvent actionSixDownEvent = new ActionButtonEvent();

        [FoldoutGroup("Action Six")]
        public ActionButtonEvent actionSixUpEvent = new ActionButtonEvent();

        [FoldoutGroup("Action Six")]
        public ActionButtonEvent actionSixHeldEvent = new ActionButtonEvent();

        private bool orientInput = false;
        private bool onlyTrackY = false;

        public Transform transformToOrientTo
        {
            get;
            private set;
        }

        public Transform locomotionOrientation
        {
            get;
            private set;
        }

        public override void InitializeComponent()
        {
            if(locomotionOrientation == null)
            {
                locomotionOrientation = new GameObject().transform;
                locomotionOrientation.name = (owner.name + "'s orientationController");
                DontDestroyOnLoad(locomotionOrientation.gameObject);
            }
        }

        public override void DisableComponent()
        {
        }

        private void Update()
        {
            UpdateOrientationDirection();
            UpdateOrientedLocomotionInput();
        }

        public void SetLocomotionInput(Vector3 globalInput)
        {
            this.globalInput = globalInput;
            locomotionInputUpdateEvent.Invoke(globalInput);
        }

        public void UpdateOrientedLocomotionInput()
        {
            if (orientInput && locomotionOrientation != null)
            {
                Vector3 _oreientedInput = globalInput;

                _oreientedInput = (locomotionOrientation.right * _oreientedInput.x) + locomotionOrientation.forward * _oreientedInput.y;
                orientedLocomotionInputUpdateEvent.Invoke(_oreientedInput);
            }
        }

        public void OrientLocomotionInput(Transform transformToOrientTo, bool onlyTrackY)
        {
            orientInput = true;
            this.onlyTrackY = onlyTrackY;
            this.transformToOrientTo = transformToOrientTo;
        }

        private void UpdateOrientationDirection()
        {
            if(locomotionOrientation != null && transformToOrientTo != null)
            {
                Quaternion _rotation = transformToOrientTo.rotation;
                if(onlyTrackY)
                {
                    _rotation.x = 0;
                    _rotation.z = 0;
                }

                locomotionOrientation.rotation = _rotation;
            }
        }
    }
}
