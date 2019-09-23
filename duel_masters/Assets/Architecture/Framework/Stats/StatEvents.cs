using UnityEngine.Events;

namespace GameFramework.Stats
{
    [System.Serializable]
    public class StatChangedEvent : UnityEvent<Stat, float>
    {
    }

    [System.Serializable]
    public class StatReachedMinEvent : UnityEvent<Stat>
    {
    }

    [System.Serializable]
    public class StatReachedMaxEvent : UnityEvent<Stat>
    {
    }
}
