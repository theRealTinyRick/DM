/*
 Author: Aaron Hines
 Description: Core actions to be carried out in the game
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DM.Systems.Players;
using DM.Systems.GameEvents;

namespace DM.Systems.Actions
{
    public partial class Actions
    {
        public void Draw(Player targetPlayer, int amountToDraw = 1)
        {
            for( int _i = 0; _i < amountToDraw; _i++ )
            {
                targetPlayer.deck.Transfer();
            }
        }
    }
}
