/*
 Author: Aaron Hines
 Description: The physical representation of  a card in scene
*/
using UnityEngine;
using Sirenix.OdinInspector;

using GameFramework.Actors.Components;

namespace DM.Systems.Cards
{
    public class CardComponent : ActorComponent
    {
        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        public Card card;

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        public MeshRenderer cardFace;

        public override void InitializeComponent() { }
        public override void DisableComponent() { }

        public void AssignCard( Card card )
        {
            this.card = card;

            cardFace.material = card.cardMaterial;
            gameObject.name = string.Format( "Card: {0}", card.cardName );
        }
    }
}

