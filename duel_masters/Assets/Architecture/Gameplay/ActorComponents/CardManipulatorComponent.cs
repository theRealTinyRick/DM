/*
 Author: Aaron Hines
 Description: represents the playerComponent in the scene. Handles spawning and despawning cards. Some coroutines may be called here too
*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Sirenix.OdinInspector;

using GameFramework.Phases;
using GameFramework.Actors.Components;
using GameFramework.Utilities.Extensions;

using DM.Systems.Cards;
using DM.Systems.Players;
using DM.Systems.Duel.Phases;
using DM.Systems.Actions;
using DM.Systems.Turns;
using DM.Systems.Gameplay.Locations;
using DM.Systems.Casting;

namespace DM.Systems.Gameplay
{
    public class CardInputEvent : UnityEvent<Card> { }
    public class CardInputEventNoArg : UnityEvent { }

    [RequireComponent(typeof(PlayerComponent))]
    public class CardManipulatorComponent : ActorComponent
    {
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

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        public Dictionary<CardLocation, List<Transform>> cardLocations = new Dictionary<CardLocation, List<Transform>>();

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        public Quaternion tapRotation;

        private List<CardComponent> spawnedCards
        {
            get
            {
                return player.spawnedCards;
            }
        }

        private ActionManager actionManager
        {
            get => ActionManager.instance;
        }

        private PhaseManager phaseManager
        {
            get => PhaseManager.instance;
        }

        private TurnManager turnManager
        {
            get => TurnManager.instance;
        }

        private PlayerComponent player;
        private new Camera camera;

        private CardComponent currentManipulatedCard;
        private CardComponent currentHoveringCard;
        private bool clicking = false;

        public CardInputEvent cardHoverEvent = new CardInputEvent();
        public CardInputEvent cardClickedEvent = new CardInputEvent();
        public CardInputEvent cardReleasedEvent = new CardInputEvent();
        public CardInputEventNoArg confirmPressedEvent = new CardInputEventNoArg();
        public CardInputEventNoArg cancelledPressedEvent = new CardInputEventNoArg();

        public override void InitializeComponent()
        {
            player = owner.GetActorComponent<PlayerComponent>();
            camera = owner.GetComponentInChildren<Camera>();
        }

        public override void DisableComponent() { } // satisfying the abstract call

        private void FixedUpdate()
        {
            MouseHover();
            UpdateCardPosition();
        }

        private void Update()
        {
            // TODO: make this use unity's input system, that needs to be finished anyway
            if(Input.GetKeyDown(KeyCode.Return))
            {
                OnConfirmedInput();
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                OnCancelInput();
            }

            if(Input.GetMouseButtonDown(1))
            {
                OnCancelInput();
            }
        }

        /// <summary>
        ///     Updates cards positions according to their CardLocation
        /// </summary>
        private void UpdateCardPosition()
        {
            if ( spawnedCards.Count == 0 )
            {
                return;
            }

            Dictionary<CardLocation, int> locationIndexMap = new Dictionary<CardLocation, int>()
            {
                { CardLocation.Hand, 0 },
                { CardLocation.Deck, 0 },
                { CardLocation.BattleZone, 0 },
                { CardLocation.ShieldZone, 0 },
                { CardLocation.Graveyard, 0 },
                { CardLocation.ManaZone, 0 },
                {CardLocation.Casting, 0 }
            };

            foreach ( CardComponent _card in spawnedCards )
            {
                if ( _card.externallyManipulated )
                {
                    continue;
                }

                int _index = locationIndexMap[_card.card.currentLocation];
                if ( cardLocations[_card.card.currentLocation].Count > _index )
                {
                    _card.transform.position = Vector3.Lerp( _card.transform.position, cardLocations[_card.card.currentLocation][_index].position, cardSpeed );
                    if(_card.card.tapped)
                    {
                        _card.transform.rotation = Quaternion.Lerp( _card.transform.rotation, cardLocations[_card.card.currentLocation][_index].rotation * tapRotation , cardSpeed );
                    }
                    else
                    {
                        _card.transform.rotation = Quaternion.Lerp( _card.transform.rotation, cardLocations[_card.card.currentLocation][_index].rotation, cardSpeed );
                    }

                    locationIndexMap[_card.card.currentLocation]++;
                }
            }
        }

        private void MouseHover()
        {
            RaycastHit _hit;
            Ray ray = camera.ScreenPointToRay( Input.mousePosition );

            if ( Physics.Raycast( ray, out _hit, 100 ) )
            {
                if ( _hit.transform.gameObject.WithInLayerMask( cardLayerMask ) )
                {
                    CardComponent _cardComponent = _hit.transform.gameObject.GetComponentInChildren<CardComponent>();
                    if ( _cardComponent != null)
                    {
                        currentHoveringCard = _cardComponent;
                        cardHoverEvent.Invoke( _cardComponent.card );
                      return;
                    }

                }
            }

            currentHoveringCard = null;
        }

        public void OnInputDown()
        {
            if(!CanDragCards() || turnManager.currentTurnPlayer != player )
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
                    if( _card != null )
                    {
                        cardClickedEvent.Invoke( _card.card );

                        if( player.hand.Contains( _card.card ) )
                        {
                            currentManipulatedCard = _card;
                        }
                    }
                }
            }

            clicking = true;
        }

        public void OnInputHeld()
        {
            if ( !CanDragCards() || turnManager.currentTurnPlayer != player )
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
            if ( !player.isLocal )
            {
                return;
            }

            if (currentManipulatedCard != null)
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
                                actionManager.TriggerAddManaFromHand( player, currentManipulatedCard.card );
                            }
                        }
                    }

                    if( phaseManager.currentPhase.identifier == mainPhase )
                    {
                        MainPhase _mainPhase = phaseManager.currentPhase as MainPhase;
                        if ( _mainPhase != null )
                        {
                            CastManager.instance.Cast( currentManipulatedCard.card );
                        }
                    }
                }
            }

            ReleaseCard();
        }

        public void OnConfirmedInput()
        {
            if(!player.isLocal)
            {
                return;
            }

            confirmPressedEvent.Invoke();
        }
        
        public void OnCancelInput()
        {
            if ( !player.isLocal )
            {
                return;
            }

            cancelledPressedEvent.Invoke();
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
            if(!player.isLocal)
            {
                return false;
            }

            if(phaseManager.currentPhase == null)
            {
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
