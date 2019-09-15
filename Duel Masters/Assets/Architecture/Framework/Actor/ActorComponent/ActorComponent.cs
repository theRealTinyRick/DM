/*
 Author: Aaron Hines
 Edits By:
 Description: A base class for components that will be used by actors
 */
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameFramework.Actors.Components
{
    public abstract class ActorComponent : SerializedMonoBehaviour
    {
        private Actor _owner;
        /// <summary>
        /// The owning actor of this class
        /// </summary>
        public Actor owner { get => _owner; }

        /// <summary>
        /// Used by Actor class to initialize the component
        /// </summary>
        public abstract void InitializeComponent();

        /// <summary>
        /// Used by the actor class to reset any values that may cause problems
        /// </summary>
        public abstract void DisableComponent();
        /// <summary>
        /// Used by the actor to set itself as the owner of this component
        /// </summary>
        /// <param name="owner"></param>
        public virtual void SetOwner(Actor owner)
        {
            if(owner.HasActorComponent(this))
            {
                _owner = owner;
            }
            else
            {
                Debug.LogWarning("You are trying to assign an owner to this component that does not actuall have this component. ", gameObject);
            }
        }
    }
}
