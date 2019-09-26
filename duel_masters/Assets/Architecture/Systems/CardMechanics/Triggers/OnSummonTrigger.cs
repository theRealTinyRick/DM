/*
 Author: Aaron Hines
 Description: Trigger fired when the owning card is summoned
*/
using DM.Systems.Cards;
using DM.Systems.GameEvents;
using DM.Systems.Players;

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
            //DuelManager.instance.creatureSummonedEvent.AddListener( OnTrigger );
        }

        public void DeInitialize()
        {
           // DuelManager.instance.creatureSummonedEvent.RemoveListener( OnTrigger );
        }

        public void OnTrigger(DuelistComponent player, Card card)
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
