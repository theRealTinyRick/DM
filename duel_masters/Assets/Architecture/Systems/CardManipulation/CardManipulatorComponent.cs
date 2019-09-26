/*
 Author: Aaron Hines
 Description: represents the playerComponent in the scene. Handles spawning and despawning cards. Some coroutines may be called here too
*/
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

using GameFramework.Phases;
using GameFramework.Actors.Components;
using GameFramework.Utilities.Extensions;

using DM.Systems.Cards;
using DM.Systems.Players;
using DM.Systems.Duel.Phases;
using DM.Systems.Actions;

namespace DM.Systems.CardManipulation
{
    [RequireComponent(typeof(DuelistComponent))]
    public class CardManipulatorComponent : ActorComponent
    {
        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private PhaseManager phaseManager;

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private float cardVerticalOffset;

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private float cardSpeed;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        private LayerMask cardLayerMask;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        private LayerMask tableLayerMask;

        [SerializeField]
        [TabGroup( Tabs.PROPERTIES )]
        private Transform playerPos;

        [SerializeField]
        [TabGroup( Tabs.PROPERTIES )]
        private float dragDistance;

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private PhaseIdentifier manaPhase;

        [SerializeField]
        [TabGroup( Tabs.PROPERTIES )]
        private PhaseIdentifier mainPhase;

        private CardComponent currentManipulatedCard;
        private DuelistComponent playerComponent;
        private new Camera camera;

        private bool clicking = false;

        public override void InitializeComponent()
        {
            playerComponent = owner.GetActorComponent<DuelistComponent>();
            camera = owner.GetComponentInChildren<Camera>();

            DuelManager.instance.gameStartedEvent.AddListener( OnGameStarted );
            DuelManager.instance.gameEndedEvent.AddListener( OnGameStarted );
        }

        public override void DisableComponent()
        {
            DuelManager.instance.gameStartedEvent.RemoveListener( OnGameStarted );
            DuelManager.instance.gameEndedEvent.RemoveListener( OnGameStarted );
        }

        public void OnGameStarted()
        {
        }

        public void OnInputDown()
        {
            if(!CanDragCards())
            {
                return;
            }

            RaycastHit _hit;
            Ray ray = camera.ScreenPointToRay( Input.mousePosition );

            if(Physics.Raycast(ray, out _hit, 100))
            {
                if(_hit.transform.gameObject.WithInLayerMask(cardLayerMask))
                {
                    CardComponent _card = _hit.transform.gameObject.GetComponentInChildren<CardComponent>();
                    if( _card != null && playerComponent.hand.Contains(_card.card) )
                    {
                        currentManipulatedCard = _card;
                    }
                }
            }

            clicking = true;
        }

        public void OnInputHeld()
        {
            if ( !CanDragCards() )
            {
                ReleaseCard();
                return;
            }

            if (currentManipulatedCard != null)
            {
                RaycastHit _hit;
                Ray ray = camera.ScreenPointToRay( Input.mousePosition );

                if ( Physics.Raycast( ray, out _hit, 100, tableLayerMask ) )
                {
                    currentManipulatedCard.externallyManipulated = true;

                    currentManipulatedCard.transform.position = Vector3.Lerp( currentManipulatedCard.transform.position, _hit.point + (Vector3.up * cardVerticalOffset), cardSpeed );
                    currentManipulatedCard.transform.rotation = Quaternion.Lerp( currentManipulatedCard.transform.rotation, Quaternion.LookRotation( -_hit.normal ), cardSpeed);
                }
                else
                {
                    currentManipulatedCard.externallyManipulated = false;
                }
            }
        }

        public void OnInputUp()
        {
            // check distance from current location and phase - call an action

            if(currentManipulatedCard != null)
            {
                if(Vector3.Distance(currentManipulatedCard.transform.position, playerPos.position) > dragDistance)
                {
                    if( phaseManager.currentPhase.identifier == manaPhase )
                    {
                        ManaPhase _manaPhase = phaseManager.currentPhase as ManaPhase;
                        if( _manaPhase != null )
                        {
                            if(!_manaPhase.manaAdded)
                            {
                                Action.AddToManaFromHand( playerComponent, currentManipulatedCard.card );
                            }
                        }
                    }

                    if( phaseManager.currentPhase.identifier == mainPhase )
                    {
                        // TODO: make main phase 

                        //ManaPhase _manaPhase = phaseManager.currentPhase as ManaPhase;
                        //if(_manaPhase == null)
                        //{

                        //}
                    }
                }
            }

            ReleaseCard();
        }

        public void ReleaseCard()
        {
            if ( currentManipulatedCard != null )
            {
                currentManipulatedCard.externallyManipulated = false;
            }

            currentManipulatedCard = null;
            clicking = false;
        }

        private bool CanDragCards()
        {
            if(phaseManager == null)
            {
                Debug.LogError( "Phase manager is null", gameObject );
                return false;
            }

            if(phaseManager.currentPhase == null)
            {
                Debug.LogError( "current Phase is null", gameObject );
                return false;
            }

            if( phaseManager.currentPhase.identifier == manaPhase || phaseManager.currentPhase.identifier == mainPhase )
            {
                return true;
            }

            return false;
        }
    }
}
