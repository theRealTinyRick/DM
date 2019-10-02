using DM.Systems.Cards;

namespace DM.Systems.Casting
{
    public interface ICastCondition
    {
        bool CanPlayCard( Card card );
    }
}
