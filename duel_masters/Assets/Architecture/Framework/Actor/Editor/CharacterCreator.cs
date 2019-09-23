using UnityEngine;
using UnityEditor;

public static class CharacterCreator
{
    public const string CREATE_TPS_CHARACTER = "GameObject/Character Presets/Create TPS Character";

    public const string TPS_CHARACTER_PATH = "Prefabs/TPS_CHARACTER";

    [MenuItem(CREATE_TPS_CHARACTER, priority = 1)]
    public static void CreateTPSCharacter()
    {
        GameObject _prefab = Resources.Load<GameObject>(TPS_CHARACTER_PATH);
        if(_prefab != null)
        {
            UnityEngine.Object.Instantiate(_prefab);
        }
        else
        {
            Debug.LogError("TPS Character path could not be found");
        }
    }
}
