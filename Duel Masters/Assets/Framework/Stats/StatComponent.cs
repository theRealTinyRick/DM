/*
 Author: Aaron Hines
 Edits By: 
 Description:
 */
using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using Sirenix.OdinInspector;

using GameFramework.Actors.Components;

namespace GameFramework.Stats
{
    [System.Serializable]
    public class StatSlot
    {
        public float minAmount;
        public float maxAmount;
        public float currentAmount;
        
        public List<StatModifier> modifiers = new List<StatModifier>();
    }

    public class StatComponent : ActorComponent
    {
        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        private Dictionary<Stat, StatSlot> _stats = new Dictionary<Stat, StatSlot>();
        public Dictionary<Stat, StatSlot> stats
        {
            get
            {
                return _stats;
            }
            private set
            {
                _stats = value;
            }
        }

        public List<StatSlot> slots
        {
            get
            {
                return stats.Values.ToList();
            }
        }

        [TabGroup(Tabs.EVENTS)]
        [SerializeField]
        public StatChangedEvent statChangedEvent = new StatChangedEvent();

        [TabGroup(Tabs.EVENTS)]
        [SerializeField]
        public StatReachedMinEvent statReachedMinEvent = new StatReachedMinEvent();

        [TabGroup(Tabs.EVENTS)]
        [SerializeField]
        public StatReachedMaxEvent statReachedMaxEvent = new StatReachedMaxEvent();

        public void Update()
        {
            TickModifiers();
        }

        // satisfying the abstract class
        public override void InitializeComponent(){}
        public override void DisableComponent() {}

        private void TickModifiers()
        {
            foreach (var _pair in stats)
            {
                List<StatModifier> _modsToRemove = new List<StatModifier>();
                foreach (StatModifier _mod in _pair.Value.modifiers)
                {
                    _mod.currentTime += Time.deltaTime;
                    if (!_mod.indefinite && _mod.currentTime >= _mod.duration)
                    {
                        _modsToRemove.Add(_mod);
                    }
                }

                foreach (StatModifier _mod in _modsToRemove)
                {
                    _pair.Value.modifiers.Remove(_mod);
                }
            }
        }

        public float GetStat(Stat stat)
        {
            if (stats.ContainsKey(stat))
            {
                return stats[stat].currentAmount;
            }

            return 0;
        }

        public bool TryGetStat(Stat stat, out float value)
        {
            if (stats.ContainsKey(stat))
            {
                value = stats[stat].currentAmount;
                return true;
            }

            value = 0;
            return false;
        }

        public bool Add(Stat stat, float amount)
        {
            if(stats.ContainsKey(stat))
            {
                stats[stat].currentAmount += amount;
                if(stats[stat].currentAmount >= stats[stat].maxAmount)
                {
                    stats[stat].currentAmount = stats[stat].maxAmount;
                    statReachedMaxEvent?.Invoke(stat);
                }

                statChangedEvent?.Invoke(stat, stats[stat].currentAmount);

                return true;
            }
            return false; 
        }

        public bool Subtract(Stat stat, float amount)
        {
            if (stats.ContainsKey(stat))
            {
                stats[stat].currentAmount -= amount;
                if (stats[stat].currentAmount <= stats[stat].minAmount)
                {
                    stats[stat].currentAmount = stats[stat].minAmount;
                    statReachedMinEvent?.Invoke(stat);
                }

                statChangedEvent?.Invoke(stat, stats[stat].currentAmount);

                return true;
            }
            return false;
        }

        public bool SetStat(Stat stat, float amount)
        {
            if(stats.ContainsKey(stat))
            {
                if(stats[stat].currentAmount != amount)
                {
                    stats[stat].currentAmount = amount;

                    if (stats[stat].currentAmount <= stats[stat].minAmount)
                    {
                        stats[stat].currentAmount = stats[stat].minAmount;
                        statReachedMinEvent?.Invoke(stat);
                    }

                    if (stats[stat].currentAmount >= stats[stat].maxAmount)
                    {
                        stats[stat].currentAmount = stats[stat].maxAmount;
                        statReachedMaxEvent?.Invoke(stat);
                    }

                    statChangedEvent?.Invoke(stat, stats[stat].currentAmount);
                    return true;
                }
            }
            return false;
        }

        public bool SetStatToMax(Stat stat)
        {
            if (stats.ContainsKey(stat))
            {
                if (stats[stat].currentAmount != stats[stat].maxAmount)
                {
                    stats[stat].currentAmount = stats[stat].maxAmount;

                    statReachedMaxEvent?.Invoke(stat);
                    statChangedEvent?.Invoke(stat, stats[stat].currentAmount);
                    return true;
                }
            }
            return false;
        }

        public bool SetStatToMin(Stat stat)
        {
            if (stats.ContainsKey(stat))
            {
                if (stats[stat].currentAmount != stats[stat].minAmount)
                {
                    stats[stat].currentAmount = stats[stat].minAmount;

                    statReachedMinEvent?.Invoke(stat);
                    statChangedEvent?.Invoke(stat, stats[stat].currentAmount);
                    return true;
                }
            }
            return false;
        }
    }
}
