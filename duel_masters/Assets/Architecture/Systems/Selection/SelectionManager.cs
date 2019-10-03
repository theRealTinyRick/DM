using DM.Systems.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Systems.Selection
{
    public class SelectionManager : Singleton_SerializedMonobehaviour<SelectionManager>
    {
        public void StartSelection(CardCollection collection, List<ISelectionFilter> filters, List<ISelectionRequirements>reqs, bool usepopup = false)
        {
            //List<Card> _card = new List<Card>() { collection.cards };

            if (usepopup)
            {
                // TODO: add logic for the popup bpx
            }



        }

        //private List<Card> RunThroughFilters( CardCollection collection, List<ISelectionFilter> filters )
        //{

        //}
    }
}
