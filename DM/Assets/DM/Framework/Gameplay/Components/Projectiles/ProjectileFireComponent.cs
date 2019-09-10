/*
 Author: Aaron Hines
 Edits By: 
 Description: Fires projectiles
 */
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

using GameFramework.Actors;
using GameFramework.Actors.Components;
using GameFramework.State;

namespace GameFramework.Gameplay.Components.Projectiles
{
    public class ProjectileFireComponent : ActorComponent
    {
        [TabGroup(Tabs.PROPERTIES)]
        [ValidateInput("HasProjectileComponent", "Please ensure that you are assigning a gameobject that is an actor, has a valid identity and contains a Projectile Component.")]
        [SerializeField]
        protected Actor projectile;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        protected Transform projectileSpawnTransform;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        [Tooltip("If delay is set to 0, there will be no delay")]
        protected float delay;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        [Tooltip("IIf set to true, the projectile component will not fire till EnableFiring() is called, Used for when aiming or another condition must be met before firing")]
        protected bool disableFiringOnStart;

        [TabGroup(Tabs.EVENTS)]
        [SerializeField]
        public ProjectileHitEvent hitEvent = new ProjectileHitEvent();

        [TabGroup(Tabs.EVENTS)]
        [SerializeField]
        public ProjectileFiredEvent firedEvent = new ProjectileFiredEvent();

        protected List<ProjectileComponent> spawnedProjectiles = new List<ProjectileComponent>();

        public bool firingEnabled
        {
            get;
            protected set;
        }

        public float fireTime
        {
            get;
            protected set;
        }

        public override void InitializeComponent()
        {
            fireTime = Time.time - delay;
            firingEnabled = true;

            if(disableFiringOnStart)
            {
                firingEnabled = false;
            }
        }

        public override void DisableComponent()
        {
            foreach(ProjectileComponent _projectile in spawnedProjectiles)
            {
                _projectile.destroyedEvent.RemoveListener(OnProjectileDestroyed);
            }

            spawnedProjectiles.Clear();
        }

        public virtual void Fire()
        {
            if (CanFire())
            {
                fireTime = Time.time;
                SpawnProjectile();
            }
        }

        protected void SpawnProjectile()
        {
            GameObject _spawnedGameObject = ActorManager.SpawnActor(projectile.identity, projectileSpawnTransform.position, projectileSpawnTransform.rotation);
            ProjectileComponent _newProjectile = _spawnedGameObject.GetComponentInChildren<ProjectileComponent>();
            if (_newProjectile != null)
            {
                spawnedProjectiles.Add(_newProjectile);
                _newProjectile.destroyedEvent.AddListener(OnProjectileDestroyed);
                _newProjectile.hitEvent.AddListener(OnProjectileHit);

                _newProjectile.Fire();

                firedEvent?.Invoke(_newProjectile);
            }
        }

        protected virtual bool CanFire()
        {
            if (projectile == null || projectile.identity == null)
            {
                Debug.LogError("The identity for the projectile is null. Will not fire.", gameObject);
                return false;
            }

            return (Time.time - fireTime >= delay || delay <= 0) && firingEnabled;
        }

        protected virtual void OnProjectileDestroyed(ProjectileComponent projectile)
        {
            if(spawnedProjectiles.Contains(projectile))
            {
                spawnedProjectiles.Remove(projectile);
            }
        }

        protected virtual void OnProjectileHit(HitInfo hitInfo)
        {
            hitEvent?.Invoke(hitInfo);
        }

        private bool HasProjectileComponent(Actor potentialProjectile)
        {
            if (potentialProjectile == null)
            {
                return true;
            }

            return potentialProjectile.GetComponent<ProjectileComponent>() != null  && potentialProjectile.identity != null;
        }

        public void EnableFiring()
        {
            firingEnabled = true;
        }

        public void DisableFiring()
        {
            firingEnabled = false;
        }
    }
}
