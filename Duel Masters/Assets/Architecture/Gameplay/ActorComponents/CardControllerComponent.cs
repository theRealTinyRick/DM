using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

using GameFramework.Actors.Components;

namespace DM.Systems.Gameplay.Components
{
    public class CardControllerComponent : ActorComponent
    {
        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private LayerMask cardLayer;

        [TabGroup( Tabs.PROPERTIES )]
        [SerializeField]
        private LayerMask boardLayer;

        //[TabGroup(Tabs.PROPERTIES)]
        //[SerializeField]

        private bool holdingInput = false;

        public override void DisableComponent() { }
        public override void InitializeComponent() { }

        public void InputDown()
        {
            holdingInput = true;

        }

        public void InputHeld()
        {
            holdingInput = true;

        }

        public void InputUp()
        {
            holdingInput = false;

        }

        public void FixedUpdate()
        {
            if(holdingInput)
            {
                // move card
            }
        }
    }
}
