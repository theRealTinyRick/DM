/*
 Author: Aaron Hines
 Edits By: 
 Description:
 */
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnEnableEvent : UnityEvent
{ }

[System.Serializable]
public class OnDisableEvent : UnityEvent
{ }

public class OnEnableComponent : MonoBehaviour
{
    [SerializeField]
    public OnEnableEvent onEnable = new OnEnableEvent();
    public OnDisableEvent onDisable = new OnDisableEvent();

    public void OnEnable()
    {
        onEnable?.Invoke(); 
    }

    public void OnDisable()
    {
        onDisable?.Invoke();
    }
}
