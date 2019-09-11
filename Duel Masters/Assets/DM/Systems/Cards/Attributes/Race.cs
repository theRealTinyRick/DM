/*
 Author: Aaron Hines
 Description: Identitfier for a creature race
*/
using UnityEngine;
using Sirenix.OdinInspector;

namespace DM.Systems.Cards
{
    [CreateAssetMenu(fileName = "New Creature Race", menuName = Constants.CreateNewCreatureRace, order = 1)]
    public class Race : SerializedScriptableObject
    {
    }
}
