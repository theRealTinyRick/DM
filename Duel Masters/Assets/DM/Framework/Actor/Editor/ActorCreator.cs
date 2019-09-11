/*
 Author: Aaron Hines
 Edits By: 
 Description: Generates the generic prefabs for actors, pawns and characters
 */
using UnityEngine;
using UnityEditor;

namespace GameFramework.Actors
{
    public static class ActorCreator
    {
        public const string CREATE_ACTOR = "GameObject/Actor/Create Actor";
        public const string CREATE_PAWN = "GameObject/Actor/Create Pawn";
        public const string CREATE_CHARACTER = "GameObject/Actor/Create Character";

        public const string ACTOR_PATH = "Prefabs/Actor";
        public const string PAWN_PATH = "Prefabs/Pawn";
        public const string CHARACTER_PATH = "Prefabs/Character";

        /// <summary>
        /// Creates a new pawn in the scene
        /// </summary>
        [MenuItem(CREATE_ACTOR, priority = 1)]
        public static void CreateActor()
        {
            GameObject _actorPrefab = Resources.Load<GameObject>(ACTOR_PATH);
            if(_actorPrefab != null)
            {
                Object.Instantiate(_actorPrefab);
            }
            else
            {
                Debug.LogError("Actor path could not be found");
            }
        }

        /// <summary>
        /// Creates a new pawn in the scene
        /// </summary>
        [MenuItem(CREATE_PAWN, priority = 2)]
        public static void CreatePawn ()
        {
            GameObject _pawnPrefab = Resources.Load<GameObject>(PAWN_PATH);
            if (_pawnPrefab != null)
            {
                Object.Instantiate(_pawnPrefab);
            }
            else
            {
                Debug.LogError("Pawn path could not be found");
            }
        }

        /// <summary>
        /// Creates a new character in the scene
        /// </summary>
        [MenuItem(CREATE_CHARACTER, priority = 3)]
        public static void CreateCharacter()
        {
            GameObject _pawnPrefab = Resources.Load<GameObject>(CHARACTER_PATH);
            if (_pawnPrefab != null)
            {
                Object.Instantiate(_pawnPrefab);
            }
            else
            {
                Debug.LogError("Character path could not be found");
            }
        }
    }
}
