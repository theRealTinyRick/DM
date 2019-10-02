using UnityEngine;
using UnityEditor;
using System.IO;

public class GenerateMaterial
{
    [MenuItem( "Assets/Duel Masters/Create Unlit Texture" )]
    public static void MakeMaterialFromTex()
    {
        foreach ( var _selection in Selection.objects )
        {
            Texture _tex = _selection as Texture;
            if(_tex != null)
            {
                Material _mat = new Material( Shader.Find( "Unlit/Texture" ) );
                _mat.SetTexture( "_MainTex", _tex );

                if(!Directory.Exists( GetPath() + "Materials/" ) )
                {
                    Directory.CreateDirectory( GetPath() + "Materials/" );
                }
                AssetDatabase.CreateAsset( _mat, GetPath() + "Materials/" + _selection.name + "_mat.mat" /*"Assets/MyMaterial.mat"*/);
            }
            else
            {
                Debug.Log( "This operation can only be performed on textures" );
            }
        }
    }

    private static string GetPath()
    {
        string path = AssetDatabase.GetAssetPath( Selection.activeObject );
        if ( path == "" )
        {
            path = "Assets";
        }
        else if ( Path.GetExtension( path ) != "" )
        {
            path = path.Replace( Path.GetFileName( AssetDatabase.GetAssetPath( Selection.activeObject ) ), "" );
        }
        return path;
    }
}
