using System.Linq;
using System.Collections.Generic;
using DM.Systems.Cards;

namespace DM.Systems.Selection
{
    public class UntappedSelectionFilter : ISelectionFilter
    {
        public List<Card> Filter( List<Card> cards )
        {
            return cards.FindAll( _card => !_card.tapped ).ToList(); 
        }
    }
}
