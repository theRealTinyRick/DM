﻿using System;
using System.Collections.Generic;

using UnityEngine;
using Sirenix.OdinInspector;

using DuelMasters.Systems.Cards;
using DuelMasters.Systems.Players;
using DuelMasters.Systems.Gameplay;

namespace DuelMasters.Systems.Selection
{
    public class SelectionFinishedEvent : UnityEngine.Events.UnityEvent<List<Card>> { }
    public class UseSelectionPopupEvent: UnityEngine.Events.UnityEvent<List<Card>> { }
    public class SelectionChangedEvent : UnityEngine.Events.UnityEvent<List<Card>> { }

    public class SelectionManager : Singleton_SerializedMonobehaviour<SelectionManager>
    {
        [HideInInspector]
        public SelectionFinishedEvent selectionFinishedEvent = new SelectionFinishedEvent();

        [HideInInspector]
        public UseSelectionPopupEvent selectionPopupEvent = new UseSelectionPopupEvent();

        [HideInInspector]
        public SelectionChangedEvent selectionChangedEvent = new SelectionChangedEvent();

        private bool selectionHasFinished = false;
        private bool inSelection = false;

        private bool confirmedSelection;
        private bool cancelledSelection;

        [SerializeField]
        private List<Card> currentPossibleSelection = new List<Card>();

        [SerializeField]
        private List<Card> _currentSelection = new List<Card>();
        public List<Card> currentSelection
        {
            get
            {
                if(_currentSelection == null)
                {
                    _currentSelection = new List<Card>();
                }

                return _currentSelection;
            }
            private set
            {
                _currentSelection = value;
            }
        }

        private List<ISelectionRequirements> reqs = new List<ISelectionRequirements>();

        private Action<List<Card>> currentCallback;

        PlayerComponent player;
        CardManipulatorComponent cardManipulatorComponent;

        public void StartSelection( Action<List<Card>> callback, List<CardCollection> collectionsToSelect, List<ISelectionFilter> filters = null, List<ISelectionRequirements> reqs = null, bool usepopup = false )
        {   
            if( !inSelection )
            {
                inSelection = true;
                selectionHasFinished = false;

                List<Card> _cards = new List<Card>( );
                foreach(CardCollection _cardCollection in collectionsToSelect)
                {
                    _cards.AddRange( _cardCollection.cards );
                }

                _cards = RunThroughFilters( _cards, filters );
                if(_cards.Count > 0)
                {
                    this.reqs = reqs;

                    if(usepopup)
                    {
                        selectionPopupEvent.Invoke(_cards);
                    }

                    currentPossibleSelection = _cards;
                    currentCallback = callback;
                    inSelection = true;

                    player = DuelManager.instance.localPlayer;
                    cardManipulatorComponent = player.GetComponentInChildren<CardManipulatorComponent>();
                    cardManipulatorComponent.cardClickedEvent.AddListener(OnCardClicked);
                    cardManipulatorComponent.confirmPressedEvent.AddListener(ConfirmSelection);
                    cardManipulatorComponent.cancelledPressedEvent.AddListener(CancelSelection);
                }
                else
                {
                    FinishSelection();
                }
            }
        }

        private List<Card> RunThroughFilters( List<Card> cards, List<ISelectionFilter> filters )
        {
            if( filters != null )
            {
                foreach ( ISelectionFilter _filter in filters )
                {
                    cards = _filter.Filter( cards );
                }
            }
            return cards;
        }

        private void FinishSelection(bool cancelled = false)
        {
            currentCallback( currentSelection );
            cardManipulatorComponent.cardClickedEvent.RemoveListener( OnCardClicked );
            cardManipulatorComponent.confirmPressedEvent.RemoveListener( ConfirmSelection );
            cardManipulatorComponent.cancelledPressedEvent.RemoveListener( CancelSelection );

            cardManipulatorComponent = null;
            player = null;

            selectionHasFinished = true;
            inSelection = false;

            if(!cancelled)
            {
                selectionFinishedEvent.Invoke( currentSelection );
            }

            currentSelection = null;
        }

        private bool MeetsReqs()
        {
            if(currentSelection == null || currentSelection.Count == 0)
            {
                return false;
            }

            foreach(ISelectionRequirements _req in reqs)
            {
                if(!_req.Meets(currentSelection))
                {
                    return false;
                }
            }

            return true;
        }

        public void  ConfirmSelection()
        {
            if( !inSelection )
            {
                return;
            }

            if (MeetsReqs())
            {
                FinishSelection();
                return;
            }

            return;
        }
        
        public void  CancelSelection()
        {
            if ( !inSelection )
            {
                return;
            }

            FinishSelection( true );
        }

        public void AddToSelection( Card card )
        {
            if ( !inSelection )
            {
                return;
            }

            if ( !currentSelection.Contains( card ) && currentPossibleSelection.Contains( card ) )
            {
                currentSelection.Add( card );
                selectionChangedEvent.Invoke( currentSelection );
            }
        }

        public void RemoveFromSelection( Card card )
        {
            if ( !inSelection )
            {
                return;
            }

            if (currentSelection.Contains(card) && currentPossibleSelection.Contains( card ) )
            {
                currentSelection.Remove( card );
                selectionChangedEvent.Invoke( currentSelection );
            }
        }

        private void OnCardClicked(Card card)
        {
            if(currentSelection.Contains(card))
            {
                RemoveFromSelection( card );
            }
            else
            {
                AddToSelection(card);
            }
        }
    }
}
