/*
 Author: Aaron Hines
 Edits By: 
 Description: The main class for all characters in your game
 */
using UnityEngine;
using Sirenix.OdinInspector;

using GameFramework.Stats;

namespace GameFramework.Actors
{
    [RequireComponent(typeof(StatComponent))]
    public class Character : Pawn
    {
        [TabGroup(Tabs.CHARACTER)]
        [SerializeField]
        private string _characterName;
        /// <summary>
        /// The name of the character that could be displayed in the the UI
        /// </summary>
        public string characterName { get => _characterName; private set => _characterName = value; }

        private StatComponent _stats;
        /// <summary>
        /// the characters stats
        /// </summary>
        public StatComponent stats { get => _stats; }


        private void Awake()
        {
            _stats = GetComponentInChildren<StatComponent>();
        }
    }
}
