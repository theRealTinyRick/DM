/*
 Author: Aaron Hines
 Edits By: 
 Description: General Inpout events that will be used by the PlayerInputComponent
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
        private List<ContentCollection> contents = new List<ContentCollection>();


#if UNITY_EDITOR
        [Button]
        public void Activate()
        {
            Populate();
        }

        private void Populate()
        {
            List<EditorBuildSettingsScene> _editorBuildSettingsScenes = new List<EditorBuildSettingsScene>();

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
