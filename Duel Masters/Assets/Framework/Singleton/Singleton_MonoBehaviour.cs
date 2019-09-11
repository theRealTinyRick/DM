/*
 Author: Aaron Hines
 Edits By: 
 Description:
 */
using UnityEngine;

public class Singleton_MonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            lock (_lock)
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        return instance;
                    }

                    if (instance == null)
                    {
                        GameObject singleton = new GameObject();
                        instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();

                        DontDestroyOnLoad(singleton);
                    }
                }

                return instance;
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