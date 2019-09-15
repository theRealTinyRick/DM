/*
 Author: Aaron Hines
 Edits By: 
 Description: Helper class for passing around hit info gained from raycasts or collisions - 
 may be removed later if it proves to be easier to just pass RayCastHit or ContactPoint
 */
using System;
using UnityEngine;

[Serializable]
public class HitInfo
{
    public HitInfo(GameObject source, GameObject objectHit, Vector3 hitPoint, Vector3 hitNormal)
    {
        this.source = source;
        this.objectHit = objectHit;
        this.hitPoint = hitPoint;
        this.hitNormal = hitNormal;
    }

    public GameObject source
    {
        get;
        private set;
    }

    public GameObject objectHit
    {
        get;
        private set;
    }

    public Vector3 hitPoint
    {
        get;
        private set;
    }

    public Vector3 hitNormal
    {
        get;
        private set;
    }
}