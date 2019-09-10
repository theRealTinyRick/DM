/*
 Author: Aaron Hines
 Edits By: 
 Description: Spawns actors - you should make subclasses for this for special spawning.
 */
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Sirenix.OdinInspector;

using GameFramework.Actors;

namespace GameFramework.Gameplay.Components.Spawning
{
    [System.Serializable]
    public class SpawnComponentSpawnedEvent :  UnityEvent<Actor>
    {
    }

    public class SpawnComponent : SerializedMonoBehaviour
    {
        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        protected Identity actorToSpawn;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        protected Transform spawnLocation;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        protected bool parentToSpawnLocation;

        protected List<Actor> spawnedActors = new List<Actor>();

        public virtual void Spawn()
        {
            if(CanSpawn())
            {
                ActorManager.SpawnActor(actorToSpawn, spawnLocation.position, spawnLocation.rotation, parent: parentToSpawnLocation ? spawnLocation : null);
            }
        }

        public virtual void SpawnAtLocation(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            if(actorToSpawn != null)
            {
                ActorManager.SpawnActor(actorToSpawn, position, rotation, parent);
            }
        }

        public virtual void SpawnAtLocation(Vector3 position, Vector3 lookDirection, Transform parent = null)
        {
            if(actorToSpawn != null)
            {
               ActorManager.SpawnActor(actorToSpawn, position, Quaternion.LookRotation(lookDirection), parent);
            }
        }

        public virtual void SpawnAtHitInfo(HitInfo info)
        {
            if (actorToSpawn != null)
            {
                ActorManager.SpawnActor(actorToSpawn, info.hitPoint, Quaternion.LookRotation(info.hitNormal));
            }
        }

        protected virtual bool CanSpawn()
        {
            return actorToSpawn != null && spawnLocation != null;
        }
    }
}

