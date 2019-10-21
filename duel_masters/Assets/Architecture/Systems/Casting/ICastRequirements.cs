using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuelMasters.Systems.Cards;

namespace DuelMasters.Systems.Casting
{
    public interface ICastRequirements
    {
        Card card { get; set; }
        bool running { get; set; }
        bool failed { get; set; }

        void Start();
        void Stop();
    }
}