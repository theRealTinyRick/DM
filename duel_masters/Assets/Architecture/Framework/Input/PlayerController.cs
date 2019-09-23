/*
 Author: Aaron Hines
 Edits By: 
 Description: Used as a wrapper for Unity's input systems. Possesses a pawn and passes input to them
 */
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Experimental.Input;

using GameFramework.Actors;
using GameFramework.Actors.Components;

namespace GameFramework
{
    [System.Serializable]
    public class PlayerController
    {
        [SerializeField]
        public Dictionary<Pawn, PlayerInputComponent> controlledPawns
        {
            get;
            private set;
        } = new Dictionary<Pawn, PlayerInputComponent>();

        public List<InputAction> inputs
        {
            get ;
            private set;
        }

        public InputActionAsset playerControls
        {
            get;
            private set;
        }

        #region Gameplay Input Events
        private InputActionMap gameplayActionMap;

        // vectors
        private InputAction locomotionInput; //WASD or left stick
        private InputAction cameraInput; // mouse or right stick
        private InputAction selectionInput; // mouse or right stick

        // buttons
        private InputAction fire1;
        private bool _fire1Held = false;
        public bool fire1Held { get => _fire1Held; private set => _fire1Held = value; }

        private InputAction fire2;
        private bool _fire2Held = false;
        public bool fire2Held { get => _fire2Held; private set => _fire2Held = value; }

        private InputAction focus1;
        private bool _focus1Held = false;
        public bool focus1Held { get => _focus1Held; private set => _focus1Held = value; }

        private InputAction focus2;
        private bool _focus2Held = false;
        public bool focus2Held { get => _focus2Held; private set => _focus2Held = value; }

        private InputAction actionButtonOne;
        private bool _actionOneHeld = false;
        public bool actionOneHeld { get => _actionOneHeld; private set => _actionOneHeld = value; }

        private InputAction actionButtonTwo;
        private bool _actionTwoHeld = false;
        public bool actionTwoHeld { get => _actionTwoHeld; private set => _actionTwoHeld = value; } 

        private InputAction actionButtonThree;
        private bool _actionThreeHeld = false;
        public bool actionThreeHeld { get => _actionThreeHeld; private set => _actionThreeHeld = value; }

        private InputAction actionButtonFour;
        private bool _actionFourHeld = false;   
        public bool actionFourHeld { get => _actionFourHeld; private set => _actionFourHeld = value; }

        private InputAction actionButtonFive;
        private bool _actionFiveHeld = false; 
        public bool actionFiveHeld { get => _actionFiveHeld; private set => _actionFiveHeld = value; }

        private InputAction actionButtonSix;
        private bool _actionSixHeld = false;
        public bool actionSixHeld { get => _actionSixHeld; private set => _actionSixHeld = value; }

        private InputAction jumpInput;
        private bool _jumpHeld = false;
        public bool jumpHeld { get => _jumpHeld; private set => _jumpHeld = value; }
        #endregion

        #region Gameplay Input Values
        private Vector2 _locomotionInputValue = new Vector2();
        public Vector2 locomotionInputValue { get => _locomotionInputValue; }

        private Vector2 _cameraInputValue = new Vector2();
        public Vector2 cameraInputValue { get => _cameraInputValue; }

        private Vector2 _selectionInputValue = new Vector2();
        public Vector2 selectionInputValue { get => _selectionInputValue; }
        #endregion

        #region Menu Input Events
        private InputActionMap menuActionMap;
        #endregion

        public PlayerController () { }

        public PlayerController(InputActionAsset playerControls)
        {
            this.playerControls = playerControls;
        }

        public void Initialize()
        {
            if(playerControls != null)
            {
                // TODO: Add all the input actions back to the system
                // init buttons 
                gameplayActionMap = playerControls.GetActionMap("Gameplay");
                menuActionMap = playerControls.GetActionMap("Menu");

                //locomotionInput = gameplayActionMap.GetAction("Locomotion");
                //locomotionInput.performed += UpdateLocomotion;
                //locomotionInput.cancelled += UpdateLocomotion;

                //cameraInput = gameplayActionMap.GetAction("CameraInput");
                //cameraInput.performed += UpdateCameraInput;
                //cameraInput.cancelled += UpdateCameraInput;

                //selectionInput = gameplayActionMap.GetAction("Selection");
                //selectionInput.performed += UpdateSelectionInput;
                //selectionInput.cancelled += UpdateSelectionInput;

                fire1 = gameplayActionMap.GetAction("Fire_1");
                fire1.performed += HandleFireOne;
                fire1.cancelled += HandleFireOne;

                fire2 = gameplayActionMap.GetAction("Fire_2");
                fire2.performed += HandleFireTwo;
                fire2.cancelled += HandleFireTwo;

                //focus1 = gameplayActionMap.GetAction("Focus1");
                //focus1.performed += HandleFocusOne;
                //focus1.cancelled += HandleFocusOne;

                //focus2 = gameplayActionMap.GetAction("Focus2");
                //focus2.performed += HandleFocusTwo;
                //focus2.cancelled += HandleFocusTwo;

                //actionButtonOne = gameplayActionMap.GetAction("ActionButton1");
                //actionButtonOne.performed += HandleActionOne;
                //actionButtonOne.cancelled += HandleActionOne;

                //actionButtonTwo = gameplayActionMap.GetAction("ActionButton2");
                //actionButtonTwo.performed += HandleActionTwo;
                //actionButtonTwo.cancelled += HandleActionTwo;

                //actionButtonThree = gameplayActionMap.GetAction("ActionButton3");
                //actionButtonThree.performed += HandleActionThree;
                //actionButtonThree.cancelled += HandleActionThree;

                //actionButtonFour = gameplayActionMap.GetAction("ActionButton4");
                //actionButtonFour.performed += HandleActionFour;
                //actionButtonFour.cancelled += HandleActionFour;

                //actionButtonFive = gameplayActionMap.GetAction("ActionButton5");
                //actionButtonFive.performed += HandleActionFive;
                //actionButtonFive.cancelled += HandleActionFive;

                //actionButtonSix = gameplayActionMap.GetAction("ActionButton6");
                //actionButtonSix.performed += HandleActionSix;
                //actionButtonSix.cancelled += HandleActionSix;

                //jumpInput = gameplayActionMap.GetAction("JumpButton");
                //jumpInput.performed += HandleJumpButton;
                //jumpInput.cancelled += HandleJumpButton;
            }
        }

        public void Enable()
        {
            locomotionInput?.Enable();
            cameraInput?.Enable();
            selectionInput?.Enable();

            fire1?.Enable();
            fire2?.Enable();
            focus1?.Enable();
            focus2?.Enable();

            actionButtonOne?.Enable();
            actionButtonTwo?.Enable();
            actionButtonThree?.Enable();
            actionButtonFour?.Enable();
            actionButtonFive?.Enable();
            actionButtonSix?.Enable();

            jumpInput?.Enable();
        }

        public void Disable()
        {
            locomotionInput?.Disable();
            cameraInput?.Disable();

            fire1?.Disable();
            fire2?.Disable();
            focus1?.Disable();
            focus2?.Disable();

            actionButtonOne?.Disable();
            actionButtonTwo?.Disable();
            actionButtonThree?.Disable();
            actionButtonFour?.Disable();
            actionButtonFive?.Disable();
            actionButtonSix?.Disable();

            jumpInput?.Disable();
        }

        public virtual void Tick(float deltaTime)
        {
            // update held values
            foreach (KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
            {
                PlayerInputComponent _inputComponent = _pair.Value;

                if (jumpHeld)
                    _inputComponent.jumpButtonHeldEvent.Invoke();


                if(fire1Held)
                    _inputComponent.fireOneHeldEvent.Invoke();

                if (fire2Held)
                    _inputComponent.fireTwoHeldEvent.Invoke();

                if (focus1Held)
                    _inputComponent.focusOneHeldEvent.Invoke();

                if (focus2Held)
                    _inputComponent.focusTwoHeldEvent.Invoke();


                if (actionOneHeld)
                    _inputComponent.actionOneHeldEvent.Invoke();

                if (actionTwoHeld)
                    _inputComponent.actionTwoHeldEvent.Invoke();

                if (actionThreeHeld)
                    _inputComponent.actionThreeHeldEvent.Invoke();

                if (actionFourHeld)
                    _inputComponent.actionFourHeldEvent.Invoke();

                if (actionFiveHeld)
                    _inputComponent.actionFiveUpEvent.Invoke();

                if (actionSixHeld)
                    _inputComponent.actionSixHeldEvent.Invoke();
            }
        }

        #region Input Handlers
        private void UpdateLocomotion(InputAction.CallbackContext context)
        {
            _locomotionInputValue = context.ReadValue<Vector2>();

            foreach(KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
            {
                _pair.Value.SetLocomotionInput(_locomotionInputValue);
            }
        }

        private void UpdateCameraInput(InputAction.CallbackContext context)
        {
            _cameraInputValue = context.ReadValue<Vector2>();

            foreach (KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
            {
                _pair.Value.cameraInputEvent.Invoke(_cameraInputValue);
            }
        }

        private void UpdateSelectionInput(InputAction.CallbackContext context)
        {
            _selectionInputValue = context.ReadValue<Vector2>();

            foreach (KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
            {
                _pair.Value.selectionInputEvent.Invoke(_selectionInputValue);
            }
        }

        private void HandleFireOne(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                _fire1Held = true;
                foreach (KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
                {
                    _pair.Value.fireOneDownEvent.Invoke();
                }
            }

            if(context.cancelled)
            {
                _fire1Held = false;
                foreach (KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
                {
                    _pair.Value.fireOneUpEvent.Invoke();
                }
            }
        }

        private void HandleFireTwo(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _fire2Held = true;
                foreach (KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
                {
                    _pair.Value.fireTwoDownEvent.Invoke();
                }
            }

            if (context.cancelled)
            {
                _fire2Held = false;
                foreach (KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
                {
                    _pair.Value.fireTwoUpEvent.Invoke();
                }
            }
        }

        private void HandleFocusOne(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                _focus1Held = true;
                foreach (KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
                {
                    _pair.Value.focusOneDownEvent.Invoke();
                }
            }

            if(context.cancelled)
            {
                _fire1Held = false;
                foreach (KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
                {
                    _pair.Value.focusOneUpEvent.Invoke();
                }
            }
        }

        private void HandleFocusTwo(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                _focus2Held = true;
                foreach (KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
                {
                    _pair.Value.focusTwoDownEvent.Invoke();
                }
            }

            if(context.cancelled)
            {
                _focus2Held = false;

                foreach (KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
                {
                    _pair.Value.focusTwoUpEvent.Invoke();
                }
            }
        }

        private void HandleActionOne(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                actionOneHeld = true;
                foreach (KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
                {
                    _pair.Value.actionOneDownEvent.Invoke();
                }
            }

            if(context.cancelled)
            {
                actionOneHeld = false;
                foreach (KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
                {
                    _pair.Value.actionOneUpEvent.Invoke();
                }
            }
        }

        private void HandleActionTwo(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                actionTwoHeld = true;
                foreach (KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
                {
                    _pair.Value.actionTwoDownEvent.Invoke();
                }
            }

            if (context.cancelled)
            {
                actionTwoHeld = false;
                foreach (KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
                {
                    _pair.Value.actionTwoUpEvent.Invoke();
                }
            }
        }

        private void HandleActionThree(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                actionThreeHeld = true;
                foreach (KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
                {
                    _pair.Value.actionThreeDownEvent.Invoke();
                }
            }

            if (context.cancelled)
            {
                actionThreeHeld = false;
                foreach (KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
                {
                    _pair.Value.actionThreeUpEvent.Invoke();
                }
            }
        }

        private void HandleActionFour(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                actionFourHeld = true;
                foreach (KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
                {
                    _pair.Value.actionFourDownEvent.Invoke();
                }
            }

            if (context.cancelled)
            {
                actionFourHeld = false;
                foreach (KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
                {
                    _pair.Value.actionFourUpEvent.Invoke();
                }
            }
        }

        private void HandleActionFive(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                actionFiveHeld = true;
                foreach (KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
                {
                    _pair.Value.actionFiveDownEvent.Invoke();
                }
            }

            if (context.cancelled)
            {
                actionFiveHeld = false;
                foreach (KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
                {
                    _pair.Value.actionFiveUpEvent.Invoke();
                }
            }
        }

        private void HandleActionSix(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                actionSixHeld = true;
                foreach (KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
                {
                    _pair.Value.actionSixDownEvent.Invoke();
                }
            }

            if (context.cancelled)
            {
                actionSixHeld = false;
                foreach (KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
                {
                    _pair.Value.actionSixUpEvent.Invoke();
                }
            }
        }

        private void HandleJumpButton(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                _jumpHeld = true;
                foreach (KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
                {
                    _pair.Value.jumpButtonDownEvent.Invoke();
                }
            }

            if(context.cancelled)
            {
                _jumpHeld = false;
                foreach (KeyValuePair<Pawn, PlayerInputComponent> _pair in controlledPawns)
                {
                    _pair.Value.jumpButtonUpEvent.Invoke();
                }
            }
        }
        #endregion

        #region Possess
        /// <summary>
        /// Adds the pawn to this player controller. Any input gotten from this will be passes the pawn
        /// </summary>
        /// <param name="pawn"></param>
        public void Possess(Pawn pawn)
        {
            pawn.SetHasController(true, this);
            controlledPawns.Add(pawn, pawn.GetActorComponent<PlayerInputComponent>());
        }

        public void Disown(Pawn pawn)
        {
            if(controlledPawns.ContainsKey(pawn))
            {
                pawn.SetHasController(false, null);
            }
        }

        public void DisownAll()
        {
            foreach(Pawn _pawn in controlledPawns.Keys)
            {
                Disown(_pawn);
            }

            controlledPawns.Clear();
        }

        #endregion

    }
}
