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
    [System.Serializable]
    public class SelectionFinishedEvent : UnityEngine.Events.UnityEvent<List<Card>>
    { }

    public class SelectionManager : Singleton_SerializedMonobehaviour<SelectionManager>
    {
        public SelectionFinishedEvent selectionFinishedEvent = new SelectionFinishedEvent();

        public bool selectionHasFinished = false;
        private bool inSelection = false;

        private bool cancelledSelection;
        private bool confirmedSelection;

        private List<Card> currentSelection = new List<Card>();

        public void StartSelection( CardCollection collection, List<ISelectionFilter> filters = null, List<ISelectionRequirements> reqs = null, bool usepopup = false )
        {   
            if( !inSelection )
            {
                inSelection = true;
                selectionHasFinished = false;

                List<Card> _cards = new List<Card>( collection.cards );
                _cards = RunThroughFilters( _cards, filters );

                if(_cards.Count > 0)
                {
                    StartCoroutine( SelectionRoutine( _cards, usepopup ) );
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

        private IEnumerator SelectionRoutine( List<Card> cards, bool usepopup = false )
        {
            yield return new WaitForSeconds( 1 );
            FinishSelection();
        }

        private void FinishSelection()
        {
            selectionHasFinished = true;
            inSelection = false;
            selectionFinishedEvent.Invoke( currentSelection );

            currentSelection = null;
        }
    }
}
