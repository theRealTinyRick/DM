using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

using GameFramework;
using GameFramework.Actors;

public class DespawnComponent :  SerializedMonoBehaviour
{
    [TabGroup(Tabs.PROPERTIES)]
    [SerializeField]
    private Identity identity;

    public virtual void DespawnAll()
    {
        List<Actor> _actors = ActorManager.GetActors(identity);
        if(_actors != null)
        {
            foreach (Actor _actor in _actors)
            {
                _actor.QueueForDespawn();
            }
        }
    }
}
