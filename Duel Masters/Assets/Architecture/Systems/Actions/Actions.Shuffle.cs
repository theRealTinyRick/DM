/*
 Author: Aaron Hines
 Description: Core actions to be carried out in the game - drawing
*/
using DM.Systems.Players;
using DM.Systems.GameEvents;
using DM.Systems.Cards;

namespace DM.Systems.Actions
{
    public partial class Action
    {
        public static void Shuffle(CardCollection collection)
        {
            collection.Shuffle();   
        }
    }
}
