using System;
using Sirenix.OdinInspector;

namespace GameFramework.Stats
{
    [Serializable]
    public class StatModifier
    {
        #region Constructors

        public StatModifier(Stat stat, Identity source, bool canStack = false, float percentage = 0.0f, float duration = 0.0f)
        {
            this.stat = stat;
            this.source = source;

            this.canStack = canStack;

            this.percentage = percentage;
            this.usePercentage = true;

            this.duration = duration;
        }

        public StatModifier(Stat stat, Identity source, bool canStack = false, float percentage = 0.0f)
        {
            this.stat = stat;
            this.source = source;

            this.canStack = canStack;

            this.percentage = percentage;
            this.usePercentage = true;

            this.indefinite = true;
        }

        public StatModifier(Stat stat, Identity source, float duration = 0.0f, float amount = 0.0f, bool canStack = false)
        {
            this.stat = stat;
            this.source = source;

            this.duration = duration;
            this.amount = amount;
            this.canStack = canStack;

            this.usePercentage = false;
        }

        public StatModifier(Stat stat, Identity source, float amount = 0.0f, bool canStack = false)
        {
            this.stat = stat;
            this.source = source;

            this.amount = amount;
            this.canStack = canStack;

            this.usePercentage = false;
        }

        public StatModifier(StatModifier mod)
        {
            this.stat = mod.stat;
            this.source = mod.source;
            this.canStack = mod.canStack;
            this.usePercentage = mod.usePercentage;
            this.indefinite = mod.indefinite;
            this.percentage = mod.percentage;
            this.amount = mod.amount;
            this.duration = mod.duration;
        }

        #endregion

        public Stat stat;
        public Identity source;

        public bool canStack = false;
        public bool usePercentage = false;
        public bool indefinite = false;

        [ShowIf("usePercentage")]
        public float percentage = 0.0f;

        [ShowIf("NotUsePercentage")]
        public float amount = 0.0f;

        [ShowIf("NotIndefinite")]
        public float duration = 0.0f;

        public float currentTime = 0.0f;

        private bool NotUsePercentage()
        {
            return !usePercentage;
        }

        private bool NotIndefinite()
        {
            return !indefinite;
        }
    }
}
