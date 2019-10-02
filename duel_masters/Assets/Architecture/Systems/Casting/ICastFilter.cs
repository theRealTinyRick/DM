using DM.Systems.Cards;

namespace DM.Systems.Casting
{
    public interface ICastFilter
    {
        bool CanPlayCard( Card card );
    }
}
