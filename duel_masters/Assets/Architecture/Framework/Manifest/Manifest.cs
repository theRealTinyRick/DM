/*
 Author: Aaron Hines
 Edits By: 
 Description: An object that can load levels in groups and dynamically add themselves to build settings
 */
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using Sirenix.OdinInspector;

namespace GameFramework.Manifest
{
    [CreateAssetMenu(fileName = "New Manifest", menuName = Constants.CREATE_MANIFEST, order = 1)]
    public class Manifest : SerializedScriptableObject
    {
        [SerializeField]
        [InlineEditor]
        private List<ContentCollection> _contents = new List<ContentCollection>();
        public List<ContentCollection> contents
        {
            get => _contents;
        }

#if UNITY_EDITOR
        [SerializeField]
        private SceneAsset _startupScene;
        public SceneAsset startupScene
        {
            get
            {
                if(_startupScene == null)
                {
                    _startupScene = (SceneAsset)Resources.Load( "Startup" );
                }
                return _startupScene;
            }
            private set
            {
                _startupScene = value;
            }
        }
        [Button]
        public void Activate()
        {
            Populate();
        }

        private void Populate()
        {
            if(startupScene == null)
            {
                Debug.LogError( "MANIFEST ACTIVATE FAILED: Startup scene could not be dynamically loaded", this );
            }

            List<EditorBuildSettingsScene> _editorBuildSettingsScenes =
                new List<EditorBuildSettingsScene>() { new EditorBuildSettingsScene( AssetDatabase.GetAssetPath( startupScene ), true ) };

            foreach(var _content in contents)
            {
                foreach(var _scene in _content.scenes)
                {
                   _editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(AssetDatabase.GetAssetPath(_scene), true));
                }

                _content.LoadSceneNames();
            }

            EditorBuildSettings.scenes = _editorBuildSettingsScenes.ToArray();
        }
#endif
    }
}
