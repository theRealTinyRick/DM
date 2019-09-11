/*
 Author: Aaron Hines
 Edits By: 
 Description:
 */
using UnityEngine;

using Sirenix.OdinInspector;

public abstract class Singleton_SerializedMonobehaviour<T> : SerializedMonoBehaviour where T : SerializedMonoBehaviour
{
    private static T _instance;

    private static object _lock = new object();

    public static T instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();

                        DontDestroyOnLoad(singleton);
                    }
                }

                return _instance;
            }
        }
    }

    private static bool applicationIsQuitting = false;

    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);

        Enable();
    }

    private void OnDisable()
    {
        Disable();
    }

    private void OnDestroy()
    {
        applicationIsQuitting = true;
    }

    // this method is meant to be overridden
    protected virtual void Enable()
    {
    }

    // this method is meant to be overridden
    protected virtual void Disable()
    {
    }
}