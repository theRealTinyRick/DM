using UnityEngine;

using Sirenix.OdinInspector;

using GameFramework.Phases;
using GameFramework.Actors.Components;
using GameFramework.Utilities.Extensions;

using DM.Systems.Cards;
using DM.Systems.Players;
using DM.Systems.Duel.Phases;
using DM.Systems.Actions;
using DM.Systems.Turns;
using DM.Systems.Gameplay.Locations;
using DM.Systems.Casting;
using DM.Systems.Gameplay;

namespace DM.Gameplay.UI
{
    [RequireComponent( typeof( PlayerComponent ) )]
    [RequireComponent( typeof( CardManipulatorComponent ) )]
    public class CardDetailsComponent : ActorComponent
    {
        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        private GameObject detailsPrefab;

        private PlayerComponent player;
        private CardManipulatorComponent cardManipulatorComponent;
        private new Camera camera;

        private CardComponent currentlyViewedCard;

        public override void InitializeComponent()
        {
            player = owner.GetActorComponent<PlayerComponent>();
            cardManipulatorComponent = owner.GetActorComponent<CardManipulatorComponent>();
            camera = owner.GetComponentInChildren<Camera>();

            cardManipulatorComponent.cardHoverEvent.AddListener( OnCardHover );
        }

        public override void DisableComponent()
        {

        }

        private void OnCardHover(Card card)
        {
            if ( card.cardStatus != CardStatus.Hidden && !( card.cardStatus == CardStatus.Private && card.owner != player ) )
            {

            }
        }
    }
}
