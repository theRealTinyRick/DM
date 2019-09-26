/*
 Author: Aaron Hines
 Description: Core actions to be carried out in the game - drawing
*/
using DM.Systems.Players;
using DM.Systems.Cards;
using DM.Systems.Gameplay.Locations;

namespace DM.Systems.Actions
{
    public partial class Action
    {
        public static void Draw(DuelistComponent targetPlayer, int amountToDraw = 1)
        {
            for( int _i = 0; _i < amountToDraw; _i++ )
            {
                if(targetPlayer.deck.cards.Count > 0)
                {
                    Card _targetCard = targetPlayer.deck.cards[0];
                    targetPlayer.deck.Transfer( _targetCard, targetPlayer.hand );
                    _targetCard.UpdateCardLocation( CardLocation.Hand );

                    DuelManager.instance.cardDrawnEvent.Invoke( targetPlayer, _targetCard );
                }
            }
        }
    }
}
