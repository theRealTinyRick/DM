/*
 Author: Aaron Hines
 Description: Core actions to be carried out in the game - drawing
*/
using DM.Systems.Players;
using DM.Systems.GameEvents;
using DM.Systems.Cards;

namespace DM.Systems.Actions
{
    public partial class Actions
    {
        public static void Draw(Player targetPlayer, int amountToDraw = 1)
        {
            for( int _i = 0; _i < amountToDraw; _i++ )
            {
                if(targetPlayer.deck.cards.Count > 0)
                {
                    Card _targetCard = targetPlayer.deck.cards[0];
                    targetPlayer.deck.Transfer(_targetCard, targetPlayer.hand);

                    CardDrawnEvent _event = new CardDrawnEvent( targetPlayer );
                    _event.Invoke( _targetCard );
                }
            }
        }
    }
}
