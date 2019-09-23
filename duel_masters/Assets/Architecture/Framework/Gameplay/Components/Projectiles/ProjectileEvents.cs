/*
 Author: Aaron Hines
 Edits By: 
 Description: Holds general events for projectiles
 */
using UnityEngine.Events;

namespace GameFramework.Gameplay.Components.Projectiles
{
    [System.Serializable]
    public class ProjectileFiredEvent : UnityEvent<ProjectileComponent>
    {
    }

    [System.Serializable]
    public class ProjectileHitEvent : UnityEvent<HitInfo>
    {
    }

    [System.Serializable]
    public class ProjectileDestroyedEvent : UnityEvent<ProjectileComponent>
    {
    }
}
