/*
 Author: Aaron Hines
 Description: Core actions to be carried out in the game - adding to mana
*/
using DM.Systems.Players;
using DM.Systems.Cards;

namespace DM.Systems.Actions
{
    public partial class Action
    {
        private static void AddToMana(Player targetPlayer, Card card, CardCollection collection)
        {
            if( card != null )
            {
                collection.Transfer( card, targetPlayer.manaZone );
                DuelManager.instance.manaAddedEvent.Invoke( targetPlayer, card );
            }
        }

        public static void AddToManaFromHand( Player targetPlayer, Card card)
        {
            AddToMana( targetPlayer, card, targetPlayer.hand );
        }

        public static void AddToManaFromDeck( Player targetPlayer, Card card )
        {
            AddToMana( targetPlayer, card, targetPlayer.deck );
        }

        public static void AddToManaFromBattleZone( Player targetPlayer, Card card )
        {
            AddToMana( targetPlayer, card, targetPlayer.battleZone );
        }

        public static void AddToManaFromGraveyard( Player targetPlayer, Card card )
        {
            AddToMana( targetPlayer, card, targetPlayer.graveyard );
        }

        public static void AddToManaFromShields( Player targetPlayer, Card card )
        {
            AddToMana( targetPlayer, card, targetPlayer.sheildZone );
        }
    }
}
