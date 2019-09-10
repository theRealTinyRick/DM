/*
 Author: Aaron Hines
 Edits By:
 Desctiption: Holds the events common to an actor
 */
using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using Sirenix.OdinInspector;

using GameFramework.Actors.Components;

namespace GameFramework.Actors
{
    /// <summary>
    /// The base of every interactable object in the scene
    /// </summary>
    public class Actor : SerializedMonoBehaviour
    {
        #region Properties
        [TabGroup(Tabs.ACTOR, Order = 0)]
        [SerializeField]
        private Guid _actorId;
        /// <summary>
        /// A completely unique identifier for actors
        /// </summary>
        public Guid actorId { get => _actorId; }

        [TabGroup(Tabs.ACTOR, Order = 0)]
        [SerializeField]
        private Identity _identity;
        /// <summary>
        /// A general identifier for this actor.
        /// </summary>
        public Identity identity { get => _identity; }

        /// <summary>
        /// Determines that this actor will not be destroyed when a scene is loaded
        /// </summary>
        [TabGroup(Tabs.ACTOR, Order = 0)]
        [SerializeField]
        protected bool dontDestroyOnLoad;

        private bool _spawned = true;
        /// <summary>
        /// An easy check to determine if the actor is currently spawned
        /// </summary>
        public bool spawned { get => _spawned; }

        public bool despawningNextFrame
        {
            get;
            private set;
        }

        private bool _registered = false;
        /// <summary>
        /// determines that this actor is regestered with the actor manager
        /// </summary>
        public bool registered { get => _registered; }
        #endregion

        /// <summary>
        /// A list of all the actor components attached to this actor
        /// </summary>
        private List<ActorComponent> actorComponents = new List<ActorComponent>();

        #region Events
        /// <summary>
        /// An event fired when this actor is enabled in scene, used for easy hook up to components
        /// </summary>  
        [TabGroup(Tabs.EVENTS)]
        [SerializeField]
        public OnEnableEvent enableEvent = new OnEnableEvent();

        /// </summary>
        /// An event fired when this actor is enabled in scene, used for easy hook up to components
        /// </summary>
        [TabGroup(Tabs.EVENTS)]
        [SerializeField]
        public OnDisableEvent disableEvent = new OnDisableEvent();

        /// <summary>
        /// An event fired when this actor is spawned
        /// </summary>
        [TabGroup(Tabs.EVENTS)]
        [SerializeField]
        public SpawnedEvent spawnedEvent = new SpawnedEvent();

        /// <summary>
        /// An event fired when this actor is despawned
        /// </summary>
        [TabGroup(Tabs.EVENTS)]
        [SerializeField]
        public DespawnedEvent despawnedEvent = new DespawnedEvent();
        #endregion

        protected virtual void OnEnable()
        {
            despawningNextFrame = false;

            if(dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }

            SetupActorComponents();
            RegisterActor();
            enableEvent.Invoke();
        }

        protected virtual void OnDisable()
        {
            DisableActorComponents();
            DeregisterActor();
            disableEvent.Invoke();
        }

        /// <summary>
        /// Gets all Actor Components under this object and initalizes them
        /// </summary>
        public void SetupActorComponents()
        {
            actorComponents = GetComponentsInChildren<ActorComponent>().ToList();
            foreach (ActorComponent _component in actorComponents)
            {
                _component.SetOwner(this);
                _component.InitializeComponent();
            }
        }

        /// <summary>
        /// Disabled all actor components
        /// </summary>
        public void DisableActorComponents()
        {
            foreach(ActorComponent _component in actorComponents)
            {
                _component.DisableComponent();
            }
        }

        /// <summary>
        /// Gets a cached component with the given typ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetActorComponent <T> () where T : ActorComponent
        {
            foreach(ActorComponent _component in actorComponents)
            {
                if ((_component as T) != null) return _component as T;
            }

            return null;            
        }

        /// <summary>
        /// Gets cached components of the given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetActorComponents <T> () where T : ActorComponent
        {
            List<T> _result = new List<T>();
            foreach (ActorComponent _component in actorComponents)
            {
                if ((_component as T) != null) _result.Add(_component as T);
            }

            return _result;
        }

        /// <summary>
        /// Determines if a specific actor component is on this actor
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public bool HasActorComponent(ActorComponent component)
        {
            return actorComponents.Contains(component);
        }

        public bool HasActorComponent<T>() where T: ActorComponent
        {
            return GetActorComponent<T>() != null;
        }

        /// <summary>
        /// Registers this actor with the ActorManageer
        /// </summary>
        protected virtual void RegisterActor()
        {
            ActorManager.RegisterActor(this);
            _registered = true;
        }

        /// <summary>
        /// Deregisters this acto with the ActorManagr
        /// </summary>
        /// <param name="status"></param>
        protected virtual void DeregisterActor()
        {
            ActorManager.DeregisterActor(this);
            _registered = false;
        }

        /// <summary>
        /// Spawns a copy of this actor
        /// NOTE: the copy will have the default position, rotation and no parent
        /// </summary>
        public void SpawnCopy()
        {
            ActorManager.SpawnActor(identity);
        }
        
        /// <summary>
        /// Despawns this actor
        /// </summary>
        public void Despawn()
        {
            ActorManager.Despawn(this);
        }
        
        public void QueueForDespawn()
        {
            despawningNextFrame = true;
        }

        /// <summary>
        /// Used by the Actor Manager to notify other systems that this actor has been spawned
        /// </summary>
        public void OnSpawned()
        {
            _spawned = true;
            spawnedEvent.Invoke();
        }

        /// <summary>
        /// Used by the Actor Manager to notify other systems that this actor has been despawned
        /// </summary>
        public void OnDespawned()
        {
            _spawned = false;
            spawnedEvent.Invoke();
        }

        #region Updates
        private void Update()
        {
            if(despawningNextFrame)
            {
                Despawn();
            }

            Tick(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            FixedTick();
        }

        private void LateUpdate()
        {
            LateTick();
        }

        /// <summary>
        /// Used for combined logic in the Update Function
        /// </summary>
        /// <param name="deltaTime"></param>
        protected virtual void Tick(float deltaTime) { }

        /// <summary>
        /// Used for combined logic in the FixedUpdate function
        /// </summary>
        protected virtual void FixedTick() { }

        /// <summary>
        /// Used for combined logic in the LateUpdate function
        /// </summary>
        protected virtual void LateTick() { }
        #endregion
    }
}

