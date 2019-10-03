using DM.Systems.Cards;
using System.Collections.Generic;

namespace DM.Systems.Selection
{
    public class MeetsManaRequirements : ISelectionRequirements
    {
        public Card card;

        public bool Meets( List<Card> selection )
        {
            foreach(Civ _civ in card.civilization.civs)
            {
                // TODO: account for colorless mana later

                // if the civ cannot be found return false
                if(selection.Find(_card => _card.civilization.civs.Contains(_civ)) == null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
