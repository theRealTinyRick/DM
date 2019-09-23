/*
Author: Aaron Hines
Edits By:
Desciption: An identifier used to keep track of Actors
*/
using UnityEngine;

namespace GameFramework
{
    [CreateAssetMenu(fileName = "New Identity", menuName = Constants.CREATE_IDENTITY, order = 1)]
    public class Identity : ScriptableObject
    {
        /// <summary>
        /// The name of the identity
        /// </summary>
        [SerializeField]
        private string _identityName;
        public string identityName
        {
            get => _identityName;
        }

        /// <summary>
        /// The perfab associtate with it
        /// </summary>
        [SerializeField]
        private GameObject _prefab;
        public GameObject prefab
        {
            get => _prefab;
        }

        /// <summary>
        /// The parent identity
        /// </summary>
        [SerializeField]
        private Identity _parent;
        public Identity parent
        {
            get => _parent;
        }

        [SerializeField]
        private bool _isReplicated;
        public bool isReplicated
        {
            get => _isReplicated;
        }

        /// <summary>
        /// Determines if the child identity is a descendant of the parent
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        /// <returns></returns>
        public bool IsDescendantOf(Identity parent)
        {
            Identity _child = this;

            while(_child.parent != null)
            {
                if(_child.parent == parent)
                {
                    return true;
                }
                else if(_child.parent != null)
                {
                    _child = _child.parent;
                    continue;
                }
            }
            return false;
        }

        public bool IsAncestorOf(Identity child)
        {
            while (child.parent != null)
            {
                if (child.parent == this)
                {
                    return true;
                }
                else if (child.parent != null)
                {
                    child = child.parent;
                    continue;
                }
            }
            return false;
        }
    }
}

