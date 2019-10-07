using DM.Systems.Cards;
using System.Collections.Generic;

namespace DM.Systems.Selection
{
    public class MeetsManaRequirements : ISelectionRequirements
    {
        public Card card;

        public MeetsManaRequirements(Card card)
        {
            this.card = card;
        }

        public bool Meets( List<Card> selection )
        {
            foreach(Civ _civ in card.civilization.civs)
            {
                // TODO: account for colorless mana later

                // check the number of cards
                if(selection.Count < card.manaCost)
                {
                    return false;
                }

                if(selection.FindAll(_card => _card.tapped).Count > 0)
                {
                    return false;
                }

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
