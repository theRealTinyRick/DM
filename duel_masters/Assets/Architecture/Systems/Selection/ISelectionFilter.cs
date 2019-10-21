
using DuelMasters.Systems.Cards;
using System.Collections.Generic;

namespace DuelMasters.Systems.Selection
{
    public interface ISelectionFilter
    {
        List<Card> Filter( List<Card> cards );
    }
}
