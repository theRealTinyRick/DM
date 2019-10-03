
using DM.Systems.Cards;
using System.Collections.Generic;

namespace DM.Systems.Selection
{
    public interface ISelectionFilter
    {
        List<Card> Filter( List<Card> cards );
    }
}
