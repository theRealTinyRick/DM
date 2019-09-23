/*
 Author: Aaron Hines
 Description: Core actions to be carried out in the game - tapping cards
*/
using DM.Systems.Cards;

namespace DM.Systems.Actions
{
    public partial class Action
    {
        public static void TapAll( CardCollection collection )
        {
            foreach ( Card _card in collection.cards )
            {
                TapCard( _card );
            }
        }

        public static void UntapAll(CardCollection collection)
        {
            foreach(Card _card in collection.cards)
            {
                UntapCard( _card );
            }
        }

        public static void TapCard(Card card)
        {
            if(!card.tapped)
            {
                card.SetTap( true );
                DuelManager.instance.cardTappedEvent?.Invoke( card );
            }
        }

        public static void UntapCard(Card card)
        {
            if(card.tapped)
            {
                card.SetTap( false );
                DuelManager.instance.cardUntappedEvent?.Invoke( card );
            }
        }
    }
}
