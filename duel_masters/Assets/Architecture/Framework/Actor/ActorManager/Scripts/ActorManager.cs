/*
 Author: Aaron Hines
 Edits By: 
 Description: Manages the spawning and despawing of actors as well as provides methods for finding actors in scene
 */
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Photon.Pun;

namespace GameFramework.Actors
{
    /// <summary>
    /// A class designed to house despawned actors
    /// </summary>
    [System.Serializable]
    public class SpawnPool
    {
        /// <summary>
        /// The transform in the scene that all despawned actors will live under
        /// </summary>
        public Transform parent;
        public List<Actor> actors = new List<Actor>();

        public SpawnPool(Transform parent, List<Actor> actors)
        {
            this.parent = parent;
            this.actors = actors;
        }
    }

    public class ActorManager : Singleton_SerializedMonobehaviour<ActorManager>
    {
        /// <summary>
        /// all actors present in scene
        /// </summary>
        [SerializeField]
        [InlineEditor]
        private List<Actor> _actors = new List<Actor>();

        /// <summary>
        /// Actors in scene sorted for easy access
        /// </summary>
        private static Dictionary<Identity, List<Actor>> sortedActors = new Dictionary<Identity, List<Actor>>();

        /// <summary>
        /// A pool used for optimzing and tracking actors spawning
        /// </summary>
        [SerializeField]
        private Dictionary<Identity, SpawnPool> spawnPool = new Dictionary<Identity, SpawnPool>();

        #region Events
        public static OnActorSpawnedEvent onActorSpawnedEvent = new OnActorSpawnedEvent();
        public static OnActorDespawnedEvent onActorDespawnedEvent = new OnActorDespawnedEvent();

        public static OnActorRegistered onActorRegistered = new OnActorRegistered();
        public static OnActorDeregistered onActorDeregistered = new OnActorDeregistered();
        #endregion

        /// <summary>
        /// Registers actors in scene
        /// </summary>
        /// <param name="actor"></param>
        public static void RegisterActor(Actor actor)
        {
            if (!instance._actors.Contains(actor))
            {
                instance._actors.Add(actor);
            }

            if (actor.identity != null)
            {
                if (!sortedActors.ContainsKey(actor.identity))
                {
                    sortedActors.Add(actor.identity, new List<Actor>());
                }

                if (!sortedActors[actor.identity].Contains(actor))
                {
                    sortedActors[actor.identity].Add(actor);
                }
            }
            onActorRegistered.Invoke(actor);
        }

        /// <summary>
        /// Deregesters an actor in scene
        /// </summary>
        /// <param name="actor"></param>
        public static void DeregisterActor(Actor actor)
        {
            if (instance._actors.Contains(actor))
            {
                instance._actors.Remove(actor);
            }

            if (actor.identity != null && sortedActors.ContainsKey(actor.identity))
            {
                sortedActors[actor.identity].Remove(actor);
            }

            onActorDeregistered.Invoke(actor);
        }

        /// <summary>
        /// Use an identity to properly spawn an actor
        /// </summary>
        /// <param name="identity">identity of the actor you want to spawn</param>
        /// <param name="parent">The parent you want to set the actor to</param>
        /// <returns></returns>
        public static GameObject SpawnActor(Identity identity, Transform parent = null)
        {
            return SpawnActor(identity, parent != null ? parent.position : Vector3.zero, parent != null ? parent.rotation : Quaternion.identity, parent);
        }

        /// <summary>
        /// Used to spawn an actor at a location in space
        /// </summary>
        /// <param name="identity">identity of the actor you want to spawn</param>
        /// <param name="position">the position you want to spawn in</param>
        /// <param name="rotation">the rotation you want to spawn with</param>
        /// <param name="parent">the parent you want the object to have</param>
        /// <returns></returns>
        public static GameObject SpawnActor(Identity identity, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            if (identity.prefab == null)
            {
                Debug.LogError("The prefab for " + identity.identityName + " is null");
                return null;
            }
            Actor _prefab = GetPrefab(identity);

            if (_prefab == null || identity.isReplicated)
            {
                return SpawnNewActor(identity.prefab, position, rotation, parent, identity.isReplicated);
            }

            return RespawnActor(_prefab, position, rotation, parent).gameObject;
        }

        /// <summary>
        /// If a spawn pool is not found, this method will spawn a new object
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private static GameObject SpawnNewActor(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null, bool replicate = false)
        {
            GameObject _spawnedActor = null;
            _spawnedActor = Instantiate(prefab, position, rotation, parent);

            _spawnedActor.transform.SetParent(parent);
            FinishSpawn(_spawnedActor.GetComponent<Actor>());

            return _spawnedActor;
        }

        /// <summary>
        /// If a spawn pool id found, this method will use it for the spawn
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private static Actor RespawnActor(Actor prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            prefab.gameObject.SetActive(true);
            prefab.transform.position = position;
            prefab.transform.rotation = rotation;
            prefab.transform.SetParent(parent);

            FinishSpawn(prefab);

            instance.spawnPool[prefab.identity].actors.Remove(prefab);

            return prefab;
        }

        /// <summary>
        /// Finds a gameobject to spawn in the spawn pool
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        private static Actor GetPrefab(Identity identity)
        {
            Actor _prefab = null;

            if (instance.spawnPool.TryGetValue(identity, out SpawnPool _pool))
            {
                if (_pool.actors.Count > 0)
                {
                    _prefab = _pool.actors[0];
                }
            }

            return _prefab;
        }

        /// <summary>
        /// Gets the finished actor and fires any events and methods nessicary
        /// </summary>
        /// <param name="actor"></param>
        private static void FinishSpawn(Actor actor)
        {
            if (actor != null)
            {
                actor?.OnSpawned();
                onActorSpawnedEvent?.Invoke(actor);
            }
        }

        /// <summary>
        /// Despawns an actor and adds it to the spawn pool
        /// </summary>
        /// <param name="actor"></param>
        public static void Despawn(Actor actor)
        {
            if (actor.identity != null)
            {
                if (instance.spawnPool.ContainsKey(actor.identity))
                {
                    Transform _parent = instance.spawnPool[actor.identity].parent;
                    Despawn(actor, _parent);
                }
                else
                {
                    Transform _parent = new GameObject().transform;
                    _parent.name = actor.identity.identityName + " pool";
                    _parent.SetParent(instance.transform);

                    instance.spawnPool.Add(actor.identity, new SpawnPool(_parent, new List<Actor>()));

                    Despawn(actor, _parent);
                }
            }
            else
            {
                actor.OnDespawned();
                Destroy(actor.gameObject);
                Debug.LogError("You are trying to despawn an actor with no identity, this will result in the actor being destroyed. This could have unintended behaviour");
            }
        }

        /// <summary>
        /// Finalizes te Despawn and sets it up to be respawned
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="parent"></param>
        private static void Despawn(Actor actor, Transform parent)
        {
            actor.OnDespawned();
            onActorDespawnedEvent.Invoke(actor);

            actor.gameObject.SetActive(false);

            actor.transform.SetParent(parent);
            actor.transform.position = parent.position;
            actor.transform.rotation = parent.rotation;

            Rigidbody[] _rigidbodies = actor.GetComponentsInChildren<Rigidbody>();

            if(_rigidbodies.Length > 0)
            {
                foreach(Rigidbody _rb in _rigidbodies)
                {
                    _rb.velocity = Vector3.zero;
                    _rb.angularVelocity = Vector3.zero;
                }
            }

            instance.spawnPool[actor.identity].actors.Add(actor);
        }

        /// <summary>
        /// Gets the first actor that we can find with the given identity
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static Actor GetActor(Identity identity)
        {
            foreach(Identity _key in sortedActors.Keys)
            {
                if(_key == identity || _key.IsDescendantOf(identity))
                {
                    if (sortedActors[_key].Count > 0)
                    {
                        return sortedActors[_key][0];
                    }
                }
            }

            return null;
        }

        /// <summary>
        ///     Gets all the actors in scene with the given identity
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static List<Actor> GetActors(Identity identity)
        {
            foreach (Identity _key in sortedActors.Keys)
            {
                if (_key == identity || _key.IsDescendantOf(identity))
                {
                    return sortedActors[_key];
                }
            }

            return null;
        }

        /// <summary>
        /// Used for finding a specific type of Actor (child class) with the given identity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static T GetActor<T>(Identity identity) where T : Actor
        {
            foreach (Identity _key in sortedActors.Keys)
            {
                if (_key == identity || _key.IsDescendantOf(identity))
                {
                    List<Actor> _actors = sortedActors[_key];

                    foreach (Actor _actor in _actors)
                    {
                        T _castedActor = _actor as T;
                        if (_castedActor != null)
                        {
                            return _castedActor;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Used for finding a list of specific actors (child class) with the given identity
        /// Could be used for finding specific characters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static List<T> GetActors<T>(Identity identity) where T : Actor
        {
            List<T> _actors = new List<T>();

            foreach (Identity _key in sortedActors.Keys)
            {
                if (_key == identity || _key.IsDescendantOf(identity))
                {
                    foreach (Actor _actor in _actors)
                    {
                        T _castedActor = _actor as T;
                        if (_castedActor != null)
                        {
                            _actors.Add(_castedActor);
                        }
                    }
                }
            }

            return _actors;
        }

        /// <summary>
        /// Gets all actors of a specific type (Actor, Pawn, Character)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetActors<T>() where T : Actor
        {
            List<T> _returnedActors = new List<T>();

            foreach (Actor _actor in instance._actors)
            {
                T _castedActor = _actor as T;
                if (_castedActor != null)
                {
                    _returnedActors.Add(_castedActor);
                }
            }

            return _returnedActors;
        }
    }
}

