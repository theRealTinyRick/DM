
using DM.Systems.Cards;
using System.Collections.Generic;

namespace DM.Systems.Selection
{
    public interface ISelectionFilter
    {
        void Filter( List<Card> cards );
    }
}
