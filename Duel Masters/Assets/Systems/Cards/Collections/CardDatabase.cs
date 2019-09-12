/*
 Author: Aaron Hines
 Description: central location for constant strings used in code
*/

using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace DM.Systems.Cards
{
    [CreateAssetMenu(fileName = "New Card Database", menuName = Constants.CREATE_NEW_CARD_DATABASE, order = 1)]
    public class CardDatabase : SerializedScriptableObject
    {
        [SerializeField]
        private Dictionary<CardData, int> _collection = new Dictionary<CardData, int>();
        public Dictionary<CardData, int> collection
        {
            get => _collection;
        }
    }
}
