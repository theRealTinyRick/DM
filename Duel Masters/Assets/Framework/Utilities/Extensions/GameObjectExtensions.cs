/*
 Author: Aaron Hines
 Edits By: 
 Description: Holds some extension methods for gameobjects
 */
using UnityEngine;

namespace GameFramework.Utilities.Extensions
{
    public static class GameObjectExtensions
    {
        public static bool WithInLayerMask(this GameObject gameObject, LayerMask layerMask)
        {
            return layerMask == (layerMask | (1 << gameObject.layer));
        }
    }
}
