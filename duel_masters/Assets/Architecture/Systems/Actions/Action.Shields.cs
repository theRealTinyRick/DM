/*
 Author: Aaron Hines
 Description: Core actions to be carried out in the game - shields
*/

using UnityEngine;

using DM.Systems.Players;
using DM.Systems.GameEvents;
using DM.Systems.Cards;
using DM.Systems.Gameplay.Locations;

namespace DM.Systems.Actions
{
    public partial class Action
    {
        #region Adding
        private static void AddToShields( DuelistComponent targetPlayer, CardCollection collection, Card card )
        {
            if ( card != null )
            {
                collection.Transfer( card, targetPlayer.sheildZone );
                card.UpdateCardLocation( CardLocation.ShieldZone );

                DuelManager.instance.shieldAddedEvent.Invoke( targetPlayer, card );
            }
        }

        public static void AddToShieldsFromTopOfDeck( DuelistComponent targetPlayer, int amount = 1 )
        {
            for(int _i = 0; _i < amount; _i++ )
            {
                if(targetPlayer.deck.Count > 0)
                {
                    Card _card = targetPlayer.deck.cards[0];
                    AddToShields( targetPlayer, targetPlayer.deck, _card );
                }
            }
        }

        public static void AddToShieldsFromHand( DuelistComponent targetPlayer, Card card )
        {
            AddToShields( targetPlayer, targetPlayer.hand, card);
        }

        public static void AddToShieldsFromBattleZone( DuelistComponent targetPlayer, Card card )
        {
            AddToShields( targetPlayer, targetPlayer.battleZone, card);
        }


        public static void AddToShieldsFromGraveyard( DuelistComponent targetPlayer, Card card )
        {
            AddToShields( targetPlayer, targetPlayer.graveyard, card);
        }

        public static void AddToShieldsFromManaZone( DuelistComponent targetPlayer, Card card )
        {
            AddToShields( targetPlayer, targetPlayer.manaZone, card);
        }
        #endregion

        #region Breaking

        public static void BreakShield(DuelistComponent targetPlayer, Card card)
        {
            targetPlayer.sheildZone.Transfer( card, targetPlayer.hand );
            card.UpdateCardLocation( CardLocation.Hand );
            DuelManager.instance.shieldAddedEvent.Invoke( targetPlayer, card );
        }

        public static void BreakRandom(DuelistComponent targetPlayer, int amount = 1)
        {
            for(int _i = 0; _i < amount; _i++ )
            {
                if(targetPlayer.sheildZone.Count > 0)
                {
                    Card _random = targetPlayer.sheildZone.cards[Random.Range( 0, targetPlayer.sheildZone.Count )];
                    BreakShield( targetPlayer, _random );
                }
            }
        }

        public static void BreakFirst(DuelistComponent targetPlayer, int amount = 1 )
        {
            for(int _i = 0; _i < amount; _i++ )
            {
                if(targetPlayer.sheildZone.Count > 0)
                {
                    Card _card = targetPlayer.sheildZone.cards[0];
                    BreakShield( targetPlayer, _card );
                }
            }
        }

        #endregion

    }
}
