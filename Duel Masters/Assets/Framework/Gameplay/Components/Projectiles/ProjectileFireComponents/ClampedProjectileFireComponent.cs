/*
 Author: Aaron Hines
 Edits By: 
 Description: Fires projectiles
 */
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameFramework.Gameplay.Components.Projectiles
{
    public class ClampedProjectileFireComponent : ProjectileFireComponent
    {
        enum ClampStrategy
        {
            WaitForDestruction,
            DestroyPrevious
        }

        [Space]
        [Space]
        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        ClampStrategy clampStrategy;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        [ShowIf("WaitForDestruction")]
        protected int maxNumberOfSpawnedProjectiles;

        protected override bool CanFire()
        {
            bool _canFire = WaitForDestruction() ? spawnedProjectiles.Count < maxNumberOfSpawnedProjectiles : true;

            return base.CanFire() && _canFire;
        }

        public override void Fire()
        {
            if(CanFire())
            {
                for(int _i = 0; _i < spawnedProjectiles.Count; _i ++)
                {
                    spawnedProjectiles[_i].Destroy();
                }

                spawnedProjectiles.Clear();
            }

            base.Fire();
        }

        private bool WaitForDestruction()
        {
            return clampStrategy == ClampStrategy.WaitForDestruction;
        }

        private bool DestroyPrevious()
        {
            return clampStrategy == ClampStrategy.DestroyPrevious;
        }
    }
}
