/*
 Author: Aaron Hines
 Description: Trigger fired when the owning card is summoned
*/
using DM.Systems.Cards;
using DM.Systems.GameEvents;

namespace DM.Systems.CardMechanics.Triggers
{
    public class OnSummonTrigger : IMechanicTrigger
    {
        public Card card
        {
            get;
            set;
        }

        public void Initialize(Card card)
        {
            CreatureSummonedEvent.AddGlobalListener(OnTrigger);
        }

        public void DeInitialize()
        {
            CreatureSummonedEvent.RemoveGlobalListener(OnTrigger);
        }

        public void OnTrigger(Card card)
        {
            if(this.card == card)
            {
                Trigger(card);
            }
        }

        public void Trigger(object arg)
        {
            //throw new NotImplementedException();
        }
    }
}
