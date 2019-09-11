/*
 Author: Aaron Hines
 Edits By:
 Desctiption: Holds the events common to an actor,
 becuase things can get wierd
 */
using System;
using UnityEngine.Events;

namespace GameFramework.Actors
{
    [Serializable]
    public class OnEnableEvent : UnityEvent { }

    [Serializable]
    public class OnDisableEvent : UnityEvent { }

    [Serializable]
    public class SpawnedEvent : UnityEvent { }

    [Serializable]
    public class DespawnedEvent : UnityEvent { }

    [Serializable]
    public class OnActorSpawnedEvent : UnityEvent<Actor> { }

    [Serializable]
    public class OnActorDespawnedEvent : UnityEvent<Actor> { }

    [Serializable]
    public class OnActorRegistered : UnityEvent<Actor> { }

    [Serializable]
    public class OnActorDeregistered : UnityEvent<Actor> { }
}

