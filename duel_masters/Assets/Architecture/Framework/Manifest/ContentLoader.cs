/*
 Author: Aaron Hines
 Edits By: 
 Description: simple component for level loading
 */
using UnityEngine;

namespace GameFramework.Manifest
{
    public class ContentLoader : MonoBehaviour
    {
        [SerializeField]
        private ContentCollection contentToLoad;

        public void LoadContent()
        {
            if(contentToLoad != null)
            {
                contentToLoad.Load();
            }
        }
    }
}
