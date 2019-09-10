/*
 Author: Aaron Hines
 Edits By: 
 Description: keeps track of all of the playercontrollers and helps to link them up to pawns
 */
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using Sirenix.OdinInspector;

using GameFramework.Actors;

namespace GameFramework
{
    public class GameManager : Singleton_SerializedMonobehaviour<GameManager>
    {
        [TabGroup("Player")]
        [SerializeField]
        public int maxPlayerCount = 1;

        [TabGroup("Player")]
        [SerializeField]
        private Dictionary<PlayerController, List<Pawn>> playerControllers = new Dictionary<PlayerController, List<Pawn>>();

        [TabGroup("Player")]
        [SerializeField]
        private InputActionAsset playerControls;

        [TabGroup("General")]
        [SerializeField]
        private bool hideMouseOnStartup = false;

        private bool initialized = false;

        private void Start()
        {
            InitializePlayerControllers();

            if(hideMouseOnStartup)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

        }

        public void OnEnable()
        {
            EnableInput();
        }

        public void OnDisable()
        {
            DisableInput();
        }

        /// <summary>
        /// Used to pair a player controller with a Pawn
        /// </summary>
        /// <param name="pawn"></param>
        /// <param name="playerIndex"></param>
        public static bool Possess(Pawn pawn, int playerIndex)
        {
            if (!instance.initialized) InitializePlayerControllers();

            if (instance.playerControllers.Count > playerIndex)
            {
                PlayerController _controller = instance.playerControllers.Keys.ToArray()[playerIndex];

                instance.playerControllers[_controller].Add(pawn);
                instance.playerControllers.Keys.ToList()[playerIndex].Possess(pawn);
                return true;
            }

            return false;
        }

        public static bool Disown(Pawn pawn)
        {
            foreach(PlayerController _playerController in instance.playerControllers.Keys)
            {
                Pawn _pawn = instance.playerControllers[_playerController].Find(_item => _item == pawn);
                if(_pawn != null)
                {
                    _playerController.Disown(pawn);
                    return true;
                }
            }

            return false;
        }

        private static void InitializePlayerControllers()
        {
            if (instance.initialized) return;

            DisownAllPawns();
            instance.playerControllers.Clear();

            if(instance.playerControls == null)
            {
                instance.playerControls = (InputActionAsset)Resources.Load(Constants.INPUT_ACTION_ASSET);
                if(instance.playerControls == null)
                {
                    Debug.LogError("Could not load the player controls asset");
                }
            }

            for(int _i = 0; _i < instance.maxPlayerCount; _i++)
            {
                PlayerController _playerController = new PlayerController(playerControls: instance.playerControls);
                _playerController.Initialize();

                instance.playerControllers.Add(_playerController, new List<Pawn>());
            }

            instance.EnableInput();
            instance.initialized = true;
        }

        private static void DisownAllPawns()
        {
            foreach(PlayerController _playerController in instance.playerControllers.Keys)
            {
                _playerController.DisownAll();
            }
        }

        private void Update()
        {
            PlayerControllerTick(Time.deltaTime);
        }

        private void PlayerControllerTick(float deltaTime)
        {
            playerControllers.Keys.ToList().ForEach(_playerController => _playerController.Tick(deltaTime));
        }

        private void EnableInput()
        {
            playerControllers.Keys.ToList().ForEach(_playerController => _playerController.Enable());
        }

        private void DisableInput()
        {
            playerControllers.Keys.ToList().ForEach(_playerController => _playerController.Disable());
        }
    }
}
