/*
 Author: Aaron Hines
 Description: Trigger fired when the owning card is summoned
*/
using UnityEngine;
using DuelMasters.Systems.Cards;
using DuelMasters.Systems.Players;

namespace DuelMasters.Systems.Effects.Triggers
{
    [System.Serializable]
    public class OnSummonTrigger : IMechanicTrigger
    {
        public Card card
        {
            get;
            private set;
        }

        public Effect effect
        {
            get;
            private set;
        }

        public IMechanicTrigger Copy()
        {
            return new OnSummonTrigger();
        }

        public void Initialize(Card card, Effect effect)
        {

            this.card = card;
            this.effect = effect;
            DuelManager.instance.creatureSummonedEvent.AddListener( OnTrigger );
        }

        public void DeInitialize()
        {
            DuelManager.instance.creatureSummonedEvent.RemoveListener( OnTrigger );
        }

        public void OnTrigger(PlayerComponent player, Card card)
        {
            if(this.card == card)
            {
                Trigger(card);
            }
        }

        public void Trigger(object arg)
        {
            effect.Trigger(this);
        }
    }
}
