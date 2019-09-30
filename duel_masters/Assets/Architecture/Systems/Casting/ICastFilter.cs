using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Systems.Casting
{
    public interface ICastFilter
    {
        Guid owningCardId { get; set; }
    }
}
