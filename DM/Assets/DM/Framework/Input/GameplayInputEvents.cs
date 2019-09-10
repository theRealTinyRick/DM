/*
 Author: Aaron Hines
 Edits By: 
 Description: General Inpout events that will be used by the PlayerInputComponent
 */
using System;
using UnityEngine;
using UnityEngine.Events;

namespace GameFramework.Core.GameFramework.InputEvents
{
    [Serializable]
    public class LocomotionInputEvent : UnityEvent<Vector3> { }

    [Serializable]
    public class CameraInputEvent : UnityEvent<Vector2> { }

    [Serializable]
    public class SelectionInputEvent : UnityEvent<Vector2> { }


    [Serializable]
    public class JumpButtonEvent : UnityEvent { }


    [Serializable]
    public class FireInputEvent : UnityEvent { }

    [Serializable]
    public class FocusInputEvent : UnityEvent { }


    [Serializable]
    public class ActionButtonEvent : UnityEvent { }
}


