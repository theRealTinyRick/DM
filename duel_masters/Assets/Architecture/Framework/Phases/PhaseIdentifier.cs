/*
 Author: Aaron Hines
 Description: identifies a game phase for the phase manager
*/
using UnityEngine;

namespace GameFramework.Phases
{
    [CreateAssetMenu(fileName = "New Phase", menuName = Constants.CREATE_PHASE , order = 5)]
    public class PhaseIdentifier : ScriptableObject
    {
    }
}