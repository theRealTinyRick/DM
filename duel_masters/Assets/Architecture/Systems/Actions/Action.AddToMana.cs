/*
 Author: Aaron Hines
 Description: Core actions to be carried out in the game - adding to mana
*/
using DM.Systems.Players;
using DM.Systems.Cards;
using DM.Systems.Gameplay.Locations;

namespace DM.Systems.Actions
{
    public partial class Action
    {
        private static void AddToMana(DuelistComponent targetPlayer, Card card, CardCollection collection)
        {
            if( card != null )
            {
                collection.Transfer( card, targetPlayer.manaZone );
                card.UpdateCardLocation( CardLocation.ManaZone );
                DuelManager.instance.manaAddedEvent.Invoke( targetPlayer, card );
            }
        }

        public static void AddToManaFromHand( DuelistComponent targetPlayer, Card card)
        {
            AddToMana( targetPlayer, card, targetPlayer.hand );
        }

        public static void AddToManaFromDeck( DuelistComponent targetPlayer, Card card )
        {
            AddToMana( targetPlayer, card, targetPlayer.deck );
        }

        public static void AddToManaFromBattleZone( DuelistComponent targetPlayer, Card card )
        {
            AddToMana( targetPlayer, card, targetPlayer.battleZone );
        }

        public static void AddToManaFromGraveyard( DuelistComponent targetPlayer, Card card )
        {
            AddToMana( targetPlayer, card, targetPlayer.graveyard );
        }

        public static void AddToManaFromShields( DuelistComponent targetPlayer, Card card )
        {
            AddToMana( targetPlayer, card, targetPlayer.sheildZone );
        }
    }
}
