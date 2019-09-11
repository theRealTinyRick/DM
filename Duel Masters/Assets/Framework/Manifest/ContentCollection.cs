/*
 Author: Aaron Hines
 Edits By: 
 Description: Essentially defines a levels and it's dependent scenes
 */
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace GameFramework.Manifest
{
    [CreateAssetMenu(fileName = "New Content Collection", menuName = Constants.CREATE_CONTENT_COLLECTION, order = 1)]
    public class ContentCollection : SerializedScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField]
        private List<SceneAsset> _scenes = new List<SceneAsset>();
        public List<SceneAsset> scenes
        {
            get
            {
                return _scenes;
            }
        }

        public void LoadSceneNames()
        {
            sceneNames = new List<string>();
            foreach(SceneAsset _sceneAsset in scenes)
            {
                sceneNames.Add(_sceneAsset.name);
                Debug.Log(_sceneAsset.name);
            }
        }
#endif

        public List<string> sceneNames
        {
            get;
            private set;
        }
    }
}