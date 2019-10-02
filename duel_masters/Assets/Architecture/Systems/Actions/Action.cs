using DM.Systems.Cards;
using DM.Systems.Gameplay.Locations;
using DM.Systems.Players;

namespace DM.Systems.Actions
{ 
    public class Action
    {
        #region TAPPING
        public static void TapAll( CardCollection collection )
        {
            foreach ( Card _card in collection.cards )
            {
                TapCard( _card );
            }
        }

        public static void UntapAll( CardCollection collection )
        {
            foreach ( Card _card in collection.cards )
            {
                UntapCard( _card );
            }
        }

        public static void TapCard( Card card )
        {
            if ( !card.tapped )
            {
                card.SetTap( true );
                DuelManager.instance.cardTappedEvent?.Invoke( card );
            }
        }

        public static void UntapCard( Card card )
        {
            if ( card.tapped )
            {
                card.SetTap( false );
                DuelManager.instance.cardUntappedEvent?.Invoke( card );
            }
        }
        #endregion

        #region DECK
        public static void Shuffle( CardCollection collection )
        {
            collection.Shuffle();
        }

        public static void Draw( PlayerComponent targetPlayer, int amountToDraw = 1 )
        {
            for ( int _i = 0; _i < amountToDraw; _i++ )
            {
                if ( targetPlayer.deck.cards.Count > 0 )
                {
                    Card _targetCard = targetPlayer.deck.cards[0];
                    targetPlayer.deck.Transfer( _targetCard, targetPlayer.hand );
                    _targetCard.UpdateCardLocation( CardLocation.Hand );

                    DuelManager.instance.cardDrawnEvent.Invoke( targetPlayer, _targetCard );
                }
            }
        }
        #endregion

        #region SHIELDS
        private static void AddToShields( PlayerComponent targetPlayer, CardCollection collection, Card card )
        {
            if ( card != null )
            {
                collection.Transfer( card, targetPlayer.sheildZone );
                card.UpdateCardLocation( CardLocation.ShieldZone );

                DuelManager.instance.shieldAddedEvent.Invoke( targetPlayer, card );
            }
        }

        public static void AddToShieldsFromTopOfDeck( PlayerComponent targetPlayer, int amount = 1 )
        {
            for ( int _i = 0; _i < amount; _i++ )
            {
                if ( targetPlayer.deck.Count > 0 )
                {
                    Card _card = targetPlayer.deck.cards[0];
                    AddToShields( targetPlayer, targetPlayer.deck, _card );
                }
            }
        }

        public static void AddToShieldsFromHand( PlayerComponent targetPlayer, Card card )
        {
            AddToShields( targetPlayer, targetPlayer.hand, card );
        }

        public static void AddToShieldsFromBattleZone( PlayerComponent targetPlayer, Card card )
        {
            AddToShields( targetPlayer, targetPlayer.battleZone, card );
        }

        public static void AddToShieldsFromGraveyard( PlayerComponent targetPlayer, Card card )
        {
            AddToShields( targetPlayer, targetPlayer.graveyard, card );
        }

        public static void AddToShieldsFromManaZone( PlayerComponent targetPlayer, Card card )
        {
            AddToShields( targetPlayer, targetPlayer.manaZone, card );
        }

        public static void BreakShield( PlayerComponent targetPlayer, Card card )
        {
            targetPlayer.sheildZone.Transfer( card, targetPlayer.hand );
            card.UpdateCardLocation( CardLocation.Hand );
            DuelManager.instance.shieldAddedEvent.Invoke( targetPlayer, card );
        }

        public static void BreakRandom( PlayerComponent targetPlayer, int amount = 1 )
        {
            for ( int _i = 0; _i < amount; _i++ )
            {
                if ( targetPlayer.sheildZone.Count > 0 )
                {
                    Card _random = targetPlayer.sheildZone.cards[UnityEngine.Random.Range( 0, targetPlayer.sheildZone.Count )];
                    BreakShield( targetPlayer, _random );
                }
            }
        }

        public static void BreakFirst( PlayerComponent targetPlayer, int amount = 1 )
        {
            for ( int _i = 0; _i < amount; _i++ )
            {
                if ( targetPlayer.sheildZone.Count > 0 )
                {
                    Card _card = targetPlayer.sheildZone.cards[0];
                    BreakShield( targetPlayer, _card );
                }
            }
        }
        #endregion

        #region MANA
        private static void AddToMana( PlayerComponent targetPlayer, Card card, CardCollection collection )
        {
            if ( card != null )
            {
                collection.Transfer( card, targetPlayer.manaZone );
                card.UpdateCardLocation( CardLocation.ManaZone );
                DuelManager.instance.manaAddedEvent.Invoke( targetPlayer, card );
            }
        }

        public static void AddToManaFromHand( PlayerComponent targetPlayer, Card card )
        {
            AddToMana( targetPlayer, card, targetPlayer.hand );
        }

        public static void AddToManaFromDeck( PlayerComponent targetPlayer, Card card )
        {
            AddToMana( targetPlayer, card, targetPlayer.deck );
        }

        public static void AddToManaFromBattleZone( PlayerComponent targetPlayer, Card card )
        {
            AddToMana( targetPlayer, card, targetPlayer.battleZone );
        }

        public static void AddToManaFromGraveyard( PlayerComponent targetPlayer, Card card )
        {
            AddToMana( targetPlayer, card, targetPlayer.graveyard );
        }

        public static void AddToManaFromShields( PlayerComponent targetPlayer, Card card )
        {
            AddToMana( targetPlayer, card, targetPlayer.sheildZone );
        }
        #endregion

        #region CASTING/SUMMON
        #endregion

        #region BATTLE
        #endregion
    }
}
