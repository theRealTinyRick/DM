using DuelMasters.Systems.Cards;

namespace DuelMasters.Systems.Casting
{
    public interface ICastCondition
    {
        bool CanPlayCard( Card card );
    }
}
