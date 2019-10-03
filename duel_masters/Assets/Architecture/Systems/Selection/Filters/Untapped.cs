using System.Linq;
using System.Collections.Generic;
using DM.Systems.Cards;

namespace DM.Systems.Selection
{
    public class UntappedSelectionFilter : ISelectionFilter
    {
        public void Filter( List<Card> cards )
        {
            cards = cards.FindAll( _card => !_card.tapped ).ToList(); 
        }
    }
}
