// GENERATED AUTOMATICALLY FROM 'Assets/DM/Framework/Input/Resources/InputActionsMain.inputactions'

using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace GameFramework.Input
{
    public class PlayerControls : IInputActionCollection
    {
        private InputActionAsset asset;
        public PlayerControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActionsMain"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""9fb9857c-84e7-4b80-9402-70eb098ac4d1"",
            ""actions"": [
                {
                    ""name"": ""Locomotion"",
                    ""type"": ""Value"",
                    ""id"": ""86e7859d-abbc-4347-bfa8-cf6f8c8ab0ce"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Selection"",
                    ""type"": ""Value"",
                    ""id"": ""86e7859d-abbc-4347-bfa8-cf6f8c8ab0ce"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraInput"",
                    ""type"": ""Value"",
                    ""id"": ""86e7859d-abbc-4347-bfa8-cf6f8c8ab0ce"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""JumpButton"",
                    ""type"": ""Value"",
                    ""id"": ""5764c5e3-b764-4a12-925c-c8c87892a9a2"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire1"",
                    ""type"": ""Value"",
                    ""id"": ""fc2e8298-5a41-4ab3-8cd9-215e02b9fd17"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire2"",
                    ""type"": ""Value"",
                    ""id"": ""fc2e8298-5a41-4ab3-8cd9-215e02b9fd17"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Focus1"",
                    ""type"": ""Value"",
                    ""id"": ""fc2e8298-5a41-4ab3-8cd9-215e02b9fd17"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Focus2"",
                    ""type"": ""Value"",
                    ""id"": ""fc2e8298-5a41-4ab3-8cd9-215e02b9fd17"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActionButton1"",
                    ""type"": ""Value"",
                    ""id"": ""fc2e8298-5a41-4ab3-8cd9-215e02b9fd17"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActionButton2"",
                    ""type"": ""Value"",
                    ""id"": ""fc2e8298-5a41-4ab3-8cd9-215e02b9fd17"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActionButton3"",
                    ""type"": ""Value"",
                    ""id"": ""fc2e8298-5a41-4ab3-8cd9-215e02b9fd17"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActionButton4"",
                    ""type"": ""Value"",
                    ""id"": ""fc2e8298-5a41-4ab3-8cd9-215e02b9fd17"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActionButton5"",
                    ""type"": ""Value"",
                    ""id"": ""fc2e8298-5a41-4ab3-8cd9-215e02b9fd17"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActionButton6"",
                    ""type"": ""Value"",
                    ""id"": ""fc2e8298-5a41-4ab3-8cd9-215e02b9fd17"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""762ee295-8197-4169-82c3-2ea11a778c09"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Locomotion"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""16e08e1d-e667-489b-8922-da54681d5972"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Locomotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""f09c9673-7570-414d-a38b-00040362892e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Locomotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""ef569492-c95e-4048-9d85-d157384796b5"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Locomotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""1229134c-6300-47fd-87fa-3dd1ecce91ae"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Locomotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""093552bc-d16c-4055-ab2b-5f4a2b16ac8a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Locomotion"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""4734e9c0-2c5d-4ffd-aba2-4d3ac5958835"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Locomotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""e2bcd680-95cc-4253-a170-5bac0a6ce614"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Locomotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""96e87baf-6a6d-4927-8b93-b76c7cb2e2de"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Locomotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""dea4f028-81b3-42ca-875d-b40e9ef1f3a7"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Locomotion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""c62d4f12-6fd7-4521-aacd-a94b843c356f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Selection"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""e003dbb1-fd83-4677-a2a3-91108bab119f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Selection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""7da52634-4400-48b3-b6c2-4e543a260df6"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Selection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""c613ae53-f02c-4a3c-a3f5-7a60ce5ca0d6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Selection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""00d3921f-224f-4d6a-af29-6b8ae7a631d5"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Selection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""14cbcd82-ba61-44a8-83b7-64e0517312f0"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Selection"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""e7810365-a06d-4f85-a874-78e6e28bf2ae"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Selection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""7d046dcc-5896-424f-943b-b911a86a85c6"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Selection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""e45599c9-c942-4482-b964-33c15a208a31"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Selection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""987457c7-9ac3-4b28-82db-10fcc8429edc"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Selection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""44e07923-103e-4ac4-8c14-978b5b381429"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Focus1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""520015e4-5cfd-405b-985b-d9d7a5814edf"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Focus2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""004a3c4b-20be-4d21-bed7-78cc66f131a5"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard Mouse"",
                    ""action"": ""Fire1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc8fe7e0-27f3-4cc3-8659-c007dbc1b148"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard Mouse"",
                    ""action"": ""Fire2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8c93c4a4-1c9e-40e8-903e-ee654c8fa09e"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActionButton1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3fd7cd63-4495-42a2-b290-913ecfa3a5d1"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActionButton2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e2d3bf28-d455-43d3-81c2-8f2375250d2a"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActionButton3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec890897-a352-4033-b56d-cc42e15a69d3"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActionButton4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16c4fa7f-c44c-45ef-b932-9525520edf83"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActionButton5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d2b7ba17-eede-4b91-a61f-7f39c6db3413"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActionButton6"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""396cf25b-3f7f-4f02-a385-8991fc4af4e9"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""JumpButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""43da3ae5-8e2c-4a0e-aaf2-9dc181c21cb2"",
            ""actions"": [],
            ""bindings"": []
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard Mouse"",
            ""bindingGroup"": ""Keyboard Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Gameplay
            m_Gameplay = asset.GetActionMap("Gameplay");
            m_Gameplay_Locomotion = m_Gameplay.GetAction("Locomotion");
            m_Gameplay_Selection = m_Gameplay.GetAction("Selection");
            m_Gameplay_CameraInput = m_Gameplay.GetAction("CameraInput");
            m_Gameplay_JumpButton = m_Gameplay.GetAction("JumpButton");
            m_Gameplay_Fire1 = m_Gameplay.GetAction("Fire1");
            m_Gameplay_Fire2 = m_Gameplay.GetAction("Fire2");
            m_Gameplay_Focus1 = m_Gameplay.GetAction("Focus1");
            m_Gameplay_Focus2 = m_Gameplay.GetAction("Focus2");
            m_Gameplay_ActionButton1 = m_Gameplay.GetAction("ActionButton1");
            m_Gameplay_ActionButton2 = m_Gameplay.GetAction("ActionButton2");
            m_Gameplay_ActionButton3 = m_Gameplay.GetAction("ActionButton3");
            m_Gameplay_ActionButton4 = m_Gameplay.GetAction("ActionButton4");
            m_Gameplay_ActionButton5 = m_Gameplay.GetAction("ActionButton5");
            m_Gameplay_ActionButton6 = m_Gameplay.GetAction("ActionButton6");
            // Menu
            m_Menu = asset.GetActionMap("Menu");
        }

        ~PlayerControls()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // Gameplay
        private readonly InputActionMap m_Gameplay;
        private IGameplayActions m_GameplayActionsCallbackInterface;
        private readonly InputAction m_Gameplay_Locomotion;
        private readonly InputAction m_Gameplay_Selection;
        private readonly InputAction m_Gameplay_CameraInput;
        private readonly InputAction m_Gameplay_JumpButton;
        private readonly InputAction m_Gameplay_Fire1;
        private readonly InputAction m_Gameplay_Fire2;
        private readonly InputAction m_Gameplay_Focus1;
        private readonly InputAction m_Gameplay_Focus2;
        private readonly InputAction m_Gameplay_ActionButton1;
        private readonly InputAction m_Gameplay_ActionButton2;
        private readonly InputAction m_Gameplay_ActionButton3;
        private readonly InputAction m_Gameplay_ActionButton4;
        private readonly InputAction m_Gameplay_ActionButton5;
        private readonly InputAction m_Gameplay_ActionButton6;
        public struct GameplayActions
        {
            private PlayerControls m_Wrapper;
            public GameplayActions(PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Locomotion => m_Wrapper.m_Gameplay_Locomotion;
            public InputAction @Selection => m_Wrapper.m_Gameplay_Selection;
            public InputAction @CameraInput => m_Wrapper.m_Gameplay_CameraInput;
            public InputAction @JumpButton => m_Wrapper.m_Gameplay_JumpButton;
            public InputAction @Fire1 => m_Wrapper.m_Gameplay_Fire1;
            public InputAction @Fire2 => m_Wrapper.m_Gameplay_Fire2;
            public InputAction @Focus1 => m_Wrapper.m_Gameplay_Focus1;
            public InputAction @Focus2 => m_Wrapper.m_Gameplay_Focus2;
            public InputAction @ActionButton1 => m_Wrapper.m_Gameplay_ActionButton1;
            public InputAction @ActionButton2 => m_Wrapper.m_Gameplay_ActionButton2;
            public InputAction @ActionButton3 => m_Wrapper.m_Gameplay_ActionButton3;
            public InputAction @ActionButton4 => m_Wrapper.m_Gameplay_ActionButton4;
            public InputAction @ActionButton5 => m_Wrapper.m_Gameplay_ActionButton5;
            public InputAction @ActionButton6 => m_Wrapper.m_Gameplay_ActionButton6;
            public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
            public void SetCallbacks(IGameplayActions instance)
            {
                if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
                {
                    Locomotion.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocomotion;
                    Locomotion.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocomotion;
                    Locomotion.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLocomotion;
                    Selection.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelection;
                    Selection.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelection;
                    Selection.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelection;
                    CameraInput.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCameraInput;
                    CameraInput.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCameraInput;
                    CameraInput.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCameraInput;
                    JumpButton.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJumpButton;
                    JumpButton.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJumpButton;
                    JumpButton.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJumpButton;
                    Fire1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFire1;
                    Fire1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFire1;
                    Fire1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFire1;
                    Fire2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFire2;
                    Fire2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFire2;
                    Fire2.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFire2;
                    Focus1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFocus1;
                    Focus1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFocus1;
                    Focus1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFocus1;
                    Focus2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFocus2;
                    Focus2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFocus2;
                    Focus2.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFocus2;
                    ActionButton1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionButton1;
                    ActionButton1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionButton1;
                    ActionButton1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionButton1;
                    ActionButton2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionButton2;
                    ActionButton2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionButton2;
                    ActionButton2.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionButton2;
                    ActionButton3.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionButton3;
                    ActionButton3.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionButton3;
                    ActionButton3.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionButton3;
                    ActionButton4.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionButton4;
                    ActionButton4.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionButton4;
                    ActionButton4.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionButton4;
                    ActionButton5.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionButton5;
                    ActionButton5.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionButton5;
                    ActionButton5.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionButton5;
                    ActionButton6.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionButton6;
                    ActionButton6.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionButton6;
                    ActionButton6.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActionButton6;
                }
                m_Wrapper.m_GameplayActionsCallbackInterface = instance;
                if (instance != null)
                {
                    Locomotion.started += instance.OnLocomotion;
                    Locomotion.performed += instance.OnLocomotion;
                    Locomotion.canceled += instance.OnLocomotion;
                    Selection.started += instance.OnSelection;
                    Selection.performed += instance.OnSelection;
                    Selection.canceled += instance.OnSelection;
                    CameraInput.started += instance.OnCameraInput;
                    CameraInput.performed += instance.OnCameraInput;
                    CameraInput.canceled += instance.OnCameraInput;
                    JumpButton.started += instance.OnJumpButton;
                    JumpButton.performed += instance.OnJumpButton;
                    JumpButton.canceled += instance.OnJumpButton;
                    Fire1.started += instance.OnFire1;
                    Fire1.performed += instance.OnFire1;
                    Fire1.canceled += instance.OnFire1;
                    Fire2.started += instance.OnFire2;
                    Fire2.performed += instance.OnFire2;
                    Fire2.canceled += instance.OnFire2;
                    Focus1.started += instance.OnFocus1;
                    Focus1.performed += instance.OnFocus1;
                    Focus1.canceled += instance.OnFocus1;
                    Focus2.started += instance.OnFocus2;
                    Focus2.performed += instance.OnFocus2;
                    Focus2.canceled += instance.OnFocus2;
                    ActionButton1.started += instance.OnActionButton1;
                    ActionButton1.performed += instance.OnActionButton1;
                    ActionButton1.canceled += instance.OnActionButton1;
                    ActionButton2.started += instance.OnActionButton2;
                    ActionButton2.performed += instance.OnActionButton2;
                    ActionButton2.canceled += instance.OnActionButton2;
                    ActionButton3.started += instance.OnActionButton3;
                    ActionButton3.performed += instance.OnActionButton3;
                    ActionButton3.canceled += instance.OnActionButton3;
                    ActionButton4.started += instance.OnActionButton4;
                    ActionButton4.performed += instance.OnActionButton4;
                    ActionButton4.canceled += instance.OnActionButton4;
                    ActionButton5.started += instance.OnActionButton5;
                    ActionButton5.performed += instance.OnActionButton5;
                    ActionButton5.canceled += instance.OnActionButton5;
                    ActionButton6.started += instance.OnActionButton6;
                    ActionButton6.performed += instance.OnActionButton6;
                    ActionButton6.canceled += instance.OnActionButton6;
                }
            }
        }
        public GameplayActions @Gameplay => new GameplayActions(this);

        // Menu
        private readonly InputActionMap m_Menu;
        private IMenuActions m_MenuActionsCallbackInterface;
        public struct MenuActions
        {
            private PlayerControls m_Wrapper;
            public MenuActions(PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputActionMap Get() { return m_Wrapper.m_Menu; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
            public void SetCallbacks(IMenuActions instance)
            {
                if (m_Wrapper.m_MenuActionsCallbackInterface != null)
                {
                }
                m_Wrapper.m_MenuActionsCallbackInterface = instance;
                if (instance != null)
                {
                }
            }
        }
        public MenuActions @Menu => new MenuActions(this);
        private int m_KeyboardMouseSchemeIndex = -1;
        public InputControlScheme KeyboardMouseScheme
        {
            get
            {
                if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.GetControlSchemeIndex("Keyboard Mouse");
                return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
            }
        }
        public interface IGameplayActions
        {
            void OnLocomotion(InputAction.CallbackContext context);
            void OnSelection(InputAction.CallbackContext context);
            void OnCameraInput(InputAction.CallbackContext context);
            void OnJumpButton(InputAction.CallbackContext context);
            void OnFire1(InputAction.CallbackContext context);
            void OnFire2(InputAction.CallbackContext context);
            void OnFocus1(InputAction.CallbackContext context);
            void OnFocus2(InputAction.CallbackContext context);
            void OnActionButton1(InputAction.CallbackContext context);
            void OnActionButton2(InputAction.CallbackContext context);
            void OnActionButton3(InputAction.CallbackContext context);
            void OnActionButton4(InputAction.CallbackContext context);
            void OnActionButton5(InputAction.CallbackContext context);
            void OnActionButton6(InputAction.CallbackContext context);
        }
        public interface IMenuActions
        {
        }
    }
}
