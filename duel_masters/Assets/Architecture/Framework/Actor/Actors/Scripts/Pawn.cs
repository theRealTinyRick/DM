/*
 Author: Aaron Hines
 Edits By:
 Desctiption: an actor that has the ability to reieve input and be possesed by a player controller
 becuase things can get wierd
 */
using UnityEngine;
using Sirenix.OdinInspector;

using GameFramework.Actors.Components;

namespace GameFramework.Actors
{
    [RequireComponent(typeof(PlayerInputComponent))]
    public class Pawn : Actor
    {
        [TabGroup(Tabs.PAWN, Order = 1)]
        [SerializeField]
        private PawnControlType pawnControlType;

        [TabGroup(Tabs.PAWN, Order = 1)]
        [SerializeField]
        private bool autoPossess = false;

        [TabGroup(Tabs.PAWN, Order = 1)]
        [SerializeField]
        private int playerIndex;

        [TabGroup("Input Configuration")]
        [SerializeField]
        private bool orientInput;

        [TabGroup("Input Configuration")]
        [ShowIf("orientInput")]
        [SerializeField]
        Transform transformToOrientTo;

        public bool pawnHasController
        {
            get;
            private set;
        }

        public PlayerController owner
        {
            get;
            private set;
        }

        public PlayerInputComponent playerInputComponent
        {
            get;
            private set;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            Photon.Pun.PhotonView photonView = GetComponent<Photon.Pun.PhotonView>();
            if(photonView != null)
            {
                if(!photonView.IsMine)
                {
                    pawnControlType = PawnControlType.Network;
                }
            }

            if(playerInputComponent == null)
            {
                playerInputComponent = GetActorComponent<PlayerInputComponent>();

                if(orientInput && transformToOrientTo != null)
                {
                    playerInputComponent.OrientLocomotionInput(transformToOrientTo, onlyTrackY: true);
                }
            }
            
            if(pawnControlType == PawnControlType.PlayerController && autoPossess)
            {
                GameManager.Possess(this, playerIndex);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if(autoPossess)
            {
                GameManager.Disown(this);
            }
        }

        /// <summary>
        /// Sets up the pawn to recieve input from a controller/keyboard
        /// </summary>
        /// <param name="index">What player input is it? Player 1? Player 2?</param>
        public void SetupPlayerController(int index)
        {
            if(pawnControlType == PawnControlType.PlayerController)
            {
                GameManager.Disown(this);
                if(GameManager.Possess(this, index))
                {
                    playerIndex = index;
                }
                else
                {
                    Debug.LogWarning("You are trying to set up a player controller on a pawn that is not controlled by Player Controller");
                }
            }
        }

        public void SetControlType(PawnControlType pawnControlType)
        {
            this.pawnControlType = pawnControlType;
        }

        public void SetHasController(bool status, PlayerController owner)
        {
            pawnHasController = status;
            this.owner = owner;
        }
    }
}

