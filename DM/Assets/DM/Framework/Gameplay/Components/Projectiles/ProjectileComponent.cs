/*
 Author: Aaron Hines
 Edits By: 
 Description: Holds core logic for projectiles
 */
using UnityEngine;
using Sirenix.OdinInspector;

using GameFramework.Actors.Components;
using GameFramework.Utilities.Extensions;

namespace GameFramework.Gameplay.Components.Projectiles
{
    public enum MoveTragectory
    {
        Arc,
        Straight
    }

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class ProjectileComponent : ActorComponent
    {
        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        protected float speed;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        protected float lifeTime;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        protected bool destroyOnHit;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        protected bool autoStart;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        protected LayerMask hitLayers;

        [TabGroup(Tabs.EVENTS)]
        [SerializeField]
        public ProjectileFiredEvent firedEvent = new ProjectileFiredEvent();

        [TabGroup(Tabs.EVENTS)]
        [SerializeField]
        public ProjectileHitEvent hitEvent = new ProjectileHitEvent();

        [TabGroup(Tabs.EVENTS)]
        [SerializeField]
        public ProjectileDestroyedEvent destroyedEvent = new ProjectileDestroyedEvent();

        //privates
        protected bool isInitialized = false;
        protected bool shouldMove = false;
        protected float currentLifeTime;
        protected Vector3 direction;

        // components
        private Rigidbody projectileRigidbody;

        public override void InitializeComponent()
        {
            if(projectileRigidbody == null)
            {
                projectileRigidbody = GetComponentInChildren<Rigidbody>();
                if (projectileRigidbody == null) return;
            }

            isInitialized = true;

            if(autoStart)
            {
                Fire();
            }
        }

        public override void DisableComponent()
        {
            isInitialized = false;
        }

        public void FixedUpdate()
        {
            Move();
        }

        [Button]
        public virtual void Fire()
        {
            shouldMove = true;
            currentLifeTime = 0;
            SetDirection(transform.forward);
        }

        public virtual void Stop()
        {
            shouldMove = false;
        }

        public virtual void Continue()
        {
            shouldMove = true;
        }

        public virtual void Hit(Collision collision)
        {
            if(!shouldMove)
            {
                return;
            }

            foreach (ContactPoint _contactPoint in collision.contacts)
            {
                if (_contactPoint.otherCollider.gameObject.WithInLayerMask(hitLayers))
                {
                    hitEvent?.Invoke(new HitInfo(gameObject, _contactPoint.otherCollider.gameObject, _contactPoint.point, transform.forward));
                }
            }

            if(destroyOnHit)
            {
                Destroy();
            }
        }

        public virtual void Destroy()
        {
            destroyedEvent?.Invoke(this);
            owner.QueueForDespawn();
        }

        public virtual void  Move()
        {
            if(shouldMove)
            {
                currentLifeTime += Time.deltaTime;
                if(currentLifeTime >= lifeTime)
                {
                    Destroy();
                }

                projectileRigidbody.velocity = transform.forward * speed;
            }
            else
            {
                projectileRigidbody.velocity = Vector3.zero;
                projectileRigidbody.angularVelocity = Vector3.zero;
            }
        }

        public void SetDirection(Vector3 direction)
        {
            this.direction = direction;
        }

        void OnCollisionEnter(Collision collision)
        {
            Hit(collision);
        }
    }
}
