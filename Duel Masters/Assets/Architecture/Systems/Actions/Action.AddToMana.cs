/*
 Author: Aaron Hines
 Description: Core actions to be carried out in the game - adding to mana
*/
using DM.Systems.Players;
using DM.Systems.GameEvents;
using DM.Systems.Cards;

namespace DM.Systems.Actions
{
    public partial class Actions
    {
        public static void AddToManaFromHand( Player targetPlayer, Card card)
        {
            if(card != null)
            {
                targetPlayer.hand.Transfer( card, targetPlayer.manaZone );
                ManaAddedEvent.InvokeGlobal( targetPlayer, card );
            }
        }

        public static void AddToManaFromDeck( Player targetPlayer, Card card )
        {
            if ( card != null )
            {

                targetPlayer.deck.Transfer( card, targetPlayer.manaZone );
                ManaAddedEvent.InvokeGlobal( targetPlayer, card );
            }
        }


        public static void AddToManaFromBattleZone( Player targetPlayer, Card card )
        {
            if ( card != null )
            {
                targetPlayer.battleZone.Transfer( card, targetPlayer.manaZone );
                ManaAddedEvent.InvokeGlobal( targetPlayer, card );
            }
        }

        public static void AddToManaFromGraveyard( Player targetPlayer, Card card )
        {
            if ( card != null )
            {

                targetPlayer.graveyard.Transfer( card, targetPlayer.manaZone );
                ManaAddedEvent.InvokeGlobal( targetPlayer, card );
            }
        }

        public static void AddToManaFromShields( Player targetPlayer, Card card )
        {
            if ( card != null )
            {
                targetPlayer.sheildZone.Transfer( card, targetPlayer.manaZone );
                ManaAddedEvent.InvokeGlobal( targetPlayer, card );
            }
        }
    }
}
