using UnityEngine;

namespace GameFramework.Manifest
{
    public class Startup : MonoBehaviour
    {
        [SerializeField]
        private Manifest startupManifest;

        private void Start()
        {
            if( startupManifest == null )
            {
                Debug.LogError( "Cannot load manifest. Reason: no manifest was assigned in the startup scene" );
                return;
            }

            if(startupManifest.contents.Count > 0 )
            {
                startupManifest.contents[0].Load();
            }
            else
            {
                Debug.LogError( "CANNOT LOAD CONTENT: no content to load from " + startupManifest.name );
            }
        }
    }
}
