/*
 Author: Aaron Hines
 Description: Identitfier for a creature race
*/
using UnityEngine;
using Sirenix.OdinInspector;

namespace DM.Systems.Cards
{
    [CreateAssetMenu(fileName = "New Creature Race", menuName = Constants.CREATE_NEW_CREATURE_RACE, order = 1)]
    public class Race : SerializedScriptableObject
    {
    }
}
