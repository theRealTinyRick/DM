// GENERATED AUTOMATICALLY FROM 'Assets/Architecture/Framework/Input/Resources/InputActionMain.inputactions'

using System;
using UnityEngine;
using UnityEngine.Experimental.Input;


namespace GameFramework.Input
{
    [Serializable]
    public class InputActionMain : InputActionAssetReference
    {
        public InputActionMain()
        {
        }
        public InputActionMain(InputActionAsset asset)
            : base(asset)
        {
        }
        private bool m_Initialized;
        private void Initialize()
        {
            // Gameplay
            m_Gameplay = asset.GetActionMap("Gameplay");
            m_Gameplay_Fire_1 = m_Gameplay.GetAction("Fire_1");
            m_Gameplay_Fire_2 = m_Gameplay.GetAction("Fire_2");
            // Menu
            m_Menu = asset.GetActionMap("Menu");
            m_Initialized = true;
        }
        private void Uninitialize()
        {
            m_Gameplay = null;
            m_Gameplay_Fire_1 = null;
            m_Gameplay_Fire_2 = null;
            m_Menu = null;
            m_Initialized = false;
        }
        public void SetAsset(InputActionAsset newAsset)
        {
            if (newAsset == asset) return;
            if (m_Initialized) Uninitialize();
            asset = newAsset;
        }
        public override void MakePrivateCopyOfActions()
        {
            SetAsset(ScriptableObject.Instantiate(asset));
        }
        // Gameplay
        private InputActionMap m_Gameplay;
        private InputAction m_Gameplay_Fire_1;
        private InputAction m_Gameplay_Fire_2;
        public struct GameplayActions
        {
            private InputActionMain m_Wrapper;
            public GameplayActions(InputActionMain wrapper) { m_Wrapper = wrapper; }
            public InputAction @Fire_1 { get { return m_Wrapper.m_Gameplay_Fire_1; } }
            public InputAction @Fire_2 { get { return m_Wrapper.m_Gameplay_Fire_2; } }
            public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled { get { return Get().enabled; } }
            public InputActionMap Clone() { return Get().Clone(); }
            public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        }
        public GameplayActions @Gameplay
        {
            get
            {
                if (!m_Initialized) Initialize();
                return new GameplayActions(this);
            }
        }
        // Menu
        private InputActionMap m_Menu;
        public struct MenuActions
        {
            private InputActionMain m_Wrapper;
            public MenuActions(InputActionMain wrapper) { m_Wrapper = wrapper; }
            public InputActionMap Get() { return m_Wrapper.m_Menu; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled { get { return Get().enabled; } }
            public InputActionMap Clone() { return Get().Clone(); }
            public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        }
        public MenuActions @Menu
        {
            get
            {
                if (!m_Initialized) Initialize();
                return new MenuActions(this);
            }
        }
    }
}
