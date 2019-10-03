using System.Collections.Generic;
using DM.Systems.Cards;

namespace DM.Systems.Selection
{
    public interface ISelectionRequirements
    {
        bool Meets(List<Card> selection);
    }
}
