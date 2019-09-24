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
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

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
        [SerializeField]
        private List<string> _sceneNames = new List<string>();
        public List<string> sceneNames
        {
            get =>_sceneNames;
            private set =>_sceneNames = value;
        }

        public void Load()
        {
            int _index = 0;
            foreach(string _scene in sceneNames)
            {
                if(_index == 0)
                {
                    SceneManager.LoadScene( _scene );
                    _index++;
                }
                else
                {
                    SceneManager.LoadScene( _scene, LoadSceneMode.Additive );
                }
                //if(PhotonNetwork.IsConnected)
                //{
                //    PhotonNetwork.LoadLevel( _scene ); // this may actuall need to be handled in a custom solution, maybe load levels additively??
                //    // TODO: Make a manifest singleton to load levels additively with photon. This way we can use rpcs
                //}
                //else
                //{
                //}

                Debug.Log( "Loading scene: " + _scene + " from content collection: " + name);
            }
        }
    }
}