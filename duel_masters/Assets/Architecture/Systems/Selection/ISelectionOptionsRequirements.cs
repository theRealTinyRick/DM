using System.Collections.Generic;
using DuelMasters.Systems.Cards;

namespace DuelMasters.Systems.Selection
{
    public interface ISelectionRequirements
    {
        bool Meets(List<Card> selection);
    }
}
