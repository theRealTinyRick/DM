﻿/*
 Author: Aaron Hines
 Description: The physical representation of  a card in scene
*/
using UnityEngine;
using Sirenix.OdinInspector;

using GameFramework.Actors.Components;

namespace DM.Systems.Cards
{
    public class CardActorComponent : ActorComponent
    {
        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public Card card;

        public override void DisableComponent()
        {
            //throw new System.NotImplementedException();
        }

        public override void InitializeComponent()
        {
            //throw new System.NotImplementedException();
        }
    }
}
