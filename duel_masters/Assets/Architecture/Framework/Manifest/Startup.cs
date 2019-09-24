using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameFramework.Manifest
{
    public class Startup : MonoBehaviour
    {
        [SerializeField]
        private Manifest startupManifest;

        private void Start()
        {
            if ( startupManifest == null )
            {
                Debug.LogError( "Cannot load manifest. Reason: no manifest was assigned in the startup scene" );
                return;
            }

            if ( startupManifest.contents.Count > 0 )
            {
                if ( startupManifest.contents[0] == null )
                {
                    return;
                }
                startupManifest.contents[0].Load();
                Destroy( gameObject );
            }
            else
            {
                Debug.LogError( "CANNOT LOAD CONTENT: no content to load from " + startupManifest.name );
            }
        }
    }
}
