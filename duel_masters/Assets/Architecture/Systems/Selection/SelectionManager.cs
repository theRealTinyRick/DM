using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using DM.Systems.Cards;

namespace DM.Systems.Selection
{
    public class SelectionManager : Singleton_SerializedMonobehaviour<SelectionManager>
    {
        private List<Card> currentSelectionPool = new List<Card>();
        private List<Card> currentSelection = new List<Card>();

        private bool inSelection;
        private bool cancelledSelection;
        private bool confirmedSelection;

        public void StartSelection( CardCollection collection, List<ISelectionFilter> filters, List<ISelectionRequirements> reqs, bool usepopup = false )
        {
            List<Card> _cards = new List<Card>( collection.cards );
            _cards = RunThroughFilters( _cards, filters );

            if(_cards.Count > 0)
            {
                StartCoroutine( SelectionRoutine( _cards, usepopup ) );
            }
        }

        private List<Card> RunThroughFilters( List<Card> cards, List<ISelectionFilter> filters )
        {
            foreach ( ISelectionFilter _filter in filters )
            {
                cards = _filter.Filter( cards );
            }

            return cards;
        }

        private IEnumerator SelectionRoutine( List<Card> cards, bool usepopup = false )
        {
            if ( usepopup )
            {
                // TODO: add logic for the popup bpx
            }

            while(true)
            {
                if(confirmedSelection)
                {
                }

                if(cancelledSelection)
                {

                }

                yield return new WaitForEndOfFrame();
            }
        }

        public void AddToSelection(Card card)
        {
            if(currentSelectionPool.Contains(card))
            {
                currentSelection.Add( card );
            }
        }

        public void RemoveFromSelection(Card card)
        {
            if(currentSelectionPool.Contains(card) && currentSelection.Contains(card))
            {
                currentSelection.Remove( card );
            }
        }

        public void ConfirmSelection()
        {

        }

        public void CancelSelection()
        {

        }
    }
}
